using Admin.Services.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using VHUX.API.Model.User;
using VHUX.Api.Repository;
using VHUX.Model;
using VHUX.Api.IRepository;

namespace VHUX.Api.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : BaseController
    {
        private readonly IConfiguration _configuration;
        private readonly IUserRepository _userRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserController(IUserRepository userRepository,  IHttpContextAccessor httpContextAccessor)
        {
            _userRepository = userRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        #region user
        [AllowAnonymous]
        [HttpPost("admin-user-create")]
        public async Task<IActionResult> UserCreate([FromBody] UserCreateModel model)
        {
            try
            {
                var checkUser = await this._userRepository.CheckUserExists(model.username, model.phone_number, model.email);
                if (checkUser > 0)
                {
                    return Ok(new ResponseSingleContentModel<string>
                    {
                        StatusCode = 400,
                        Message = "Tài khoản, email hoặc số điện thoại đã được đăng ký vui lòng kiểm tra lại",
                        Data = string.Empty
                    });
                }
             
                    var user = await this._userRepository.UserCreate(model);

                    return Ok(new ResponseSingleContentModel<UserModel>
                    {
                        StatusCode = 200,
                        Message = "",
                        Data = user
                    });
                
            }
            catch (Exception)
            {
                return this.RouteToInternalServerError();


            }
        }
        //  [Authorize(Roles = "ADMINUSERMODIFY")]
        [HttpPost("admin-user-modify")]
        public async Task<IActionResult> UserModify([FromBody] UserModifyModel userupdate)
        {
            try
            {
               
                    var user = await this._userRepository.UserModify(userupdate);

                    return Ok(new ResponseSingleContentModel<UserModifyModel>
                    {
                        StatusCode = 200,
                        Message = "Cập nhật thành công",
                        Data = userupdate
                    });
               
            }
            catch (Exception )
            {
                return Ok(new ResponseSingleContentModel<IResponseData>
                {
                    StatusCode = 500,
                    Message = "Có lỗi trong quá trình xử lý",
                    Data = null
                });
            }
        }

        //  [Authorize(Roles = "ADMINUSERLIST")]
        [HttpGet("admin-user-list")]
        public async Task<IActionResult> UserList(string? full_name, string? username, int page_number = 0, int page_size = 20)
        {
            try
            {
                PaginationSet<UserModel> Data = await _userRepository.UserList(full_name, username, page_number, page_size);

                return Ok(new ResponseSingleContentModel<PaginationSet<UserModel>>
                {
                    StatusCode = 200,
                    Message = "Success",
                    Data = Data
                });
            }
            catch (Exception ex)
            {

                return Ok(new ResponseSingleContentModel<UserModel>
                {
                    StatusCode = 500,
                    Message = "Có lỗi xảy ra trong quá trình xử lý",
                    Data = new()
                });
            }

        }

        [HttpGet("admin-authorize-check")]
        public async Task<IActionResult> GetUserById()
        {
            try
            {
                long id = userid(_httpContextAccessor);
                var user = await _userRepository.UserGetById(id);


                return Ok(new ResponseSingleContentModel<UserModel>
                {
                    StatusCode = 200,
                    Message = "Success",
                    Data = user
                });
            }
            catch (Exception ex)
            {

                return Ok(new ResponseSingleContentModel<UserModel>
                {
                    StatusCode = 500,
                    Message = "Đăng nhập không thành công " + ex.Message,
                    Data = new()
                });
            }
        }

        //  [Authorize(Roles = "ADMINUSERDETAIL")]
        [HttpGet("admin-user")]
        public async Task<IActionResult> GetUserById(long id)
        {
            try
            {
                var user = await _userRepository.UserGetById(id);
                return Ok(new ResponseSingleContentModel<UserModel>
                {
                    StatusCode = 200,
                    Message = "Thêm mới thành công",
                    Data = user
                });
            }
            catch (Exception)
            {
                return Ok(new ResponseSingleContentModel<string>
                {
                    StatusCode = 500,
                    Message = "Có lỗi xảy ra trong quá trình xử lý",
                    Data = string.Empty
                });
            }
        }

        [AllowAnonymous]
        [HttpPost("admin-login")]
        public async Task<IActionResult> Login(LoginModel login)
        {
            try
            {
               
                    var user = await _userRepository.CheckUser(login.username);
                    if (user != null)
                    {

                        var checkAccount = _userRepository.Authenticate(login);
                        UserTokenModel userAuthen = new();
                        if (checkAccount == 1)
                        {
                            //List<string> roles = await _userRepository.GetRoleByUser(user.id);
                            ClaimModel claim = new ClaimModel
                            {
                                email = user.email,
                                full_name = user.full_name,
                                id = user.id,
                                type = user.type,
                                username = user.username,
                                //roles = roles,
                            };
                            string tokenString = GenerateToken(claim);
                            userAuthen.token = tokenString;
                            userAuthen.id = user.id;

                            userAuthen.username = user.username;
                            userAuthen.full_name = user.full_name;
                            userAuthen.token = tokenString;
                            //userAuthen.roles = roles;
                            UserModel usermodel = await _userRepository.UserGetById(user.id);
                            return Ok(new ResponseSingleContentModel<UserTokenModel>
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
            catch (Exception)
            {
                return Ok(new ResponseSingleContentModel<string>
                {
                    StatusCode = 500,
                    Message = "Có lỗi xảy ra trong quá trình xử lý",
                    Data = string.Empty
                });
            }
        }
 
        [HttpPost("admin-user-changepass")]
        public async Task<IActionResult> ChangePassUser(ChangePassModel model)
        {
            try
            {
               
                    var user = await this._userRepository.ChangePassUser(model);

                    return Ok(new ResponseSingleContentModel<string>
                    {
                        StatusCode = 200,
                        Message = "Thêm mới thành công",
                        Data = null
                    });
            
             
            }
            catch (Exception)
            {
                return Ok(new ResponseSingleContentModel<string>
                {
                    StatusCode = 500,
                    Message = "Có lỗi xảy ra trong quá trình xử lý",
                    Data = string.Empty
                });
                // return this.RouteToInternalServerError();
            }
        }
        private string GenerateToken(ClaimModel user)
        {
            var identity = GetClaims(user);

            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["TokenSettings:Key"]));
            var token = new JwtSecurityToken(
            _configuration["TokenSettings:Issuer"],
             _configuration["TokenSettings:Audience"],
              expires: DateTime.Now.AddMonths(3),
              claims: identity,
              signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
              );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        private IEnumerable<Claim> GetClaims(ClaimModel user)
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

            foreach (var userRole in user.roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            return claims;
        }
        #endregion

    }
}
