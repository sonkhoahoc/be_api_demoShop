using VHUX.Api.Entity;
using VHUX.Model;

namespace VHUX.Api.IRepository
{
    public interface INewsRepository
    {
        Task<PaginationSet<News>> NewsList(string? keyword, long category_id, int page_number, int page_size);
        Task<News> News(long id);
        Task<bool> NewsDelete(long id);
        Task<News> NewsCreate(News news);
        Task<News> NewsModify(News news);
    }
}
