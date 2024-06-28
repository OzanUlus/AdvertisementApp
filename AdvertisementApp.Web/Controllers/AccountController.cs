using AdvertisementApp.Business.Services;
using AdvertisementApp.Common.Enums;
using AdvertisementApp.Dtos;
using AdvertisementApp.Web.Extension;
using AdvertisementApp.Web.Models;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AdvertisementApp.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IGenderService _genderService;
        private readonly IValidator<UserCreateModel> _userCreateValidator;
        private readonly IAppUserService _appUserService;
        private readonly IMapper _mapper;

        public AccountController(IGenderService genderService, IValidator<UserCreateModel> userCreateValidator, IAppUserService appUserService, IMapper mapper)
        {
            _genderService = genderService;
            _userCreateValidator = userCreateValidator;
            _appUserService = appUserService;
            _mapper = mapper;
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
                var dto = _mapper.Map<AppUserCreateDto>(model);
               var createResponse = await _appUserService.CreateWithRoleAsync(dto,(int)RoleType.Member);
                return this.ResponseRedirectAction(createResponse, "SignIn");
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
