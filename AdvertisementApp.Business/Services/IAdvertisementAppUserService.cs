using AdvertisementApp.Common;
using AdvertisementApp.Common.Enums;
using AdvertisementApp.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvertisementApp.Business.Services
{
    public interface IAdvertisementAppUserService
    {
        Task<IResponse<AdvertisementAppUserCreateDto>> CreateAsync(AdvertisementAppUserCreateDto advertisementAppUserCreateDto);
        Task<List<AdvertisementAppUserListDto>> GetList(AdvertisementAppUserStatusType statusType);
    }
}
