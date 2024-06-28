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
    public class GenderManager : BaseManager<GenderCreateDto, GenderUpdateDto, GenderListDto, Gender>, IGenderService
    {
        public GenderManager(IUow uow, IMapper mapper, IValidator<GenderCreateDto> createValidator, IValidator<GenderUpdateDto> updateValidator) : base(uow, mapper, createValidator, updateValidator)
        {
        }
    }
}
