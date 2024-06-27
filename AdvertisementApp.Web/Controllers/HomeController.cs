using AdvertisementApp.Business.Services;
using AdvertisementApp.Web.Extension;
using AdvertisementApp.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AdvertisementApp.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProvidedServiceService _service;

        public HomeController(ILogger<HomeController> logger, IProvidedServiceService service)
        {
            _logger = logger;
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var datas = await _service.GetAllAsync();
            return this.ResponseView(datas);
        }
        public async Task<IActionResult> HumanResource()
        {
            return View();
        }


    }
}
