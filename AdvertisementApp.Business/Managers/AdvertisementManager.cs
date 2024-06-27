using AdvertisementApp.Business.Services;
using AdvertisementApp.Common;
using AdvertisementApp.DataAccess.Configurations;
using AdvertisementApp.DataAccess.Context;
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
    public class AdvertisementManager : BaseManager<AdvertisementCreateDto, AdvertisementUpdateDto, AdvertisementListDto, Advertisement>, IAdvertisementService
    {
        private readonly IUow _uow;
        private readonly IMapper _mapper;
        public AdvertisementManager(IUow uow, IMapper mapper, IValidator<AdvertisementCreateDto> createValidator, IValidator<AdvertisementUpdateDto> updateValidator, AdvertisementContext context) : base(uow, mapper, createValidator, updateValidator)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<IResponse<List<AdvertisementListDto>>> GetActiveAsync()
        {
           var activeData = await _uow.GetRepository<Advertisement>().GetAllAsync(a => a.Status , a => a.CreatedDate, Common.Enums.OrderByType.DESC);
            var dtoActive = _mapper.Map<List<AdvertisementListDto>>(activeData);
            return new Response<List<AdvertisementListDto>>(ResponseType.Success,dtoActive);
           
            
        }
    }
}
