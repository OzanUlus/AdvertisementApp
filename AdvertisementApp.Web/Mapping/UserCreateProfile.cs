using AdvertisementApp.Dtos;
using AdvertisementApp.Web.Models;
using AutoMapper;

namespace AdvertisementApp.Web.Mapping
{
    public class UserCreateProfile : Profile
    {
        public UserCreateProfile()
        {
            CreateMap<UserCreateModel,AppUserCreateDto>().ReverseMap();
        }



    }
}
