using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MultiShop.DtoLayer.CatalogDtos.CategoryDtos;
using MultiShop.DtoLayer.CatalogDtos.ProductDtos;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;

namespace MultiShop.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [AllowAnonymous]
    [Route("Admin/Product")]
    public class ProductController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ProductController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [Route("Index")]
        public async Task<IActionResult> Index()
        {
            ViewBag.V0 = "Ürün İşlemleri";
            ViewBag.V1 = "Ana Sayfa";
            ViewBag.V2 = "Ürünler";
            ViewBag.V3 = "Ürün Listesi";

            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7000/api/Products");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var categories = JsonConvert.DeserializeObject<List<ResultProductDto>>(jsonData);
                return View(categories);
            }

            return View();
        }

        [HttpGet]
        [Route("CreateProduct")]
        public async Task<IActionResult> CreateProduct()
        {
            ViewBag.V0 = "Ürün İşlemleri";
            ViewBag.V1 = "Ana Sayfa";
            ViewBag.V2 = "Ürünler";
            ViewBag.V3 = "Ürün Listesi";

            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7000/api/Categories");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultCategoryDto>>(jsonData);
                List<SelectListItem> selectListItems = (from x in values
                                                        select new SelectListItem
                                                        {
                                                            Text = x.CategoryName,
                                                            Value = x.CategoryId
                                                        }).ToList();
                ViewBag.Categories = selectListItems;
            }
            return View();
        }

        [HttpPost]
        [Route("CreateProduct")]
        public async Task<IActionResult> CreateProduct(CreateProductDto createProductDto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(createProductDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json"); //Veri JSON formatına çevrildi.
            var responseMessage = await client.PostAsync("https://localhost:7000/api/Products", stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Product", new { area = "Admin" });
            }

            return View();
        }

        [HttpGet]
        [Route("UpdateProduct/{id}")]
        public async Task<IActionResult> UpdateProduct(string id)
        {
            ViewBag.V0 = "Ürün İşlemleri";
            ViewBag.V1 = "Ana Sayfa";
            ViewBag.V2 = "Ürünler";
            ViewBag.V3 = "Ürün Güncelleme Sayfası";

            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7000/api/Products/" + id);

            var clientCategories = _httpClientFactory.CreateClient();
            var responseMessageCategories = await clientCategories.GetAsync("https://localhost:7000/api/Categories");
            
            if (responseMessage.IsSuccessStatusCode && responseMessageCategories.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<UpdateProductDto>(jsonData);

                var jsonDataCategories = await responseMessageCategories.Content.ReadAsStringAsync();
                var valuesCategories = JsonConvert.DeserializeObject<List<ResultCategoryDto>>(jsonDataCategories);
                List<SelectListItem> selectListItems = (from x in valuesCategories
                                                        select new SelectListItem
                                                        {
                                                            Text = x.CategoryName,
                                                            Value = x.CategoryId
                                                        }).ToList();
                ViewBag.Products = selectListItems;
                return View(values);
            }

            return View();
        }

        [HttpPost]
        [Route("UpdateProduct/{id}")]
        public async Task<IActionResult> UpdateProduct(UpdateProductDto updateProductDto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(updateProductDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PutAsync("https://localhost:7000/api/Products", stringContent);

            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Product", new { area = "Admin" });
            }

            return View();
        }

        [Route("DeleteProduct/{id}")]
        public async Task<IActionResult> DeleteProduct(string id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.DeleteAsync("https://localhost:7000/api/Products?id=" + id);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Product", new { area = "Admin" });
            }

            return View();
        }

        [HttpGet]
        [Route("ProductListWithCategory")]
        public async Task<IActionResult> ProductListWithCategory()
        {
            ViewBag.V0 = "Ürün İşlemleri";
            ViewBag.V1 = "Ana Sayfa";
            ViewBag.V2 = "Ürünler";
            ViewBag.V3 = "Kategoriye Göre Ürün Listesi";

            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7000/api/Products/ProductListWithCategory");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultProductWithCategoryDto>>(jsonData);
                return View(values);
            }

            return View();
        }
    }
}
