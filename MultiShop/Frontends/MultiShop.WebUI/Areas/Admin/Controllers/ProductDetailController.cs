﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.CatalogDtos.ProductDetailDtos;
using Newtonsoft.Json;
using System.Text;

namespace MultiShop.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [AllowAnonymous]
    [Route("Admin/ProductDetail")]
    public class ProductDetailController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ProductDetailController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        [Route("UpdateProductDetail/{id}")]
        public async Task<IActionResult> UpdateProductDetail(string id)
        {
            ViewBag.V0 = "Ürün İşlemleri";
            ViewBag.V1 = "Ana Sayfa";
            ViewBag.V2 = "Ürünler";
            ViewBag.V3 = "Ürün Açıklama ve Bilgi Güncelleme Sayfası";

            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7000/api/ProductDetails/GetProductIdByProductDetailId/" + id);
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<UpdateProductDetailDto>(jsonData);
                return View(values);
            }

            return View();
        }

        [HttpPost]
        [Route("UpdateProductDetail/{id}")]
        public async Task<IActionResult> UpdateProductDetail(UpdateProductDetailDto updateProductDetailDto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(updateProductDetailDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PutAsync("https://localhost:7000/api/ProductDetails", stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("ProductListWithCategory", "Product", new { area = "Admin" });
            }

            return View();
        }
    }
}
