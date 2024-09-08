using Microsoft.AspNetCore.Mvc;

namespace MultiShop.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.V0 = "Kategori İşlemleri";
            ViewBag.V1 = "Ana Sayfa";
            ViewBag.V2 = "Kategoriler";
            ViewBag.V3 = "Kategori Listesi";
            return View();
        }
    }
}
