
using Microsoft.EntityFrameworkCore;
using VHUX.Api.Entity;
using VHUX.Api.IRepository;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;

namespace VHUX.Api.Repository

{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationContext _context;
        public CategoryRepository(ApplicationContext context)
        {
            _context = context;
        }
        #region danh mục tin
        public async Task<List<Category_News>> CategoryNewsList(string keyword)
        {
            return await Task.Run(() =>
            {
                List<Category_News> response = _context.Category_News.Where(x => !x.is_delete && (keyword == "" || keyword == null || x.name.Contains(keyword))).OrderByDescending(x => x.id).ToList();
                return response;
            });
        }
        public async Task<Category_News> CategoryNews(long id)
        {
            Category_News? category_News = await Task.Run(() =>
            {
                Category_News response = _context.Category_News.Find(id);
                return response;
            });
            return category_News;
        }
        public Task<bool> CategoryNewsDelete(long id)
        {
            Category_News category_News = _context.Category_News.FirstOrDefault(c => c.id == id);
            category_News.is_delete = true;
            _context.Category_News.Update(category_News);
            _context.SaveChanges();

            return Task.FromResult(_context.SaveChanges() > 0);
        }
        public async Task<Category_News> CategoryNewsCreate(Category_News news)
        {
            return await Task.Run(() =>
            {
                _context.Category_News.Add(news);
                _context.SaveChanges();
                return news;
            });
        }
        public async Task<Category_News> CategoryNewsModify(Category_News news)
        {
            return await Task.Run(() =>
            {
                try
                {
                    _context.Entry(news).State = EntityState.Modified;
                    _context.SaveChanges();
                }
                catch
                {
                    news.id = 0;
                }

                return news;
            });
        }
        #endregion

        #region danh sản phẩm
        public async Task<List<Category_Product>> CategoryProductList(string? keyword)
        {
            //return await Task.Run(async () =>
            //{
            List<Category_Product> response = _context.Category_Product.Where(x => !x.is_delete && (keyword == "" || keyword == null || x.name.Contains(keyword))).OrderByDescending(x => x.id).ToList();
            return response;
            //});
        }

        public async Task<Category_Product> CategoryProduct(long id) => _context.Category_Product.FirstOrDefault(r => r.id == id);

        public Task<bool> CategoryProductDelete(long id)
        {
            Category_Product category = _context.Category_Product.FirstOrDefault(c => c.id == id);
            category.is_delete = true;
            _context.Category_Product.Update(category);
            _context.SaveChanges();

            return Task.FromResult(_context.SaveChanges() > 0);
        }
        public async Task<Category_Product> CategoryProductCreate(Category_Product category)
        {
            return await Task.Run(() =>
            {
                _context.Category_Product.Add(category);
                _context.SaveChanges();
                return category;
            });
        }
        public async Task<Category_Product> CategoryProductModify(Category_Product category)
        {
            return await Task.Run(() =>
            {
                try
                {
                    _context.Entry(category).State = EntityState.Modified;
                    _context.SaveChanges();
                }
                catch
                {
                    category.id = 0;
                }

                return category;
            });
        }
        #endregion

        #region danh kích cỡ
        public async Task<List<Category_Size>> CategorySizeList()
        {
            return await Task.Run(async () =>
            {
                List<Category_Size> response = await _context.Category_Size.Where(x => !x.is_delete).OrderByDescending(x => x.id).ToListAsync();
                return response;
            });
        }
        public async Task<Category_Size> CategorySize(long id) => _context.Category_Size.FirstOrDefault(r => r.id == id);
        public async Task<bool> CategorySizeDelete(long id)
        {
            return await Task.Run(() =>
            {
                Category_Size category = _context.Category_Size.FirstOrDefault(c => c.id == id);
                category.is_delete = true;
                _context.Category_Size.Update(category);
                _context.SaveChanges();

                return true;
            });
        }
        public async Task<Category_Size> CategorySizeCreate(Category_Size category)
        {
            return await Task.Run(() =>
            {
                _context.Category_Size.Add(category);
                _context.SaveChanges();
                return category;
            });
        }
        public async Task<Category_Size> CategorySizeModify(Category_Size category)
        {
            return await Task.Run(() =>
            {
                try
                {
                    _context.Entry(category).State = EntityState.Modified;
                    _context.SaveChanges();
                }
                catch
                {
                    category.id = 0;
                }

                return category;
            });
        }
        #endregion

        #region Config
        public async Task<Config?> Config()
        {
            Config response = _context.Config.FirstOrDefault();
            if (response == null) { return null; }
            return response;
        }
        public async Task<Config> ConfigUpdate(Config model)
        {
            Config response = _context.Config.FirstOrDefault();
            if (response != null)
            {
                response.email = model.email;
                response.company_name = model.company_name;
                response.address = model.address;
                response.hotline = model.hotline;
                response.map_iframe = model.map_iframe;
                _context.Config.Update(response);
                return response;

            }
            else
            {
                _context.Config.Add(model);
                return model;
            }
        }
        #endregion
    }
}
