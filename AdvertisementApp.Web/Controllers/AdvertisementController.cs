using AdvertisementApp.Business.Services;
using AdvertisementApp.Common.Enums;
using AdvertisementApp.Dtos;
using AdvertisementApp.Entity;
using AdvertisementApp.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace AdvertisementApp.Web.Controllers
{
    public class AdvertisementController : Controller
    {
        private readonly IAppUserService _userService;
        private readonly IAdvertisementAppUserService _advertisementAppUserService;

        public AdvertisementController(IAppUserService userService, IAdvertisementAppUserService advertisementAppUserService)
        {
            _userService = userService;
            _advertisementAppUserService = advertisementAppUserService;
        }

        public IActionResult Index()
        {
            return View();
        }
        [Authorize(Roles = "Member")]
        public async Task<IActionResult> Send(int advertisementId)
        {
            var userId = int.Parse((User.Claims.FirstOrDefault(u => u.Type == ClaimTypes.NameIdentifier)).Value);
            var userInfo = await _userService.GetByIdAsync<AppUserListDto>(userId);


            ViewBag.GenderId = userInfo.Data.GenderId;

            var items = Enum.GetValues(typeof(MilitaryStatusType));
            var list = new List<MilitaryStatusListDto>();
            foreach (int item in items)
            {
                list.Add(new MilitaryStatusListDto
                {
                    Id = item,
                    Definition = Enum.GetName(typeof(MilitaryStatusType), item)
                });
            }
            ViewBag.MilitaryStatus = new SelectList(list, "Id", "Definition");
            return View(new AdvertisemetAppUserCreateModel
            {
                AdvertisementId = advertisementId,
                AppUserId = userId,
            });
        }
        [HttpPost]
        [Authorize(Roles = "Member")]
        public async Task<IActionResult> Send(AdvertisemetAppUserCreateModel model)
        {
            AdvertisementAppUserCreateDto dto = new();
            if (model.CvFile != null)
            {
                var fileName = Guid.NewGuid().ToString();
                var extName = Path.GetExtension(model.CvFile.FileName);
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot","cvFiles",fileName+extName);
                var stream = new FileStream(path,FileMode.Create);
                await model.CvFile.CopyToAsync(stream);
                dto.CvPath = path;
            }

            dto.AppUserId = model.AppUserId;
            dto.AdvertisementAppUserStatusId = model.AdvertisementAppUserStatusId;
            dto.AdvertisementId = model.AdvertisementId;
            dto.EndDate = model.EndDate;
            dto.WorkExperience = model.WorkExperience;
            dto.MilitaryStatusId = model.MilitaryStatusId;

            var response = await _advertisementAppUserService.CreateAsync(dto);
            if (response.ResponseType == Common.ResponseType.ValidationError)
            {
                foreach (var error in response.ValidationErrors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
                var userId = int.Parse((User.Claims.FirstOrDefault(u => u.Type == ClaimTypes.NameIdentifier)).Value);
                var userInfo = await _userService.GetByIdAsync<AppUserListDto>(userId);

                ViewBag.GenderId = userInfo.Data.GenderId;

                var items = Enum.GetValues(typeof(MilitaryStatusType));
                var list = new List<MilitaryStatusListDto>();
                foreach (int item in items)
                {
                    list.Add(new MilitaryStatusListDto
                    {
                        Id = item,
                        Definition = Enum.GetName(typeof(MilitaryStatusType), item)
                    });
                }
                ViewBag.MilitaryStatus = new SelectList(list, "Id", "Definition");
               
                return View(model);
            }
            else return RedirectToAction("HumanResource","Home");
           
        }
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> List()
        {
            var list = await _advertisementAppUserService.GetList(AdvertisementAppUserStatusType.Başvurdu);
            return View(list);
        }
    }
}
