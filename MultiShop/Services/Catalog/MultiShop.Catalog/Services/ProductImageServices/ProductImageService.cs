﻿using AutoMapper;
using MongoDB.Driver;
using MultiShop.Catalog.Dtos.ProductDtos;
using MultiShop.Catalog.Dtos.ProductImageDtos;
using MultiShop.Catalog.Entities;
using MultiShop.Catalog.Settings;

namespace MultiShop.Catalog.Services.ProductImageServices
{
    public class ProductImageService : IProductImageService
    {
        private readonly IMongoCollection<ProductImage> _categoryCollection;
        private readonly IMapper _mapper;

        public ProductImageService(IMapper mapper, IDatabaseSettings _databaseSettings)
        {
            var cliet = new MongoClient(_databaseSettings.ConnectionString);
            var database = cliet.GetDatabase(_databaseSettings.DatabaseName);
            _categoryCollection = database.GetCollection<ProductImage>(_databaseSettings.ProductImagesCollectionName);
            _mapper = mapper;
        }


        public async Task CreateProductImageAsync(CreateProductImageDto createProductImageDto)
        {
            var values = _mapper.Map<ProductImage>(createProductImageDto);
            await _categoryCollection.InsertOneAsync(values);
        }

        public Task DeleteProductImageAsync(string id)
        {
            return _categoryCollection.DeleteOneAsync(x => x.ProductImageId == id);
        }

        public async Task<GetByIdProductImageDto> GetByIdProductImageAsync(string id)
        {
            var values = await _categoryCollection.Find<ProductImage>(x => x.ProductImageId == id).FirstOrDefaultAsync();
            return _mapper.Map<GetByIdProductImageDto>(values);
        }

        public async Task<List<ResultProductImageDto>> GettAllProductImageAsync()
        {
            var values = await _categoryCollection.Find(x => true).ToListAsync();
            return _mapper.Map<List<ResultProductImageDto>>(values);
        }

        public async Task UpdateProductImageAsync(UpdateProductImageDto updateProductImageDto)
        {
            var values = _mapper.Map<ProductImage>(updateProductImageDto);
            await _categoryCollection.FindOneAndReplaceAsync(x => x.ProductImageId == updateProductImageDto.ProductImageId, values);
        }
    }
}