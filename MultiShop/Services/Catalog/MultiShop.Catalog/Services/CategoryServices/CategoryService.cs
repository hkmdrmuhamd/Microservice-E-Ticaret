using AutoMapper;
using MongoDB.Driver;
using MultiShop.Catalog.Dtos.CategoryDtos;
using MultiShop.Catalog.Entities;
using MultiShop.Catalog.Settings;

namespace MultiShop.Catalog.Services.CategoryServices
{
    public class CategoryService : ICategoryService
    {
        private readonly IMongoCollection<Category> _categoryCollection; //MongoDB veritabanı koleksiyonu
        private readonly IMapper _mapper;

        public CategoryService(IMapper mapper, IDatabaseSettings _databaseSettings)
        {
            var cliet = new MongoClient(_databaseSettings.ConnectionString); //MongoDBye bağlantı adresi alındı
            var database = cliet.GetDatabase(_databaseSettings.DatabaseName); //MongoDB veritabanı adı alındı
            _categoryCollection = database.GetCollection<Category>(_databaseSettings.CategoryCollectionName); //MongoDB veritabanı içerisindeki koleksiyon adı alındı
            _mapper = mapper;
        }

        public async Task CreateCategoryAsync(CreateCategoryDto createCategoryDto)
        {
            var values = _mapper.Map<Category>(createCategoryDto); //Gelen veriyi Category nesnesine dönüştürdük
            await _categoryCollection.InsertOneAsync(values);//MongoDB veritabanına kayıt işlemi yapıldı
        }

        public async Task DeleteCategoryAsync(string id)
        {
            await _categoryCollection.DeleteOneAsync(x => x.CategoryId == id); //MongoDB veritabanından silme işlemi yapıldı
        }

        public async Task<GetByIdCategoryDto> GetByIdCategoryAsync(string id)
        {
            var values = await _categoryCollection.Find<Category>(x => x.CategoryId == id).FirstOrDefaultAsync(); //MongoDB veritabanından id'ye göre kayıt getirme işlemi yapıldı
            return _mapper.Map<GetByIdCategoryDto>(values); //Gelen veriyi GetByIdCategoryDto nesnesine dönüştürdük
        }

        public async Task<List<ResultCategoryDto>> GettAllCategoryAsync()
        {
            var values = await _categoryCollection.Find(x => true).ToListAsync(); //MongoDB veritabanındaki tüm kayıtları getirme işlemi yapıldı
            return _mapper.Map<List<ResultCategoryDto>>(values); //Gelen veriyi ResultCategoryDto nesnesine dönüştürdük
        }

        public async Task UpdateCategoryAsync(UpdateCategoryDto updateCategoryDto)
        {
            var values = _mapper.Map<Category>(updateCategoryDto); //Gelen veriyi Category nesnesine dönüştürdük
            await _categoryCollection.FindOneAndReplaceAsync(x => x.CategoryId == updateCategoryDto.CategoryId, values); //MongoDB veritabanında güncelleme işlemi yapıldı
        }
    }
}
