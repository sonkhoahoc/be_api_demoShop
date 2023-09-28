using Admin.Services.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using VHUX.Api.IRepository;
using VHUX.API.Model.Customer;
using VHUX.Model;

namespace VHUX.Api.Controllers
{
    [Route("api/customer")]
    [ApiController]
    public class CustomerController : BaseController
    {
        private readonly IConfiguration _configuration;

        private readonly ICustomerRepository _customerRepository;
        public CustomerController(ICustomerRepository customerRepository, IConfiguration configuration)
        {
            _customerRepository = customerRepository;
            _configuration = configuration;
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(CustomerLoginModel login)
        {
            try
            {

                var user = await _customerRepository.CustomerGetPhone(login.username);
                if (user != null && user.id > 0)
                {

                    int checkAccount = await _customerRepository.Authenticate(login);
                    CustomerTokenModel userAuthen = new();
                    if (checkAccount == 1)
                    {
                        CustomerClaimModel claim = new CustomerClaimModel
                        {

                            full_name = user.name,
                            id = user.id,
                            username = user.name,
                        };
                        string tokenString = GenerateToken(claim);
                        userAuthen.token = tokenString;
                        userAuthen.id = user.id;
                        ;
                        userAuthen.full_name = user.name;
                        userAuthen.token = tokenString;
                        CustomerModel customer = await _customerRepository.Customer(user.id);
                        return Ok(new ResponseSingleContentModel<CustomerTokenModel>
                        {
                            StatusCode = 200,
                            Message = "Đăng nhập thành công",
                            Data = userAuthen
                        });
                    }
                    else
                    {
                        return Ok(new ResponseSingleContentModel<string>
                        {
                            StatusCode = 500,
                            Message = "Sai tài khoản hoặc mật khẩu",
                            Data = null
                        });
                    }
                }
                else
                {
                    return Ok(new ResponseSingleContentModel<string>
                    {
                        StatusCode = 500,
                        Message = "Tài khoản không tồn tại trong hệ thống",
                        Data = null
                    });
                }
            }
            catch
            {
                return Ok(new ResponseSingleContentModel<string>
                {
                    StatusCode = 500,
                    Message = "Tài khoản không tồn tại trong hệ thống",
                    Data = null
                });
            }
            // Return invalidate data

        }
        private string GenerateToken(CustomerClaimModel user)
        {
            var identity = GetClaims(user);

            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["TokenSettings:Key"]));
            var token = new JwtSecurityToken(
            _configuration["TokenSettings:Issuer"],
             _configuration["TokenSettings:Audience"],
              expires: DateTime.Now.AddDays(3),
              claims: identity,
              signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
              );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        private IEnumerable<Claim> GetClaims(CustomerClaimModel user)
        {
            var claims = new List<Claim>
            {
               new Claim(JwtRegisteredClaimNames.Sub, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Typ, user.type.ToString()),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.username.ToString()),
                new Claim(JwtRegisteredClaimNames.Name, user.full_name),
                new Claim(JwtRegisteredClaimNames.Email, user.email),
                new Claim(JwtRegisteredClaimNames.Sid, user.id.ToString())
            };



            return claims;
        }
        [AllowAnonymous]
        [HttpPost("create")]
        public async Task<IActionResult> CustomerCreate([FromBody] CustomerAddModel model)
        {
            try
            {
                var checkUser = await this._customerRepository.CustomerCheck(model.phone);
                if (checkUser <= 0)
                {

                    var checkadd = await this._customerRepository.CustomerCreate(model);
                    return checkadd == "0"
                        ? Ok(new ResponseSingleContentModel<string>
                        {
                            StatusCode = 200,
                            Message = "Tạo tài khoản thành công",
                            Data = string.Empty
                        })
                        : (IActionResult)Ok(new ResponseSingleContentModel<string>
                        {
                            StatusCode = 500,
                            Message = "Có lỗi trong quá trình xử lý " + checkadd,
                            Data = string.Empty
                        });

                }
                return Ok(new ResponseSingleContentModel<string>
                {
                    StatusCode = 400,
                    Message = "Tài khoản, email hoặc số điện thoại đã được đăng ký vui lòng kiểm tra lại",
                    Data = string.Empty
                });
            }
            catch (Exception)
            {
                return this.RouteToInternalServerError();


            }
        }
        [HttpGet("detail")]
        public async Task<IActionResult> Customer(long id)
        {
            try
            {
                var Customer = await this._customerRepository.Customer(id);
                return Ok(new ResponseSingleContentModel<CustomerModel>
                {
                    StatusCode = 200,
                    Message = "Success",
                    Data = Customer
                });

            }
            catch (Exception ex)
            {
                return this.RouteToInternalServerError();
            }
        }
        [AllowAnonymous]
        [HttpGet("list")]
        public async Task<IActionResult> CustomerList(string? keyword, int page_number, int page_size)
        {
            try
            {
                var Customer = await this._customerRepository.CustomerList(keyword, page_number, page_size);
                return Ok(new ResponseSingleContentModel<PaginationSet<CustomerModel>>
                {
                    StatusCode = 200,
                    Message = "Success",
                    Data = Customer
                });
            }
            catch (Exception ex)
            {
                return this.RouteToInternalServerError();

            }
        }
        [HttpPost("modify")]
        public async Task<IActionResult> CustomerModify([FromBody] CustomerModel model)
        {
            try
            {
                //model.userUpdated = userid(_httpContextAccessor);
                var Customer = await this._customerRepository.CustomerModify(model);
                return Ok(new ResponseSingleContentModel<CustomerModel>
                {
                    StatusCode = 200,
                    Message = "Success",
                    Data = Customer
                });
            }
            catch (Exception ex)
            {
                return this.RouteToInternalServerError();
            }
        }
        [HttpDelete("delete")]
        public async Task<IActionResult> CustomerDelete(long customer_id)
        {
            try
            {
                long user_id = 0;// = userid(_httpContextAccessor);
                var response = await this._customerRepository.CustomerDelete(customer_id, user_id);
                if (response)
                {
                    return Ok(new ResponseSingleContentModel<bool>
                    {
                        StatusCode = 200,
                        Message = "Success",
                        Data = response
                    });
                }
                else
                    return this.RouteToInternalServerError();

            }
            catch (Exception ex)
            {
                return this.RouteToInternalServerError();


            }
        }
    }
}
