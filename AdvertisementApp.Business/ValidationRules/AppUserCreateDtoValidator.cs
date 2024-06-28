﻿using AdvertisementApp.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvertisementApp.Business.ValidationRules
{
    public class AppUserCreateDtoValidator : AbstractValidator<AppUserCreateDto>
    {
        public AppUserCreateDtoValidator()
        {
            RuleFor(au => au.FirstName).NotEmpty();
            RuleFor(au => au.Surname).NotEmpty();
            RuleFor(au => au.Username).NotEmpty();
            RuleFor(au => au.Password).NotEmpty();
            RuleFor(au => au.PhoneNumber).NotEmpty();
            RuleFor(au => au.GenderId).NotEmpty();
        }
        
    }
}
