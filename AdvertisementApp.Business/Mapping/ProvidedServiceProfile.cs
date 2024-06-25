using AdvertisementApp.Dtos.ProvidedService;
using AdvertisementApp.Entity;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvertisementApp.Business.Mapping
{
    public class ProvidedServiceProfile : Profile
    {
        public ProvidedServiceProfile()
        {
            CreateMap<ProvidedService , ProvidedServiceCreateDto> ().ReverseMap();
            CreateMap<ProvidedService , ProvidedServiceUpdateDto> ().ReverseMap();
            CreateMap<ProvidedService , ProvidedServiceListDto> ().ReverseMap();
        }
    }
}
