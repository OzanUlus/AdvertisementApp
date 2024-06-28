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
    public class AppUserManager : BaseManager<AppUserCreateDto, AppUserUpdateDto, AppUserListDto, AppUser>, IAppUserService
    {
        public AppUserManager(IUow uow, IMapper mapper, IValidator<AppUserCreateDto> createValidator, IValidator<AppUserUpdateDto> updateValidator) : base(uow, mapper, createValidator, updateValidator)
        {
        }
    }
}
