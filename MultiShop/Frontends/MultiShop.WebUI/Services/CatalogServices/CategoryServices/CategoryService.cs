using MultiShop.DtoLayer.CatalogDtos.CategoryDtos;

namespace MultiShop.WebUI.Services.CatalogServices.CategoryServices
{
    public class CategoryService : ICategoryService
    {
        private readonly HttpClient _httpClient;

        public CategoryService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task CreateCategoryAsync(CreateCategoryDto createCategoryDto)
        {
            //var responseMessage = await client.GetAsync("https://localhost:7000/api/Categories"); bu işlemi aşağıdaki gibi daha kısa bir şekilde yapabiliriz.
            await _httpClient.PostAsJsonAsync<CreateCategoryDto>("categories", createCategoryDto);//PostAsJsonAsync methodunu kullanarak Json dönşümlerini arka planda yaparız
        }

        public async Task DeleteCategoryAsync(string id)
        {
            await _httpClient.DeleteAsync("categories?id=" + id);
        }

        public async Task<GetByIdCategoryDto> GetByIdCategoryAsync(string id)
        {
            var responseMessage = await _httpClient.GetAsync("categories/" + id);
            var value = await responseMessage.Content.ReadFromJsonAsync<GetByIdCategoryDto>();
            return value;
        }

        public async Task<List<ResultCategoryDto>> GettAllCategoryAsync()
        {
            var responseMessage = await _httpClient.GetAsync("categories");
            var values = await responseMessage.Content.ReadFromJsonAsync<List<ResultCategoryDto>>();
            return values;

        }

        public async Task UpdateCategoryAsync(UpdateCategoryDto updateCategoryDto)
        {
            await _httpClient.PutAsJsonAsync<UpdateCategoryDto>("categories", updateCategoryDto);
        }
    }
}
