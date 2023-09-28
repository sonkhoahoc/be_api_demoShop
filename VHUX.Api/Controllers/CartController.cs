using Admin.Services.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VHUX.Api.Entity;
using VHUX.Api.IRepository;
using VHUX.Model;

namespace VHUX.Api.Controllers
{
    [Route("api/cart")]
    public class CartController : BaseController
    {
        private readonly ICartRepository _cartRepository;
        public CartController(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }
        #region card
        [HttpGet("cart-list")]
        public async Task<IActionResult> CartList(string? keyword, int page_number, int page_size)
        {
            try
            {
                var cart = await this._cartRepository.CartList(keyword, page_number, page_size);
                return Ok(new ResponseSingleContentModel<PaginationSet<Cart>>
                {
                    StatusCode = 200,
                    Message = "Lấy danh sách thành công.",
                    Data = cart
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
        [HttpGet("cart")]
        public async Task<IActionResult> Cart(long id)
        {
            try
            {
                var cart = await this._cartRepository.Cart(id);
                return Ok(new ResponseSingleContentModel<Cart>
                {
                    StatusCode = 200,
                    Message = "",
                    Data = cart
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
        [HttpPost("cart-create")]
        public async Task<IActionResult> OrderCreate([FromBody] Cart model)
        {
            try
            {

                var cart = await this._cartRepository.CartCreate(model);
                return Ok(new ResponseSingleContentModel<Cart>
                {
                    StatusCode = 200,
                    Message = "Thêm mới thành công",
                    Data = cart
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
        [HttpPost("cart-modify")]
        public async Task<IActionResult> CartModify([FromBody] Cart model)
        {
            try
            {
                var cart = await this._cartRepository.CartModify(model);

                return Ok(new ResponseSingleContentModel<Cart>
                {
                    StatusCode = 200,
                    Message = "Cập nhật thành công",
                    Data = cart
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
        [HttpDelete("cart-delete")]
        public async Task<IActionResult> CartDelete(long id)
        {
            try
            {
                bool check = await this._cartRepository.CartDelete(id);
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
        #region Customer_Cart
        [HttpPost("customer-cart-create")]
        public async Task<IActionResult> CustomerCartCreate([FromBody] Customer_Cart model)
        {
            try
            {

                var response = await this._cartRepository.CustomerCartCreate(model);
                return Ok(new ResponseSingleContentModel<Customer_Cart>
                {
                    StatusCode = 200,
                    Message = "Thêm mới thành công",
                    Data = response
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
        [HttpPost("customer-cart-modify")]
        public async Task<IActionResult> CustomerCartModify([FromBody] Customer_Cart model)
        {
            try
            {
                var response = await this._cartRepository.CustomerCartModify(model);
                return Ok(new ResponseSingleContentModel<Customer_Cart>
                {
                    StatusCode = 200,
                    Message = "Thêm mới thành công",
                    Data = response
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
        [HttpGet("customer-cart-list")]
        public async Task<IActionResult> CustomerCartList(long customer_id)
        {
            try
            {
                var response = await this._cartRepository.CustomerCartList(customer_id);
                return Ok(new ResponseSingleContentModel<List<Customer_Cart>>
                {
                    StatusCode = 200,
                    Message = "",
                    Data = response
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
        #endregion
    }
}
