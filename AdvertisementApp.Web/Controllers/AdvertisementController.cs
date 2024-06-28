using AdvertisementApp.Business.Services;
using AdvertisementApp.Common.Enums;
using AdvertisementApp.Dtos;
using AdvertisementApp.Entity;
using AdvertisementApp.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Routing;
using System.Security.Claims;
using AdvertisementApp.Web.Models;

namespace AdvertisementApp.Web.Controllers
{
    public class AdvertisementController : Controller
    {
        private readonly IAppUserService _userService;

        public AdvertisementController(IAppUserService userService)
        {
            _userService = userService;
        }

        public IActionResult Index()
        {
            return View();
        }
        [Authorize(Roles ="Member")]
        public async Task<IActionResult> Send(int advertisementId)
        {
           var userId = int.Parse((User.Claims.FirstOrDefault(u => u.Type == ClaimTypes.NameIdentifier)).Value);
            var userInfo = await _userService.GetByIdAsync<AppUserListDto>(userId);
           

            ViewBag.GenderId = userInfo.Data.GenderId;

            var items = Enum.GetValues(typeof(MilitaryStatusType));
            var list = new List<MilitaryStatusListDto>();
            foreach (int item in items)
            {
                list.Add(new MilitaryStatusListDto {
                 Id = item,
                 Definition = Enum.GetName(typeof(MilitaryStatusType),item)
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
          return View();
        }
    }
}
