using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using MultiShop.WebUI.Services;
using MultiShop.WebUI.Services.Concrete;
using MultiShop.WebUI.Services.Interfaces;
using MultiShop.WebUI.Settings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddCookie(JwtBearerDefaults.AuthenticationScheme, opt =>
    {
        opt.LoginPath = "/Login/Index/"; //Login sayfasý
        opt.LogoutPath = "/Login/LogOut/"; //Çýkýþ sayfasý
        opt.AccessDeniedPath = "/Pages/AccessDenied/"; //Yetkisiz eriþim sayfasý
        opt.Cookie.HttpOnly = true; //https'e gerek olmasýn
        opt.Cookie.SameSite = SameSiteMode.Strict; // Cookie'nin sadece ayný site üzerinden gönderilmesini saðlar (CSRF korumasý için)
		opt.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest; // Cookie'nin güvenlik politikasýný isteðin güvenli olup olmamasýna göre ayarlar (HTTP/HTTPS)
		opt.Cookie.Name = "MultiShopJwt"; // Cookie'ye "MultiShopJwt" adýný verir
	});

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, opt =>
    {
        opt.LoginPath = "/Login/Index";
        opt.ExpireTimeSpan = TimeSpan.FromDays(5);
        opt.Cookie.Name = "MultiShopCookie";
        opt.SlidingExpiration = true;
    });

builder.Services.AddHttpContextAccessor(); // HTTP context eriþimini saðlamak için kullanýlýr

builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddHttpClient<IIdentityService, IdentityService>();

builder.Services.Configure<ClientSettings>(builder.Configuration.GetSection("ClientSettings"));
builder.Services.Configure<ServiceApiSettings>(builder.Configuration.GetSection("ServiceApiSettings"));

builder.Services.AddHttpClient();

builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication(); // Kimlik doðrulama iþlemlerini baþlatýr
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );
});

app.Run();
