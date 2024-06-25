using AdvertisementApp.Business.Extension;
using AdvertisementApp.Business.Services;
using AdvertisementApp.Common;
using AdvertisementApp.DataAccess.Repositories;
using AdvertisementApp.DataAccess.UnitOfWork;
using AdvertisementApp.Dtos.Interface;
using AdvertisementApp.Entity;
using AutoMapper;

using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvertisementApp.Business.Managers
{
    public class BaseManager<CreateDto, UpdateDto, ListDto,T> : IService<CreateDto, UpdateDto, ListDto,T>
         where CreateDto : class, IDto, new()
        where UpdateDto : class, IUpdateDto, new()
        where ListDto : class, IDto, new()
        where T : BaseEntity,new()
    {
        private readonly IUow _uow;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateDto> _createValidator;
        private readonly IValidator<UpdateDto> _updateValidator;

        public BaseManager(IUow uow, IMapper mapper, IValidator<CreateDto> createValidator, IValidator<UpdateDto> updateValidator)
        {
            _uow = uow;
            _mapper = mapper;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }

        public async Task<IResponse<CreateDto>> CreateAsync(CreateDto dto)
        {
           var result = _createValidator.Validate(dto);
            if(!result.IsValid) return new Response<CreateDto>(dto,result.ConvertToCustomValidationError());

            var entity = _mapper.Map<T>(dto);
            await _uow.GetRepository<T>().CreateAsync(entity);
            await _uow.SaveChangesAsync();
            return new Response<CreateDto>(ResponseType.Success , dto);
        }

        public async Task<IResponse<List<ListDto>>> GetAllAsync()
        {
            var datas = await _uow.GetRepository<T>().GetAllAsync();
            var dto = _mapper.Map<List<ListDto>>(datas);
            return new Response<List<ListDto>>(ResponseType.Success,dto);
            
        }

        public async Task<IResponse<IDto>> GetByIdAsync<IDto>(int id)
        {
            var data = await _uow.GetRepository<T>().GetByFilterAsync(x => x.Id == id);
            if(data == null) return new Response<IDto>(ResponseType.NotFoubd,$"{id} ye ssahip data bulunamadı");
             var dto = _mapper.Map<IDto>(data);
            return new Response<IDto>(ResponseType.Success, dto);
        }

        public async Task<IResponse> RemoveAsync(int id)
        {
            var data = await _uow.GetRepository<T>().FindAsync(id);
            if (data == null) return new Response(ResponseType.NotFoubd, $"{id} ye ssahip data bulunamadı");
            _uow.GetRepository<T>().Remove(data);
            await _uow.SaveChangesAsync();
            return new Response(ResponseType.Success);
        }

        public async Task<IResponse<UpdateDto>> UpdateAsync(UpdateDto dto)
        {
             var result =_updateValidator.Validate(dto);
            if(!result.IsValid) return new Response<UpdateDto>(dto, result.ConvertToCustomValidationError());
           
            var unchangedData = await _uow.GetRepository<T>().FindAsync(dto.Id);
            if (unchangedData == null) return new Response<UpdateDto>(ResponseType.NotFoubd, $"{dto.Id} ye ssahip data bulunamadı");

            var entity = _mapper.Map<T>(dto);
            _uow.GetRepository<T>().Update(entity,unchangedData);
            await _uow.SaveChangesAsync();
            return new Response<UpdateDto>(ResponseType.Success, dto);
           
        }
    }
}
