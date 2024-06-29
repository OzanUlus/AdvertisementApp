using AdvertisementApp.Business.Services;
using AdvertisementApp.Dtos;
using AdvertisementApp.Entity;
using AdvertisementApp.Web.Extension;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdvertisementApp.Web.Controllers
{
    [Authorize(Roles ="Admin")]
    public class ApplicationController : Controller
    {
        private readonly IAdvertisementService _advertisementService;

        public ApplicationController(IAdvertisementService advertisementService)
        {
            _advertisementService = advertisementService;
        }

        public async Task<IActionResult> List()
        {
            var response = await _advertisementService.GetAllAsync();
            return this.ResponseView(response);
        }

        public  IActionResult Create()
        {
            
            return View(new AdvertisementCreateDto() );
        }

        [HttpPost]
        public async Task<IActionResult> Create(AdvertisementCreateDto advertisementCreateDto)
        {
            await _advertisementService.CreateAsync(advertisementCreateDto);

            return RedirectToAction("List");
        }

        public async Task<IActionResult> Delete(int advertisementId)
        {
           var response = await _advertisementService.RemoveAsync(advertisementId);

            return this.ResponseRedirectAction(response,"List");
        }

        public async Task<IActionResult> Update(int advertisementId)
        {
            var response = await _advertisementService.GetByIdAsync<AdvertisementUpdateDto>(advertisementId);
            return this.ResponseView(response);
        }

        [HttpPost]
        public async Task<IActionResult> Update(AdvertisementUpdateDto advertisementUpdateDto)
        {
          var response = await _advertisementService.UpdateAsync(advertisementUpdateDto);
            return this.ResponseRedirectAction(response, "List");
        }



    }
}
