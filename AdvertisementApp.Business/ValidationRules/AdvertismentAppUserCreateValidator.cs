using AdvertisementApp.Common.Enums;
using AdvertisementApp.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvertisementApp.Business.ValidationRules
{
    public class AdvertismentAppUserCreateValidator : AbstractValidator<AdvertisementAppUserCreateDto>
    {
        public AdvertismentAppUserCreateValidator()
        {
            RuleFor(a => a.AppUserId).NotEmpty();
            RuleFor(a => a.AdvertisementAppUserStatusId).NotEmpty();
            RuleFor(a => a.AdvertisementId).NotEmpty();
           
            RuleFor(a => a.CvFile).NotEmpty().WithMessage("Bir dosya şeçiniz");
            RuleFor(a => a.MilitaryStatusId).NotEmpty();
            RuleFor(a => a.EndDate).NotEmpty().When(a => a.MilitaryStatusId == (int)MilitaryStatusType.Tecilli).WithMessage("Tecil tarihi boş olamaz.");
           
        }
    }
}
