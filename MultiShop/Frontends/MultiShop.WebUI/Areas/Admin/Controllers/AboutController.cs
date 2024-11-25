using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.CatalogDtos.AboutDtos;
using MultiShop.WebUI.Services.CatalogServices.AboutServices;

namespace MultiShop.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/About")]
    public class AboutController : Controller
    {
        private readonly IAboutService _aboutService;

        public AboutController(IAboutService aboutService)
        {
            _aboutService = aboutService;
        }

        [Route("Index")]
        public async Task<IActionResult> Index()
        {
            AboutViewBagList();
            var values = await _aboutService.GetAllAboutAsync();
            return View(values);
        }

        [Authorize]
        [HttpGet]
        [Route("CreateAbout")]
        public IActionResult CreateAbout()
        {
            AboutViewBagList();
            return View();
        }

        [Authorize]
        [HttpPost]
        [Route("CreateAbout")]
        public async Task<IActionResult> CreateAbout(CreateAboutDto createAboutDto)
        {
            await _aboutService.CreateAboutAsync(createAboutDto);
            return RedirectToAction("Index", "About", new { area = "Admin" });
        }

        [Authorize]
        [HttpGet]
        [Route("UpdateAbout/{id}")]
        public async Task<IActionResult> UpdateAbout(string id)
        {
            AboutViewBagList();
            var value = await _aboutService.GetByIdAboutAsync(id);
            var updateAboutDto = new UpdateAboutDto
            {
                AboutId = value.AboutId,
                Address = value.Address,
                Description = value.Description,
                Email = value.Email,
                Phone = value.Phone
            };
            return View(updateAboutDto);
        }

        [Authorize]
        [HttpPost]
        [Route("UpdateAbout/{id}")]
        public async Task<IActionResult> UpdateAbout(UpdateAboutDto updateAboutDto)
        {
            await _aboutService.UpdateAboutAsync(updateAboutDto);
            return RedirectToAction("Index", "About", new { area = "Admin" });
        }

        [Authorize]
        [Route("DeleteAbout/{id}")]
        public async Task<IActionResult> DeleteAbout(string id)
        {
            await _aboutService.DeleteAboutAsync(id);
            return RedirectToAction("Index", "About", new { area = "Admin" });
        }

        void AboutViewBagList()
        {
            ViewBag.V0 = "Hakkımızda İşlemleri";
            ViewBag.V1 = "Ana Sayfa";
            ViewBag.V2 = "Hakkımızda Alanı";
            ViewBag.V3 = "Hakkımızda Biligileri Listesi";
        }
    }
}
