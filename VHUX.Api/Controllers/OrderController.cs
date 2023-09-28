using Admin.Services.Controllers;
using Microsoft.AspNetCore.Mvc;
using VHUX.Api.Entity;
using VHUX.Api.IRepository;
using VHUX.Model;

namespace VHUX.Api.Controllers
{
    [Route("api/order")]
    public class OrderController : BaseController
    {
        private readonly IOrderRepository _orderRepository;
        public OrderController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        #region order
        [HttpGet("order-list")]
        public async Task<IActionResult> OrderList(string? keyword, byte status, int page_number, int page_size)
        {
            try
            {
                var order = await this._orderRepository.OrderList(keyword, status, page_number, page_size);
                return Ok(new ResponseSingleContentModel<PaginationSet<Order>>
                {
                    StatusCode = 200,
                    Message = "Lấy danh sách thành công.",
                    Data = order
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
        [HttpGet("order")]
        public async Task<IActionResult> Order(long id)
        {
            try
            {
                var order = await this._orderRepository.Order(id);
                return Ok(new ResponseSingleContentModel<Order>
                {
                    StatusCode = 200,
                    Message = "",
                    Data = order
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
        [HttpPost("order-create")]
        public async Task<IActionResult> OrderCreate([FromBody] Order model)
        {
            try
            {

                var order = await this._orderRepository.OrderCreate(model);
                return Ok(new ResponseSingleContentModel<Order>
                {
                    StatusCode = 200,
                    Message = "Thêm mới thành công",
                    Data = order
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
        [HttpPost("order-modify")]
        public async Task<IActionResult> OrderModify([FromBody] Order model)
        {
            try
            {
                var order = await this._orderRepository.OrderModify(model);

                return Ok(new ResponseSingleContentModel<Order>
                {
                    StatusCode = 200,
                    Message = "Cập nhật thành công",
                    Data = order
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
        [HttpDelete("order-delete")]
        public async Task<IActionResult> OrderDelete(long id)
        {
            try
            {
                bool check = await this._orderRepository.OrderDelete(id);
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
