using VHUX.Api.Entity;
using VHUX.API.Model.User;
using VHUX.Model;

namespace VHUX.Api.IRepository
{
    public interface IUserRepository
    {
        Task<UserModel> UserGetById(long id);
        Task<UserModel> UserCreate(UserCreateModel useradd);
        Task<UserModifyModel> UserModify(UserModifyModel userupdate);
        Task<bool> ChangePassUser(ChangePassModel model);
        Task<bool> UserDelete(long id, long userUpdate);
        Task<PaginationSet<UserModel>> UserList(string? full_name, string? username, int page_number, int page_size);
        int Authenticate(LoginModel login);
        Task<Admin_User> CheckUser(string username);
        Task<int> CheckUserExists(string username, string phone_number, string email); 
    }
}
