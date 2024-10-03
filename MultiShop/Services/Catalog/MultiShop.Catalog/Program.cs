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
    options.Authority = builder.Configuration["IdentityServerUrl"]; //IdentityServerUrl'i uygulama ayarlarýndan okunur bu deðer karþýlýðýnda ilgili Url'yi çalýþtýrýr.
    options.Audience = "ResourceCatalog"; //Hangi kaynaða eriþim saðlanacaðýný belirtir. (Kaynak adý IdentityServer'da Config dosyasýnda tanýmlanmýþ olmalýdýr.)
    options.RequireHttpsMetadata = false; //Https zorunluluðunu kaldýrýr.
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

    builder.Services.Configure<DatabaseSettings>(builder.Configuration.GetSection(nameof(DatabaseSettings))); //DatabaseSettings yapýlandýrma bölümünü, uygulama ayarlarýndan okur ve ayarlar
    builder.Services.AddScoped<IDatabaseSettings>(sp =>  //DatabaseSettings içindeki deðerleri alýp kullanýma hazýrlar.
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
