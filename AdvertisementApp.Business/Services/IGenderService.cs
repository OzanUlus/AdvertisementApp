﻿using AdvertisementApp.Dtos;
using AdvertisementApp.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvertisementApp.Business.Services
{
    public interface IGenderService : IService<GenderCreateDto,GenderUpdateDto,GenderListDto,Gender>
    {
    }
}
