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
        private readonly IValidator<AppUserLogInDto> _appUserLogInValidator;
        public AppUserManager(IUow uow, IMapper mapper, IValidator<AppUserCreateDto> createValidator, IValidator<AppUserUpdateDto> updateValidator, IValidator<AppUserLogInDto> appUserLogInValidator) : base(uow, mapper, createValidator, updateValidator)
        {
            _uow = uow;
            _mapper = mapper;
            _appUserValidator = createValidator;
            _appUserLogInValidator = appUserLogInValidator;
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
        public async Task<IResponse<AppUserListDto>> CheckUserAsync(AppUserLogInDto appUserLogInDto)
        {
            var loginResult = _appUserLogInValidator.Validate(appUserLogInDto);
            if (!loginResult.IsValid) 
                return new Response<AppUserListDto>(ResponseType.ValidationError, "Kullanıcı adı ve şifre boş bırakılamaz");

            var user = await _uow.GetRepository<AppUser>().GetByFilterAsync(au => au.Username == appUserLogInDto.Username && au.Password == appUserLogInDto.Password);
            if (user == null) return new Response<AppUserListDto>(ResponseType.NotFoubd, "Kullanıcı adı veya şifre hatalı");

            var appUserDto = _mapper.Map<AppUserListDto>(user);

            return new Response<AppUserListDto>(ResponseType.Success,appUserDto);
            

        }
        public async Task<IResponse<List<AppRoleListDto>>> GetRolesUserIdAsync(int userId)
        {
            var roles = await _uow.GetRepository<AppRole>().GetAllAsync(ap => ap.AppUserRoles.Any(ap => ap.AppUserId == userId));
            if (roles == null) return new Response<List<AppRoleListDto>>(ResponseType.NotFoubd, "role bulanamadı");

            var dto = _mapper.Map<List<AppRoleListDto>>(roles);
            return new Response<List<AppRoleListDto>>(ResponseType.Success, dto);
        }
    }
}
