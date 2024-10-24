using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Catalog.Dtos.SpecialOfferDtos;
using MultiShop.Catalog.Services.SpecialOfferServices;

namespace MultiShop.Catalog.Controllers
{
	[Authorize]
	[Route("api/[controller]")]
    [ApiController]
    public class SpecialOffersController : ControllerBase
    {
        private readonly ISpecialOfferService _specialOfferService;

        public SpecialOffersController(ISpecialOfferService specialOfferService)
        {
            _specialOfferService = specialOfferService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSpecialOffers()
        {
            var specialOffers = await _specialOfferService.GetAllSpecialOfferAsync();
            return Ok(specialOffers);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdSpecialOffer(string id)
        {
            var specialOffer = await _specialOfferService.GetByIdSpecialOfferAsync(id);
            return Ok(specialOffer);
        }

        [HttpPost]
        public async Task<IActionResult> CreateSpecialOffer(CreateSpecialOfferDto createSpecialOfferDto)
        {
            await _specialOfferService.CreateSpecialOfferAsync(createSpecialOfferDto);
            return Ok("Özel teklif başarıyla eklendi.");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateSpecialOffer(UpdateSpecialOfferDto updateSpecialOfferDto)
        {
            await _specialOfferService.UpdateSpecialOfferAsync(updateSpecialOfferDto);
            return Ok("Özel teklif başarıyla güncellendi.");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteSpecialOffer(string id)
        {
            await _specialOfferService.DeleteSpecialOfferAsync(id);
            return Ok("Özel teklif başarıyla silindi.");
        }
    }
}
