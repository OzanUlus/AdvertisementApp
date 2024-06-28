using AdvertisementApp.Business.Extension;
using AdvertisementApp.Business.Services;
using AdvertisementApp.Common;
using AdvertisementApp.DataAccess.UnitOfWork;
using AdvertisementApp.Dtos;
using AdvertisementApp.Dtos.Interface;
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
        private readonly IUow _uow;
        private readonly IMapper _mapper;
        private readonly IValidator<AppUserCreateDto> _appUserValidator;
        public AppUserManager(IUow uow, IMapper mapper, IValidator<AppUserCreateDto> createValidator, IValidator<AppUserUpdateDto> updateValidator) : base(uow, mapper, createValidator, updateValidator)
        {
            _uow = uow;
            _mapper = mapper;
            _appUserValidator = createValidator;
        }
        public async Task<IResponse<AppUserCreateDto>> CreateWithRoleAsync(AppUserCreateDto appUserCreateDto,int roleId) 
        {
            var validationResult = _appUserValidator.Validate(appUserCreateDto);
            if (!validationResult.IsValid)
                return new Response<AppUserCreateDto>(appUserCreateDto, validationResult.ConvertToCustomValidationError());

            var user = _mapper.Map<AppUser>(appUserCreateDto);

            await _uow.GetRepository<AppUser>().CreateAsync(user);

            await _uow.GetRepository<AppUserRole>().CreateAsync(new AppUserRole 
            {
              AppUser = user,
              AppRoleId = roleId
            });

            await _uow.SaveChangesAsync();

            return new Response<AppUserCreateDto>(ResponseType.Success, appUserCreateDto);

        }
    }
}
