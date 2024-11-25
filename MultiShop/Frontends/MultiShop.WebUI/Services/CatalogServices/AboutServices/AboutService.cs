﻿using MultiShop.DtoLayer.CatalogDtos.AboutDtos;

namespace MultiShop.WebUI.Services.CatalogServices.AboutServices
{
    public class AboutService : IAboutService
    {
        private readonly HttpClient _httpClient;

        public AboutService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task CreateAboutAsync(CreateAboutDto createAboutDto)
        {
            await _httpClient.PostAsJsonAsync<CreateAboutDto>("abouts", createAboutDto);
        }

        public async Task DeleteAboutAsync(string id)
        {
            await _httpClient.DeleteAsync("abouts?id=" + id);
        }

        public async Task<List<ResultAboutDto>> GetAllAboutAsync()
        {
            var response = await _httpClient.GetAsync("abouts");
            var values = await response.Content.ReadFromJsonAsync<List<ResultAboutDto>>();
            return values;
        }

        public async Task<GetByIdAboutDto> GetByIdAboutAsync(string id)
        {
            var response = await _httpClient.GetAsync("abouts/" + id);
            var value = await response.Content.ReadFromJsonAsync<GetByIdAboutDto>();
            return value;
        }

        public async Task UpdateAboutAsync(UpdateAboutDto updateAboutDto)
        {
            await _httpClient.PutAsJsonAsync<UpdateAboutDto>("abouts", updateAboutDto);
        }
    }
}
