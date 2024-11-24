using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.CatalogDtos.OfferDiscountDtos;
using MultiShop.WebUI.Services.CatalogServices.OfferDiscountServices;

namespace MultiShop.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/OfferDiscount")]
    public class OfferDiscountController : Controller
    {
        private readonly IOfferDiscountService _offerDiscountService;
        public OfferDiscountController(IOfferDiscountService offerDiscountService)
        {
            _offerDiscountService = offerDiscountService;
        }

        [Route("Index")]
        public async Task<IActionResult> Index()
        {
            OfferDiscountViewBagList();
            var values = await _offerDiscountService.GetAllOfferDiscountAsync();
            return View(values);
        }

        [Authorize]
        [HttpGet]
        [Route("CreateOfferDiscount")]
        public IActionResult CreateOfferDiscount()
        {
            OfferDiscountViewBagList();
            return View();
        }

        [Authorize]
        [HttpPost]
        [Route("CreateOfferDiscount")]
        public async Task<IActionResult> CreateOfferDiscount(CreateOfferDiscountDto createOfferDiscountDto)
        {
            await _offerDiscountService.CreateOfferDiscountAsync(createOfferDiscountDto);
            return RedirectToAction("Index", "OfferDiscount", new { area = "Admin" });
        }

        [Authorize]
        [HttpGet]
        [Route("UpdateOfferDiscount/{id}")]
        public async Task<IActionResult> UpdateOfferDiscount(string id)
        {
            OfferDiscountViewBagList();
            var value = await _offerDiscountService.GetByIdOfferDiscountAsync(id);
            var updateOfferDiscountDto = new UpdateOfferDiscountDto
            {
                OfferDiscountId = value.OfferDiscountId,
                Title = value.Title,
                SubTitle = value.SubTitle,
                ImageUrl = value.ImageUrl
            };
            return View(updateOfferDiscountDto);
        }

        [Authorize]
        [HttpPost]
        [Route("UpdateOfferDiscount/{id}")]
        public async Task<IActionResult> UpdateOfferDiscount(UpdateOfferDiscountDto updateOfferDiscountDto)
        {
            await _offerDiscountService.UpdateOfferDiscountAsync(updateOfferDiscountDto);
            return RedirectToAction("Index", "OfferDiscount", new { area = "Admin" });
        }

        [Authorize]
        [Route("DeleteOfferDiscount/{id}")]
        public async Task<IActionResult> DeleteOfferDiscount(string id)
        {
            await _offerDiscountService.DeleteOfferDiscountAsync(id);
            return RedirectToAction("Index", "OfferDiscount", new { area = "Admin" });
        }

        void OfferDiscountViewBagList()
        {
            ViewBag.V0 = "İndirim Teklifi İşlemleri";
            ViewBag.V1 = "Ana Sayfa";
            ViewBag.V2 = "İndirim Teklifleri";
            ViewBag.V3 = "İndirim Teklif Listesi";
        }
    }
}
