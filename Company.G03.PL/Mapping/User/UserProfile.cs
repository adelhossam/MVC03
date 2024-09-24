using AutoMapper;
using Company.G03.DAL.Models;
using Company.G03.PL.ViewModels;

namespace Company.G03.PL.Mapping.User
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserViewModel,ApplicationUser>().ReverseMap();
        }
    }
}
