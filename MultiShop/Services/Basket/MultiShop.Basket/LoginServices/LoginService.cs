namespace MultiShop.Basket.LoginServices
{
    public class LoginService : ILoginService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;// Kullanıcı kimliğini almak için HttpContextAccessor kullanılır.

        public LoginService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        
        public string GetUserId => _httpContextAccessor.HttpContext.User.FindFirst("sub").Value; //sub ile jwt içerisindeki kullanıcı kimliği alınır.
    }
}
