﻿using AdvertisementApp.Common;
using AdvertisementApp.Dtos;
using AdvertisementApp.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvertisementApp.Business.Services
{
    public interface IAppUserService : IService<AppUserCreateDto, AppUserUpdateDto,AppUserListDto,AppUser>
    {
        Task<IResponse<AppUserCreateDto>> CreateWithRoleAsync(AppUserCreateDto appUserCreateDto, int roleId);
        Task<IResponse<AppUserListDto>> CheckUserAsync(AppUserLogInDto appUserLogInDto);
        Task<IResponse<List<AppRoleListDto>>> GetRolesUserIdAsync(int userId);
    }
}
