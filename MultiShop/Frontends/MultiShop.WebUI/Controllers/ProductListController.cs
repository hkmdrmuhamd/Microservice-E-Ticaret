using Microsoft.AspNetCore.Mvc;

namespace MultiShop.WebUI.Controllers
{
    public class ProductListController : Controller
    {
        public IActionResult Index(string id)
        {
            ViewBag.CategoryId = id;
            return View();
        }

        public IActionResult ProductDetail(string id)
        {
            if(id != "string")
            {
                ViewBag.ProductID = id;
                return View();
            }

            return RedirectToAction("Index", "ProductDetail");
        }
    }
}
