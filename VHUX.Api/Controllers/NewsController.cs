using Admin.Services.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VHUX.Api.Entity;
using VHUX.Api.IRepository;
using VHUX.Model;

namespace VHUX.Api.Controllers
{
    [Route("api/news")]
    [ApiController]
    public class NewsController : BaseController
    {
        private readonly INewsRepository _newsRepository;
        public NewsController(INewsRepository newsRepository)
        {
            _newsRepository = newsRepository;
        }
        #region product
        [HttpGet("news-list")]
        public async Task<IActionResult> NewsList(string? keyword, long category_id,int page_number, int page_size)
        {
            try
            {
                var news = await this._newsRepository.NewsList(keyword, category_id,  page_number, page_size);
                return Ok(new ResponseSingleContentModel<PaginationSet<News>>
                {
                    StatusCode = 200,
                    Message = "Lấy danh sách thành công.",
                    Data = news
                });
            }
            catch (Exception)
            {
                return Ok(new ResponseSingleContentModel<IResponseData>
                {
                    StatusCode = 500,
                    Message = "Có lỗi trong quá trình xử lý",
                    Data = null
                });
            }
        }
        [HttpGet("news")]
        public async Task<IActionResult> News(long id)
        {
            try
            {
                var news = await this._newsRepository.News(id);
                return Ok(new ResponseSingleContentModel<News>
                {
                    StatusCode = 200,
                    Message = "",
                    Data = news
                });
            }
            catch (Exception)
            {
                return Ok(new ResponseSingleContentModel<IResponseData>
                {
                    StatusCode = 500,
                    Message = "",
                    Data = null
                });
            }
        }
        [HttpPost("news-create")]
        public async Task<IActionResult> NewsCreate([FromBody] News model)
        {
            try
            {

                var news = await this._newsRepository.NewsCreate(model);
                return Ok(new ResponseSingleContentModel<News>
                {
                    StatusCode = 200,
                    Message = "Thêm mới thành công",
                    Data = news
                });

            }
            catch (Exception)
            {
                return Ok(new ResponseSingleContentModel<IResponseData>
                {
                    StatusCode = 500,
                    Message = "Có lỗi trong quá trình xử lý",
                    Data = null
                });
            }
        }
        [HttpPost("news-modify")]
        public async Task<IActionResult> NewsModify([FromBody] News model)
        {
            try
            {
                var news = await this._newsRepository.NewsModify(model);

                return Ok(new ResponseSingleContentModel<News>
                {
                    StatusCode = 200,
                    Message = "Cập nhật thành công",
                    Data = news
                });

            }
            catch (Exception)
            {
                return Ok(new ResponseSingleContentModel<IResponseData>
                {
                    StatusCode = 500,
                    Message = "Có lỗi trong quá trình xử lý",
                    Data = null
                });
            }
        }
        [HttpDelete("news-delete")]
        public async Task<IActionResult> NewsDelete(long id)
        {
            try
            {
                bool check = await this._newsRepository.NewsDelete(id);
                return check
                    ? Ok(new ResponseSingleContentModel<string>
                    {
                        StatusCode = 200,
                        Message = "Xóa bản ghi thành công",
                        Data = null
                    })
                    : (IActionResult)Ok(new ResponseSingleContentModel<string>
                    {
                        StatusCode = 500,
                        Message = "Bản ghi không tồn tại hoặc bị xóa trước đó",
                        Data = null
                    });
            }
            catch (Exception)
            {
                return Ok(new ResponseSingleContentModel<IResponseData>
                {
                    StatusCode = 500,
                    Message = "Có lỗi trong quá trình xử lý",
                    Data = null
                });
            }
        }
        #endregion
    }
}
