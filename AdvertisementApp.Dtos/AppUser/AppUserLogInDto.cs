﻿using AdvertisementApp.Dtos.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvertisementApp.Dtos
{
    public class AppUserLogInDto : IDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public bool RenemberMe { get; set; }
    }
}
