using Microsoft.EntityFrameworkCore;
using VHUX.Api.Entity;
using VHUX.Api.IRepository;
using VHUX.Model;

namespace VHUX.Api.Repository
{
    public class NewsRepository: INewsRepository
    {
        private readonly ApplicationContext _context;
        public NewsRepository(ApplicationContext context)
        {
            _context = context;
        }
        #region  tin
        public async Task<PaginationSet<News>> NewsList(string? keyword, long category_id, int page_number, int page_size)
        {
            return await Task.Run(async () =>
            {
                PaginationSet<News> response = new PaginationSet<News>();
                var listItem = _context.News.Where(x => !x.is_delete);

                if (keyword is not null and not "")
                {
                    listItem = listItem.Where(r => r.title.Contains(keyword) || r.description.Contains(keyword));
                }
                if (category_id > 0)
                {
                    listItem = listItem.Where(r => r.category_id == category_id);
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
        }
        public async Task<News> News(long id)
        {
            News? category_News = await Task.Run(() =>
            {
                News response = _context.News.Find(id);
                return response;
            });
            return category_News;
        }
        public Task<bool> NewsDelete(long id)
        {
            News news = _context.News.FirstOrDefault(c => c.id == id);
            news.is_delete = true;
            _context.News.Update(news);
            _context.SaveChanges();

            return Task.FromResult(_context.SaveChanges() > 0);
        }
        public async Task<News> NewsCreate(News news)
        {
            return await Task.Run(() =>
            {
                _context.News.Add(news);
                _context.SaveChanges();
                return news;
            });
        }
        public async Task<News> NewsModify(News news)
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
    }
}
