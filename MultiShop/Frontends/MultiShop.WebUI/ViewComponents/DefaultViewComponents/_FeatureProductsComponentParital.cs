using Microsoft.AspNetCore.Mvc;

namespace MultiShop.WebUI.ViewComponents.DefaultViewComponents
{
    public class _FeatureProductsComponentParital : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
