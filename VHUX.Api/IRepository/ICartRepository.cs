using Microsoft.EntityFrameworkCore;
using VHUX.Api.Entity;
using VHUX.Api.Model.Customer;
using VHUX.Model;

namespace VHUX.Api.IRepository
{
    public interface ICartRepository
    {
        Task<PaginationSet<Cart>> CartList(string? keyword, int page_number, int page_size);
        Task<Cart> Cart(long id);
        Task<bool> CartDelete(long id);
        Task<Cart> CartCreate(Cart cart);
        Task<Cart> CartModify(Cart cart);
        #region Customer_Cart
        Task<Customer_Cart> CustomerCartCreate(Customer_Cart model);
        Task<Customer_Cart> CustomerCartModify(Customer_Cart model);
        Task<List<CustomerCartModel>> CustomerCartList(long customer_id);
        #endregion
    }
}
