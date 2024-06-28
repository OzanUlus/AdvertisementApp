using AdvertisementApp.Business.Services;
using AdvertisementApp.Common;
using AdvertisementApp.Common.Enums;
using AdvertisementApp.Dtos;
using AdvertisementApp.Web.Extension;
using AdvertisementApp.Web.Models;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace AdvertisementApp.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IGenderService _genderService;
        private readonly IValidator<UserCreateModel> _userCreateValidator;
        private readonly IAppUserService _appUserService;
        private readonly IMapper _mapper;

        public AccountController(IGenderService genderService, IValidator<UserCreateModel> userCreateValidator, IAppUserService appUserService, IMapper mapper, IValidator<AppUserLogInDto> appUserLoginClidator)
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
            model.Genders = new SelectList(genders.Data, "Id", "Definition");
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> SignUp(UserCreateModel model)
        {
            var result = _userCreateValidator.Validate(model);
            if (result.IsValid)
            {
                var dto = _mapper.Map<AppUserCreateDto>(model);
                var createResponse = await _appUserService.CreateWithRoleAsync(dto, (int)RoleType.Member);
                return this.ResponseRedirectAction(createResponse, "SignIn");
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
            }
            var genders = await _genderService.GetAllAsync();
            model.Genders = new SelectList(genders.Data, "Id", "Definition", model.GenderId);
            return View(model);
        }
        public IActionResult SignIn()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignIn(AppUserLogInDto appUserLogInDto)
        {
            var result = await _appUserService.CheckUserAsync(appUserLogInDto);
            if (result.ResponseType == ResponseType.Success)
            {
                var roleResult = await _appUserService.GetRolesUserIdAsync(result.Data.Id);
                //ilgili kullanıcının rollerini çekmemiz gerekiyor
                var claims = new List<Claim>();

                if (roleResult.ResponseType == ResponseType.Success)
                {
                    foreach (var role in roleResult.Data)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, role.Definition));
                    }
                }

                claims.Add(new Claim(ClaimTypes.NameIdentifier, result.Data.Id.ToString()));

                var claimsIdentity = new ClaimsIdentity(
                    claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties
                {

                    IsPersistent = appUserLogInDto.RenemberMe,

                };

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);
                return RedirectToAction("Index", "Home");
            }
            ModelState.AddModelError("Kullanıcı adı veya şifre hatalı", result.Message);

            return View(appUserLogInDto);
        }
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");

        }
    }
}
