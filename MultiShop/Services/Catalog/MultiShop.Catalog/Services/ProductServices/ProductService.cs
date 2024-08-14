﻿using AutoMapper;
using MongoDB.Driver;
using MultiShop.Catalog.Dtos.ProductDtos;
using MultiShop.Catalog.Entities;
using MultiShop.Catalog.Settings;

namespace MultiShop.Catalog.Services.ProductServices
{
    public class ProductService : IProductService
    {
        private readonly IMongoCollection<Product> _categoryCollection;
        private readonly IMapper _mapper;

        public ProductService(IMapper mapper, IDatabaseSettings _databaseSettings)
        {
            var cliet = new MongoClient(_databaseSettings.ConnectionString);
            var database = cliet.GetDatabase(_databaseSettings.DatabaseName);
            _categoryCollection = database.GetCollection<Product>(_databaseSettings.ProductCollectionName);
            _mapper = mapper;
        }


        public async Task CreateProductAsync(CreateProductDto createProductDto)
        {
            var values = _mapper.Map<Product>(createProductDto);
            await _categoryCollection.InsertOneAsync(values);
        }

        public Task DeleteProductAsync(string id)
        {
            return _categoryCollection.DeleteOneAsync(x => x.ProductId == id);
        }

        public async Task<GetByIdProductDto> GetByIdProductAsync(string id)
        {
            var values = await _categoryCollection.Find<Product>(x => x.ProductId == id).FirstOrDefaultAsync();
            return _mapper.Map<GetByIdProductDto>(values);
        }

        public async Task<List<ResultProductDto>> GettAllProductAsync()
        {
            var values = await _categoryCollection.Find(x => true).ToListAsync();
            return _mapper.Map<List<ResultProductDto>>(values);
        }

        public async Task UpdateProductAsync(UpdateProductDto updateProductDto)
        {
            var values =  _mapper.Map<Product>(updateProductDto);
            await _categoryCollection.FindOneAndReplaceAsync(x => x.ProductId == updateProductDto.ProductId, values);
        }
    }
}