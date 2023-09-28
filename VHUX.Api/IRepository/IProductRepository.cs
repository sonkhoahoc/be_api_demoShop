using VHUX.Api.Entity;
using VHUX.Model;

namespace VHUX.Api.IRepository
{
    public interface IProductRepository
    {
        Task<PaginationSet<Product>> ProductList(string? keyword, long category_id, long category_size_id, int page_number, int page_size);
        Task<Product> Product(long id);
        Task<bool> ProductDelete(long id);
        Task<Product> ProductCreate(Product product);
        Task<Product> ProductModify(Product product);
    }
}
