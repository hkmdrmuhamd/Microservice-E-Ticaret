using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.CatalogDtos.CategoryDtos;
using Newtonsoft.Json;

namespace MultiShop.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public CategoryController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [AllowAnonymous] // Kategori listesi herkes tarafından görüntülenebilir.
        public async Task<IActionResult> Index()
        {
            ViewBag.V0 = "Kategori İşlemleri";
            ViewBag.V1 = "Ana Sayfa";
            ViewBag.V2 = "Kategoriler";
            ViewBag.V3 = "Kategori Listesi";

            var client = _httpClientFactory.CreateClient(); //Client oluşturuldu.
            var responseMessage = await client.GetAsync("https://localhost:7000/api/Categories"); //API'den veri çekildi.
            if(responseMessage.IsSuccessStatusCode) 
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync(); //Veri JSON formatına çevrildi.
                var categories = JsonConvert.DeserializeObject<List<ResultCategoryDto>>(jsonData); //JSON formatındaki veri ResultCategoryDto tipine çevrildi.
                return View(categories);
            }

            return View();
        }
    }
}
