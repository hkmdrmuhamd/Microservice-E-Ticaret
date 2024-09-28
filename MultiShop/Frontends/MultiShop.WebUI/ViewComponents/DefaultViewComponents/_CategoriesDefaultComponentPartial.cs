using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.CatalogDtos.CategoryDtos;
using Newtonsoft.Json;

namespace MultiShop.WebUI.ViewComponents.DefaultViewComponents
{
    public class _CategoriesDefaultComponentPartial : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public _CategoriesDefaultComponentPartial(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient(); //Client oluşturuldu.
            var responseMessage = await client.GetAsync("https://localhost:7000/api/Categories"); //API'den veri çekildi.
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync(); //Veri JSON formatına çevrildi.
                var categories = JsonConvert.DeserializeObject<List<ResultCategoryDto>>(jsonData); //JSON formatındaki veri ResultCategoryDto tipine çevrildi.
                return View(categories);
            }
            return View();
        }
    }
}
