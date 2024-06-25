using AdvertisementApp.Common;
using AdvertisementApp.Dtos.Interface;
using AdvertisementApp.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvertisementApp.Business.Services
{
    public interface IService<CreateDto , UpdateDto, ListDto,T>
        where CreateDto : class,IDto,new()
        where UpdateDto : class,IUpdateDto,new()
        where ListDto : class,IDto,new()
        where T :BaseEntity,new()
    {
        Task<IResponse<CreateDto>> CreateAsync(CreateDto dto);
        Task<IResponse<UpdateDto>> UpdateAsync(UpdateDto dto);
        Task<IResponse<IDto>> GetByIdAsync<IDto>(int id);
        Task<IResponse<List<ListDto>>> GetAllAsync();
        Task<IResponse> RemoveAsync(int id);
    }
}
