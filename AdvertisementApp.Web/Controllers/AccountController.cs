using AdvertisementApp.Business.Services;
using AdvertisementApp.Web.Models;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AdvertisementApp.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IGenderService _genderService;
        private readonly IValidator<UserCreateModel> _userCreateValidator;

        public AccountController(IGenderService genderService, IValidator<UserCreateModel> userCreateValidator)
        {
            _genderService = genderService;
            _userCreateValidator = userCreateValidator;
        }

        public async Task<IActionResult> SignUp()
        {
            var genders = await _genderService.GetAllAsync();
            var model = new UserCreateModel();
            model.Genders = new SelectList(genders.Data,"Id","Definition");
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> SignUp(UserCreateModel model)
        {
            var result = _userCreateValidator.Validate(model);
            if (result.IsValid)
            {
                return View(model);
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
            }
            var genders = await _genderService.GetAllAsync();
            model.Genders = new SelectList(genders.Data, "Id", "Definition",model.GenderId);
            return View(model);
        }
    }
}
