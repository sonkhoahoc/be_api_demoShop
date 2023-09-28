using AutoMapper;
using VHUX.Api.Entity;
using VHUX.API.Model.User;

namespace VHUX.Api.Mapper
{
    public class AddAutoMapper : Profile
    {
        public AddAutoMapper()
        {
            CreateMap<Admin_User, UserCreateModel>();
            CreateMap<UserCreateModel, Admin_User>();

            CreateMap<Admin_User, UserModifyModel>();
            CreateMap<UserModifyModel, Admin_User>();

            CreateMap<Admin_User, UserModel>();
        }
    }
}
