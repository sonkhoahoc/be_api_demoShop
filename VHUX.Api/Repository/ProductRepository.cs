
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;
using VHUX.Api.Entity;
using VHUX.Api.IRepository;
using VHUX.Model;

namespace Api.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationContext _context;
        public ProductRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<PaginationSet<Product>> ProductList(string? keyword, long category_id, long category_size_id, int page_number, int page_size)
        {
            return await Task.Run(async () =>
            {
                PaginationSet<Product> response = new PaginationSet<Product>();
                var listItem = _context.Product.Where(x => !x.is_delete);

                if (keyword is not null and not "")
                {
                    listItem = listItem.Where(r => r.name.Contains(keyword) || r.code.Contains(keyword));
                }
                if (category_id > 0)
                {
                    listItem = listItem.Where(r => r.category_id == category_id);
                }
                if (category_size_id > 0)
                {
                    listItem = listItem.Where(r => r.category_size_id == category_size_id);
                }
                List<Product> products = new List<Product>();
                if (page_number > 0)
                {
                    response.totalcount = listItem.Select(x => x.id).Count();
                    response.page = page_number;
                    response.maxpage = (int)Math.Ceiling((decimal)response.totalcount / page_size);
                    products = await listItem.OrderByDescending(r => r.id).Skip(page_size * (page_number - 1)).Take(page_size).ToListAsync();
                }
                else
                {
                    products = await listItem.OrderByDescending(r => r.id).ToListAsync();
                }
                List<long> ids = products.Select(r => r.id).ToList();
                List<Product_File> product_Files = _context.Product_File.Where(r => ids.Contains(r.product_id) && !r.is_delete).ToList();
                foreach (var item in products)
                {
                    item.files = product_Files.Where(r => r.product_id == item.id).ToList();
                }
                response.list = products;
                return response;
            });
        }
        public async Task<Product> Product(long id)
        {
            Product? product = await Task.Run(() =>
            {
                Product product = _context.Product.Where(r => r.id == id && !r.is_delete).FirstOrDefault();
                product.files= _context.Product_File.Where(r => r.product_id== product.id && !r.is_delete).ToList();
                return product;
            });
            return product;
        }
        public async Task<bool> ProductDelete(long id)
        {
            return await Task.Run(() =>
            {
                Product product = _context.Product.FirstOrDefault(c => c.id == id);
                product.is_delete = true;
                _context.Product.Update(product);
                _context.SaveChanges();

                return true;
            });
        }
        public async Task<Product> ProductCreate(Product product)
        {
            return await Task.Run(() =>
            {
                _context.Product.Add(product);
                _context.SaveChanges();
                foreach (var item in product.files)
                {
                    item.product_id = product.id;
                }
                _context.Product_File.AddRange(product.files);

                _context.SaveChanges();
                return product;
            });
        }
        public async Task<Product> ProductModify(Product product)
        {
            return await Task.Run(() =>
            {
                try
                {
                    _context.Entry(product).State = EntityState.Modified;
                    foreach (var item in product.files)
                    {
                        if (item.id == 0)
                        {
                            item.product_id = product.id;
                            _context.Product_File.Add(item);

                        }
                        else
                        {
                            _context.Entry(item).State = EntityState.Modified;

                        }

                    }
                    _context.SaveChanges();
                }
                catch
                {
                    product.id = 0;
                }

                return product;
            });
        }
    }
}
