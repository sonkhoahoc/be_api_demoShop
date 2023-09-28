
using VHUX.Api.Entity;

namespace VHUX.Api.IRepository
{
    public interface ICategoryRepository
    {
        #region danh mục tin
        Task<List<Category_News>> CategoryNewsList(string keyword);
        Task<Category_News> CategoryNews(long id);
        Task<bool> CategoryNewsDelete(long id);
        Task<Category_News> CategoryNewsCreate(Category_News news);
        Task<Category_News> CategoryNewsModify(Category_News news);
        #endregion
        #region danh sản phẩm
        Task<List<Category_Product>> CategoryProductList(string? keyword);
        Task<Category_Product> CategoryProduct(long id);
        Task<bool> CategoryProductDelete(long id);
        Task<Category_Product> CategoryProductCreate(Category_Product category);
        Task<Category_Product> CategoryProductModify(Category_Product category);
        #endregion
        #region danh kích cỡ
        Task<List<Category_Size>> CategorySizeList();
        Task<Category_Size> CategorySize(long id);
        Task<bool> CategorySizeDelete(long id);
        Task<Category_Size> CategorySizeCreate(Category_Size category);
        Task<Category_Size> CategorySizeModify(Category_Size category);
        #endregion
        #region config
        Task<Config?> Config();
        Task<Config> ConfigUpdate(Config model);
        #endregion
    }
}
