using VHUX.Api.Entity;
using VHUX.Model;

namespace VHUX.Api.IRepository
{
    public interface IOrderRepository
    {
        Task<PaginationSet<Order>> OrderList(string? keyword, byte status, int page_number, int page_size);
        Task<Order> Order(long id);
        Task<bool> OrderDelete(long id);
        Task<Order> OrderCreate(Order order);
        Task<Order> OrderModify(Order order);
    }
}
