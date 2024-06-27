using AdvertisementApp.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvertisementApp.Business.ValidationRules
{
    public class ProvidedServiceUpdateDtoValidator : AbstractValidator<ProvidedServiceUpdateDto>
    {
        public ProvidedServiceUpdateDtoValidator()
        {
            RuleFor(p =>p.Id).NotEmpty();
            RuleFor(p => p.Title).NotEmpty();
            RuleFor(p => p.Description).NotEmpty();
            RuleFor(p => p.ImagePath).NotEmpty();
        }
    }
}
