using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.CatalogDtos.CategoryDtos;
using Newtonsoft.Json;

namespace MultiShop.WebUI.ViewComponents.UILayoutViewComponents
{
    public class _NavbarUILayoutComponentPartial: ViewComponent
    {
        private readonly IHttpClientFactory _clientFactory;

        public _NavbarUILayoutComponentPartial(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _clientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7000/api/Categories");
            if (responseMessage.IsSuccessStatusCode)
            {
                var content = await responseMessage.Content.ReadAsStringAsync();
                var categorys = JsonConvert.DeserializeObject<List<ResultCategoryDto>>(content);
                return View(categorys);
            }
            return View();
        }
    }
}
