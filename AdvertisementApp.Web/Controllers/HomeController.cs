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

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
