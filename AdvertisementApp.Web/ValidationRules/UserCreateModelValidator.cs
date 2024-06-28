using AdvertisementApp.Web.Models;
using FluentValidation;

namespace AdvertisementApp.Web.ValidationRules
{
    public class UserCreateModelValidator : AbstractValidator<UserCreateModel>
    {
        public UserCreateModelValidator()
        {
            RuleFor(u => u.FirstName).NotEmpty();
            RuleFor(u => u.Surname).NotEmpty();
            RuleFor(u => u.Username).NotEmpty().MinimumLength(3);
            RuleFor(u => u.PhoneNumber).NotEmpty();
            RuleFor(u => u.Password).NotEmpty().MinimumLength(3).Equal(u => u.ConfirmPassword).WithMessage("Sifreler uğuşmuyor.");
            RuleFor(u => u.ConfirmPassword).NotEmpty();
            RuleFor(u => u.GenderId).NotEmpty();
            
        }
    }
}
