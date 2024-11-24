using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.CatalogDtos.FeatureDtos;
using MultiShop.WebUI.Services.CatalogServices.FeatureServices;

namespace MultiShop.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/Feature")]
    public class FeatureController : Controller
    {
        private readonly IFeatureService _featureService;

        public FeatureController(IFeatureService featureService)
        {
            _featureService = featureService;
        }

        [Route("Index")]
        public async Task<IActionResult> Index()
        {
            FeatureViewBagList();
            var values = await _featureService.GetAllFeaturesAsync();
            return View(values);
        }

        [Authorize]
        [HttpGet]
        [Route("CreateFeature")]
        public IActionResult CreateFeature()
        {
            FeatureViewBagList();
            return View();
        }

        [Authorize]
        [HttpPost]
        [Route("CreateFeature")]
        public async Task<IActionResult> CreateFeature(CreateFeatureDto createFeatureDto)
        {
            await _featureService.CreateFeatureAsync(createFeatureDto);
            return RedirectToAction("Index", "Feature", new { area = "Admin" });
        }

        [Authorize]
        [HttpGet]
        [Route("UpdateFeature/{id}")]
        public async Task<IActionResult> UpdateFeature(string id)
        {
            FeatureViewBagList();
            var value = await _featureService.GetByIdFeatureAsync(id);
            var updateFeatureDto = new UpdateFeatureDto
            {
                FeatureId = value.FeatureId,
                Title = value.Title,
                Icon = value.Icon
            };
            return View(updateFeatureDto);
        }

        [Authorize]
        [HttpPost]
        [Route("UpdateFeature/{id}")]
        public async Task<IActionResult> UpdateFeature(UpdateFeatureDto updateFeatureDto)
        {
            await _featureService.UpdateFeatureAsync(updateFeatureDto);
            return RedirectToAction("Index", "Feature", new { area = "Admin" });
        }

        [Authorize]
        [Route("DeleteFeature/{id}")]
        public async Task<IActionResult> DeleteFeature(string id)
        {
            await _featureService.DeleteFeatureAsync(id);
            return RedirectToAction("Index", "Feature", new { area = "Admin" });
        }

        void FeatureViewBagList()
        {
            ViewBag.V0 = "Ana Sayfa Öne Çıkan Alan İşlemleri";
            ViewBag.V1 = "Ana Sayfa";
            ViewBag.V2 = "Öne Çıkan Alanlar";
            ViewBag.V3 = "Öne Çıkan Alan Listesi";
        }
    }
}
