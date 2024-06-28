using AdvertisementApp.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvertisementApp.Business.ValidationRules
{
    public class AppUserLogInDtoValidator : AbstractValidator<AppUserLogInDto>
    {
        public AppUserLogInDtoValidator()
        {
            RuleFor(au => au.Password).NotEmpty().WithMessage("Şifre boş bırakılamaz");
            RuleFor(au => au.Username).NotEmpty().WithMessage("Kullanıcı adı boş bırakılamaz");
        }
    }
}
