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
    public class GenderProfile : Profile
    {
        public GenderProfile()
        {
            CreateMap<Gender , GenderCreateDto>().ReverseMap();
            CreateMap<Gender , GenderUpdateDto>().ReverseMap();
            CreateMap<Gender , GenderListDto>().ReverseMap();
        }
    }
}
