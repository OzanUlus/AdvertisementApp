using AdvertisementApp.Business.Services;
using AdvertisementApp.DataAccess.UnitOfWork;
using AdvertisementApp.Dtos;
using AdvertisementApp.Entity;
using AutoMapper;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvertisementApp.Business.Managers
{
    public class ProvidedServiceManager : BaseManager<ProvidedServiceCreateDto, ProvidedServiceUpdateDto, ProvidedServiceListDto, ProvidedService>,IProvidedServiceService
    {
        public ProvidedServiceManager(IUow uow, IMapper mapper, IValidator<ProvidedServiceCreateDto> createValidator, IValidator<ProvidedServiceUpdateDto> updateValidator) : base(uow, mapper, createValidator, updateValidator)
        {
        }
    }
}
