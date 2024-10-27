using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.IdentityDtos.LoginDtos;
using MultiShop.WebUI.Models;
using MultiShop.WebUI.Services;
using MultiShop.WebUI.Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace MultiShop.WebUI.Controllers
{
	public class LoginController : Controller
	{
		private readonly IHttpClientFactory _httpClientFactory;
		private readonly ILoginService _loginService;
		private readonly IIdentityService _identityService;

        public LoginController(IHttpClientFactory httpClientFactory, ILoginService loginService, IIdentityService identityService)
        {
            _httpClientFactory = httpClientFactory;
            _loginService = loginService;
            _identityService = identityService;
        }

        [HttpGet]
		public IActionResult Index()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Index(CreateLoginDto createLoginDto)
		{
			var client = _httpClientFactory.CreateClient();
			var content = new StringContent(JsonSerializer.Serialize(createLoginDto), Encoding.UTF8, "application/json");
			var response = await client.PostAsync("http://localhost:5001/api/Logins", content);
			if(response.IsSuccessStatusCode)
			{
				var jsonData = await response.Content.ReadAsStringAsync();
				var tokenModel = JsonSerializer.Deserialize<JwtResponseModel>(jsonData, new JsonSerializerOptions //Modeli alır ve json'dan deserialize eder
				{
					// JSON'daki property adları camelCase ise, otomatik olarak modeldeki PascalCase adlara map'lenmesini sağlar
					PropertyNamingPolicy = JsonNamingPolicy.CamelCase
				});

				if(tokenModel != null)
				{
					// JwtSecurityTokenHandler sınıfını kullanabilmek için Microsoft.AspNetCore.Authentication.JwtBearer paketini yüklememiz gerekmektedir
					JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler(); // JWT token'ını işleyecek bir handler oluşturur
					var token = handler.ReadJwtToken(tokenModel.Token); // Token string'ini JWT objesine çevirir
					var claims = token.Claims.ToList(); // JWT içindeki tüm claim'leri listeye dönüştürür (claim: token'daki kullanıcıya ait bilgiler)

					if (tokenModel.Token != null)
					{
						claims.Add(new Claim("multishoptoken", tokenModel.Token)); //Token'i çözeceğimiz sayfalarda kullanmak için isim verip claim'lere kaydeder.
						var claimsIdentity = new ClaimsIdentity(claims, JwtBearerDefaults.AuthenticationScheme); // JWT claim'lerini kullanarak bir kimlik oluşturur
						var authProps = new AuthenticationProperties // Kimlik doğrulama özelliklerini ayarlar (token'ın bitiş süresi ve kalıcı olup olmayacağı)
						{
							ExpiresUtc = tokenModel.ExpireDate, // Token'ın ne zaman sona ereceğini ayarlar
							IsPersistent = true // Kullanıcı oturumunun kalıcı olmasını sağlar
						};

						await HttpContext.SignInAsync(JwtBearerDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProps); // Kullanıcıyı belirlenen kimlik ve özelliklerle oturum açtırır (JWT ile kimlik doğrulaması yapılır)
						var id = _loginService.GetUserId;
						return RedirectToAction("Index", "Default");
					}
				}
			}
			return View();
		}

		//[HttpGet]
		//public IActionResult SignIn()
		//{             
		//	return View();        
		//}

		//[HttpPost]
		public async Task<IActionResult> SignIn(SignInDto signInDto)
		{
            signInDto.Username = "deneme";
            signInDto.Password = "1234";
			await _identityService.SignIn(signInDto);
            return RedirectToAction("Index", "Test");
        }
	}
}
