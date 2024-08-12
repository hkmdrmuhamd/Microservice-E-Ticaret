using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Catalog.Dtos.ProductDetailDtos;
using MultiShop.Catalog.Services.ProductDetailServices;

namespace MultiShop.Catalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductDetailsController : ControllerBase
    {
        private readonly IProductDetailService _productDetailsService;

        public ProductDetailsController(IProductDetailService productDetailsService)
        {
            _productDetailsService = productDetailsService;
        }

        [HttpGet]
        public async Task<IActionResult> ProductDetailsList()
        {
            var values = await _productDetailsService.GettAllProductDetailAsync();
            return Ok(values);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductDetailsById(string id)
        {
            var values = await _productDetailsService.GetByIdProductDetailAsync(id);
            return Ok(values);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProductDetails(CreateProductDetailDto createProductDetailsDto)
        {
            await _productDetailsService.CreateProductDetailAsync(createProductDetailsDto);
            return Ok("Ürün detayı başarıyla eklendi.");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProductDetails(UpdateProductDetailDto updateProductDetailsDto)
        {
            await _productDetailsService.UpdateProductDetailAsync(updateProductDetailsDto);
            return Ok("Ürün detayı başarıyla güncellendi.");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteProductDetails(string id)
        {
            await _productDetailsService.DeleteProductDetailAsync(id);
            return Ok("Ürün detayı başarıyla silindi.");
        }
    }
}
