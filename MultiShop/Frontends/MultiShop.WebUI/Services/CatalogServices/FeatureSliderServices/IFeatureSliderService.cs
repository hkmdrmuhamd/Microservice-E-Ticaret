using MultiShop.DtoLayer.CatalogDtos.FeatureSliderDtos;

namespace MultiShop.WebUI.Services.CatalogServices.FeatureSliderServices
{
    public interface IFeatureSliderService
    {
        Task CreateFeatureSliderAsync(CreateFeatureSliderDto createFeatureSliderDto);
        Task UpdateFeatureSliderAsync(UpdateFeatureSliderDto updateFeatureSliderDto);
        Task<List<ResultFeatureSliderDto>> GetAllFeatureSliderAsync();
        Task<GetByIdFeatureSliderDto> GetByIdFeatureSliderAsync(string id);
        Task DeleteFeatureSliderAsync(string id);
        Task FeatureSliderChageStatusToTrue(string id);
        Task FeatureSliderChageStatusToFalse(string id);
    }
}
