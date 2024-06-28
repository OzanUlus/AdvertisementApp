using AdvertisementApp.Dtos;
using AdvertisementApp.Entity;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvertisementApp.Business.Mapping
{
    public class AdvertisementAppUserProfile : Profile
    {
        public AdvertisementAppUserProfile()
        {
            CreateMap<AdvertisementAppUser,AdvertisementAppUserCreateDto>().ReverseMap();
            CreateMap<AdvertisementAppUser,AdvertisementAppUserListDto>().ReverseMap();
        }
    }
}
