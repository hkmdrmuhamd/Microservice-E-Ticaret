using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using MultiShop.Catalog.Services.AboutServices;
using MultiShop.Catalog.Services.BrandServices;
using MultiShop.Catalog.Services.CategoryServices;
using MultiShop.Catalog.Services.FeatureServices;
using MultiShop.Catalog.Services.FeatureSliderServices;
using MultiShop.Catalog.Services.OfferDiscountServices;
using MultiShop.Catalog.Services.ProductDetailServices;
using MultiShop.Catalog.Services.ProductImageServices;
using MultiShop.Catalog.Services.ProductServices;
using MultiShop.Catalog.Services.SpecialOfferServices;
using MultiShop.Catalog.Settings;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.Authority = builder.Configuration["IdentityServerUrl"]; //IdentityServerUrl'i uygulama ayarlar�ndan okunur bu de�er kar��l���nda ilgili Url'yi �al��t�r�r.
    options.Audience = "ResourceCatalog"; //Hangi kayna�a eri�im sa�lanaca��n� belirtir. (Kaynak ad� IdentityServer'da Config dosyas�nda tan�mlanm�� olmal�d�r.)
    options.RequireHttpsMetadata = false; //Https zorunlulu�unu kald�r�r.
});

#region Dependency Injection
    builder.Services.AddScoped<IProductService, ProductService>();
    builder.Services.AddScoped<ICategoryService, CategoryService>();
    builder.Services.AddScoped<IProductDetailService , ProductDetailService>();
    builder.Services.AddScoped<IProductImageService , ProductImageService>();
    builder.Services.AddScoped<IFeatureSliderService, FeatureSliderService>();
    builder.Services.AddScoped<ISpecialOfferService, SpecialOfferService>();
    builder.Services.AddScoped<IFeatureService, FeatureService>();
    builder.Services.AddScoped<IOfferDiscountService, OfferDiscountService>();
    builder.Services.AddScoped<IBrandService, BrandService>();
    builder.Services.AddScoped<IAboutService, AboutService>();


    builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

    builder.Services.Configure<DatabaseSettings>(builder.Configuration.GetSection(nameof(DatabaseSettings))); //DatabaseSettings yap�land�rma b�l�m�n�, uygulama ayarlar�ndan okur ve ayarlar
    builder.Services.AddScoped<IDatabaseSettings>(sp =>  //DatabaseSettings i�indeki de�erleri al�p kullan�ma haz�rlar.
        sp.GetRequiredService<IOptions<DatabaseSettings>>().Value
    );
#endregion

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
