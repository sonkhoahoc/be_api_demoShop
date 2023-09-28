using Microsoft.EntityFrameworkCore;
using VHUX.Api.Entity;
using VHUX.Api.IRepository;
using VHUX.Api.Model.Customer;
using VHUX.Model;

namespace VHUX.Api.Repository
{
    public class CartRepository : ICartRepository
    {
        private readonly ApplicationContext _context;
        public CartRepository(ApplicationContext context)
        {
            _context = context;
        }
        public async Task<PaginationSet<Cart>> CartList(string? keyword, int page_number, int page_size)
        {
            return await Task.Run(async () =>
            {
                return await Task.Run(async () =>
                {
                    PaginationSet<Cart> response = new PaginationSet<Cart>();
                    var listItem = _context.Cart.Where(x => !x.is_delete);

                    if (keyword is not null and not "")
                    {
                        //  listItem = listItem.Where(r => r.customer_name.Contains(keyword) || r.customer_address.Contains(keyword) || r.customer_phone.Contains(keyword));
                    }


                    if (page_number > 0)
                    {
                        response.totalcount = listItem.Select(x => x.id).Count();
                        response.page = page_number;
                        response.maxpage = (int)Math.Ceiling((decimal)response.totalcount / page_size);
                        response.list = await listItem.OrderByDescending(r => r.id).Skip(page_size * (page_number - 1)).Take(page_size).ToListAsync();
                    }
                    else
                    {
                        response.list = await listItem.OrderByDescending(r => r.id).ToListAsync();
                    }

                    return response;
                });
            });
        }
        public async Task<Cart> Cart(long id)
        {
            Cart cart = _context.Cart.Where(r => r.id == id && !r.is_delete).FirstOrDefault();
            cart.cart_Details = _context.Cart_Detail.Where(r => !r.is_delete && r.cart_id == id).ToList();
            return cart;

        }
        public async Task<bool> CartDelete(long id)
        {
            return await Task.Run(() =>
            {
                Cart cart = _context.Cart.FirstOrDefault(c => c.id == id);
                cart.is_delete = true;
                _context.Cart.Update(cart);
                _context.SaveChanges();

                return true;
            });
        }
        public async Task<Cart> CartCreate(Cart cart)
        {
            return await Task.Run(() =>
            {
                _context.Cart.Add(cart);
                _context.SaveChanges();
                foreach (var item in cart.cart_Details)
                {
                    item.cart_id = cart.id;
                }
                _context.Cart_Detail.AddRange(cart.cart_Details);
                return cart;
            });
        }
        public async Task<Cart> CartModify(Cart cart)
        {
            return await Task.Run(() =>
            {
                try
                {
                    _context.Entry(cart).State = EntityState.Modified;
                    foreach (var item in cart.cart_Details)
                    {
                        if (item.id == 0)
                        {
                            item.cart_id = cart.id;

                            _context.Cart.Add(cart);

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
                    cart.id = 0;
                }

                return cart;
            });
        }

        #region Customer_Cart
        public async Task<Customer_Cart> CustomerCartCreate(Customer_Cart model)
        {
            _context.Customer_Cart.Add(model);
            _context.SaveChanges();
            return model;
        }
        public async Task<Customer_Cart> CustomerCartModify(Customer_Cart model)
        {
            _context.Customer_Cart.Update(model);
            _context.SaveChanges();
            return model;
        }
        public async Task< List<CustomerCartModel>> CustomerCartList(long customer_id)
        {
            var listItem = (from a in _context.Customer_Cart
                            join b in _context.Product on a.product_id equals b.id
                            where (a.customer_id == customer_id && !a.is_delete && !b.is_delete)
                            select new CustomerCartModel
                            {
                                product_name = b.name,
                                product_avatar = _context.Product_File.FirstOrDefault(x => x.product_id == a.product_id).file ?? "",
                                product_size = a.product_size,
                                quantity = a.quantity,
                                product_id = a.product_id,
                                customer_id = a.customer_id
                            });
            return listItem.ToList();
        }
        #endregion

    }
}
