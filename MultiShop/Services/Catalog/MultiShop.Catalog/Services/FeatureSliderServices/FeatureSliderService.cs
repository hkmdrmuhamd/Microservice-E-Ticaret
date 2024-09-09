using AutoMapper;
using MongoDB.Driver;
using MongoDB.Driver.Core.Misc;
using MultiShop.Catalog.Dtos.FeatureSliderDtos;
using MultiShop.Catalog.Entities;
using MultiShop.Catalog.Settings;

namespace MultiShop.Catalog.Services.FeatureSliderServices
{
    public class FeatureSliderService : IFeatureSliderService
    {
        public Task CreateFeatureSliderAsync(CreateFeatureSliderDto createFeatureSliderDto)
        {
            throw new NotImplementedException();
        }

        public Task DeleteFeatureSliderAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task FeatureSliderChageStatusToFalse(string id)
        {
            throw new NotImplementedException();
        }

        public Task FeatureSliderChageStatusToTrue(string id)
        {
            throw new NotImplementedException();
        }

        public Task<List<ResultFeatureSliderDto>> GetAllFeatureSliderAsync()
        {
            throw new NotImplementedException();
        }

        public Task<GetByIdFeatureSliderDto> GetByIdFeatureSliderAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateFeatureSliderAsync(UpdateFeatureSliderDto updateFeatureSliderDto)
        {
            throw new NotImplementedException();
        }
    }
}
