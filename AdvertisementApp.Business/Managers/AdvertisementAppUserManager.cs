using AdvertisementApp.Business.Extension;
using AdvertisementApp.Business.Services;
using AdvertisementApp.Common;
using AdvertisementApp.Common.Enums;
using AdvertisementApp.DataAccess.UnitOfWork;
using AdvertisementApp.Dtos;
using AdvertisementApp.Entity;
using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvertisementApp.Business.Managers
{
    public class AdvertisementAppUserManager : IAdvertisementAppUserService
    {
        private readonly IUow _uow;
        private readonly IValidator<AdvertisementAppUserCreateDto> _validator;
        private readonly IMapper _mapper;

        public AdvertisementAppUserManager(IUow uow, IValidator<AdvertisementAppUserCreateDto> validator, IMapper mapper)
        {
            _uow = uow;
            _validator = validator;
            _mapper = mapper;
        }

        public async Task<IResponse<AdvertisementAppUserCreateDto>> CreateAsync(AdvertisementAppUserCreateDto advertisementAppUserCreateDto)
        {
            var result = _validator.Validate(advertisementAppUserCreateDto);
            if (!result.IsValid)
                return new Response<AdvertisementAppUserCreateDto>(advertisementAppUserCreateDto, result.ConvertToCustomValidationError());

            var control = await _uow.GetRepository<AdvertisementAppUser>().GetByFilterAsync(a => a.AppUserId == advertisementAppUserCreateDto.AppUserId && a.AdvertisementId == advertisementAppUserCreateDto.AdvertisementId);

            if (control == null)
            {
                var createdAdvertisementAppUser = _mapper.Map<AdvertisementAppUser>(advertisementAppUserCreateDto);
                await _uow.GetRepository<AdvertisementAppUser>().CreateAsync(createdAdvertisementAppUser);
                await _uow.SaveChangesAsync();
                return new Response<AdvertisementAppUserCreateDto>(ResponseType.Success, advertisementAppUserCreateDto);
            }

            List<CustomValidationError> errors = new List<CustomValidationError> { new CustomValidationError { PropertyName = "", ErrorMessage = "Daha önce başvuru yapılan işe başvuru yapamazsınız" } };
            return new Response<AdvertisementAppUserCreateDto>(advertisementAppUserCreateDto, errors);
        }

        public async Task<List<AdvertisementAppUserListDto>> GetList(AdvertisementAppUserStatusType statusType)
        {
            var query = _uow.GetRepository<AdvertisementAppUser>().GetQueryable();

            var list = await query.Include(a => a.Advertisement).Include(a => a.AdvertisementAppUserStatus).Include(a => a.MilitaryStatus).Include(a => a.AppUser).ThenInclude(au => au.Gender).Where(a => a.AdvertisementAppUserStatusId == (int)statusType).ToListAsync();

            return _mapper.Map<List<AdvertisementAppUserListDto>>(list);
        }
        public async Task SetStatus(int advertisementAppUserId, AdvertisementAppUserStatusType type)
        {
            var query = _uow.GetRepository<AdvertisementAppUser>().GetQueryable();
            var entity = await query.SingleOrDefaultAsync(a => a.Id == advertisementAppUserId);
            entity.AdvertisementAppUserStatusId = (int)type;
            await _uow.SaveChangesAsync();

        }
    }
}
