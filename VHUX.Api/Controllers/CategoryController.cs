using Admin.Services.Controllers;
using Microsoft.AspNetCore.Mvc;
using VHUX.Api.Entity;
using VHUX.Api.IRepository;
using VHUX.Model;

namespace VHUX.Api.Controllers
{
    [Route("api/category")]
    public class CategoryController : BaseController

    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        #region product
        [HttpGet("category-product-list")]
        public async Task<IActionResult> CategoryProduct(string? keyword)
        {
            try
            {
                var category = await this._categoryRepository.CategoryProductList(keyword);
                return Ok(new ResponseSingleContentModel<List<Category_Product>>
                {
                    StatusCode = 200,
                    Message = "Lấy danh sách thành công.",
                    Data = category
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
        [HttpGet("category-product")]
        public async Task<IActionResult> CategoryProduct(long id)
        {
            try
            {
                var bank = await this._categoryRepository.CategoryProduct(id);
                return Ok(new ResponseSingleContentModel<Category_Product>
                {
                    StatusCode = 200,
                    Message = "",
                    Data = bank
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
        [HttpPost("category-product-create")]
        public async Task<IActionResult> CategoryProductCreate([FromBody] Category_Product model)
        {
            try
            {

                var bank = await this._categoryRepository.CategoryProductCreate(model);
                return Ok(new ResponseSingleContentModel<Category_Product>
                {
                    StatusCode = 200,
                    Message = "Thêm mới thành công",
                    Data = bank
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
        [HttpPost("category-product-modify")]
        public async Task<IActionResult> CategoryProductModify([FromBody] Category_Product model)
        {
            try
            {
                var bank = await this._categoryRepository.CategoryProductModify(model);

                return Ok(new ResponseSingleContentModel<Category_Product>
                {
                    StatusCode = 200,
                    Message = "Cập nhật thành công",
                    Data = bank
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
        [HttpDelete("category-product-delete")]
        public async Task<IActionResult> CategoryProductDelete(long id)
        {
            try
            {
                bool bank = await this._categoryRepository.CategoryProductDelete(id);
                return bank
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

        #region news
        [HttpGet("category-news-list")]
        public async Task<IActionResult> CategoryNewsList(string keyword)
        {
            try
            {
                var category = await this._categoryRepository.CategoryNewsList(keyword);
                return Ok(new ResponseSingleContentModel<List<Category_News>>
                {
                    StatusCode = 200,
                    Message = "Lấy danh sách thành công.",
                    Data = category
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
        [HttpGet("category-news")]
        public async Task<IActionResult> CategoryNews(long id)
        {
            try
            {
                var bank = await this._categoryRepository.CategoryNews(id);
                return Ok(new ResponseSingleContentModel<Category_News>
                {
                    StatusCode = 200,
                    Message = "",
                    Data = bank
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
        [HttpPost("category-news-create")]
        public async Task<IActionResult> CategoryNewsCreate([FromBody] Category_News model)
        {
            try
            {

                var bank = await this._categoryRepository.CategoryNewsCreate(model);
                return Ok(new ResponseSingleContentModel<Category_News>
                {
                    StatusCode = 200,
                    Message = "Thêm mới thành công",
                    Data = bank
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
        [HttpPost("category-news-modify")]
        public async Task<IActionResult> CategoryNewsModify([FromBody] Category_News model)
        {
            try
            {
                var bank = await this._categoryRepository.CategoryNewsModify(model);

                return Ok(new ResponseSingleContentModel<Category_News>
                {
                    StatusCode = 200,
                    Message = "Cập nhật thành công",
                    Data = bank
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
        [HttpDelete("category-news-delete")]
        public async Task<IActionResult> CategoryNewsDelete(long id)
        {
            try
            {
                bool bank = await this._categoryRepository.CategoryNewsDelete(id);
                return bank
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
        #region size
        [HttpGet("category-size-list")]
        public async Task<IActionResult> CategorySizeList()
        {
            try
            {
                var size = await this._categoryRepository.CategorySizeList();
                return Ok(new ResponseSingleContentModel<List<Category_Size>>
                {
                    StatusCode = 200,
                    Message = "Lấy danh sách thành công.",
                    Data = size
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
        [HttpGet("category-size")]
        public async Task<IActionResult> CategorySize(long id)
        {
            try
            {
                var size = await this._categoryRepository.CategorySize(id);
                return Ok(new ResponseSingleContentModel<Category_Size>
                {
                    StatusCode = 200,
                    Message = "",
                    Data = size
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
        [HttpPost("category-size-create")]
        public async Task<IActionResult> CategoryNewsCreate([FromBody] Category_Size model)
        {
            try
            {

                var size = await this._categoryRepository.CategorySizeCreate(model);
                return Ok(new ResponseSingleContentModel<Category_Size>
                {
                    StatusCode = 200,
                    Message = "Thêm mới thành công",
                    Data = size
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
        [HttpPost("category-size-modify")]
        public async Task<IActionResult> CategorySizeModify([FromBody] Category_Size model)
        {
            try
            {
                var size = await this._categoryRepository.CategorySizeModify(model);

                return Ok(new ResponseSingleContentModel<Category_Size>
                {
                    StatusCode = 200,
                    Message = "Cập nhật thành công",
                    Data = size
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
        [HttpDelete("category-size-delete")]
        public async Task<IActionResult> CategorySizeDelete(long id)
        {
            try
            {
                bool bank = await this._categoryRepository.CategorySizeDelete(id);
                return bank
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
        #region config
        [HttpGet("config")]
        public async Task<IActionResult> Config()
        {
            try
            {
                var response = await this._categoryRepository.Config();
                if (response != null)
                {
                    return Ok(new ResponseSingleContentModel<Config>
                    {
                        StatusCode = 200,
                        Message = "Lấy thông tin thành công.",
                        Data = response
                    });
                }
                else
                    return Ok(new ResponseSingleContentModel<IResponseData>
                    {
                        StatusCode = 500,
                        Message = "Chưa có config trong hệ thống",
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
        [HttpPost("config-create")]
        public async Task<IActionResult> ConfigCreate([FromBody] Config model)
        {
            try
            {

                var responsee = await this._categoryRepository.ConfigUpdate(model);
                return Ok(new ResponseSingleContentModel<Config>
                {
                    StatusCode = 200,
                    Message = "Cập nhật thành công",
                    Data = responsee
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
