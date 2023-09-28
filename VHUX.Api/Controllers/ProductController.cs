using Admin.Services.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VHUX.Api.Entity;
using VHUX.Api.IRepository;
using VHUX.Model;

namespace VHUX.Api.Controllers
{
    [Route("api/product")]
    public class ProductController : BaseController

    {
        private readonly IProductRepository _productRepository;
        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        #region product
        [HttpGet("product-list")]
        public async Task<IActionResult> ProductList(string? keyword, long category_id, long category_size_id, int page_number, int page_size)
        {
            try
            {
                var product = await this._productRepository.ProductList(keyword, category_id,category_size_id,page_number,page_size);
                return Ok(new ResponseSingleContentModel<PaginationSet<Product>>
                {
                    StatusCode = 200,
                    Message = "Lấy danh sách thành công.",
                    Data = product
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
        [HttpGet("product")]
        public async Task<IActionResult> Product(long id)
        {
            try
            {
                var product = await this._productRepository.Product(id);
                return Ok(new ResponseSingleContentModel<Product>
                {
                    StatusCode = 200,
                    Message = "",
                    Data = product
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
        [HttpPost("product-create")]
        public async Task<IActionResult> ProductCreate([FromBody] Product model)
        {
            try
            {

                var product = await this._productRepository.ProductCreate(model);
                return Ok(new ResponseSingleContentModel<Product>
                {
                    StatusCode = 200,
                    Message = "Thêm mới thành công",
                    Data = product
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
        [HttpPost("product-modify")]
        public async Task<IActionResult> ProductModify([FromBody] Product model)
        {
            try
            {
                var product = await this._productRepository.ProductModify(model);

                return Ok(new ResponseSingleContentModel<Product>
                {
                    StatusCode = 200,
                    Message = "Cập nhật thành công",
                    Data = product
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
        [HttpDelete("product-delete")]
        public async Task<IActionResult> ProductDelete(long id)
        {
            try
            {
                bool check = await this._productRepository.ProductDelete(id);
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
