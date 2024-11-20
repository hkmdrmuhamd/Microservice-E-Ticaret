using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using MultiShop.DtoLayer.CatalogDtos.CategoryDtos;
using MultiShop.DtoLayer.CatalogDtos.ProductDtos;
using MultiShop.WebUI.Services.CatalogServices.CategoryServices;
using MultiShop.WebUI.Services.CatalogServices.ProductServices;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;

namespace MultiShop.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/Product")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        public ProductController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }

        [Route("Index")]
        public async Task<IActionResult> Index()
        {
            ProductViewBagList();
            var values = await _productService.GetAllProductAsync();
            return View(values);
        }

        [HttpGet]
        [Route("CreateProduct")]
        public async Task<IActionResult> CreateProduct()
        {
            ProductViewBagList();
            var values = await _categoryService.GettAllCategoryAsync();
            List<SelectListItem> selectListItems = (from x in values
                                                    select new SelectListItem
                                                    {
                                                        Text = x.CategoryName,
                                                        Value = x.CategoryId
                                                    }).ToList();
            ViewBag.Categories = selectListItems;
            return View();
        }

        [HttpPost]
        [Route("CreateProduct")]
        public async Task<IActionResult> CreateProduct(CreateProductDto createProductDto)
        {
            await _productService.CreateProductAsync(createProductDto);
            return RedirectToAction("Index", "Product", new { area = "Admin" });
        }

        [HttpGet]
        [Route("UpdateProduct/{id}")]
        public async Task<IActionResult> UpdateProduct(string id)
        {
            ProductViewBagList();
            var values = await _categoryService.GettAllCategoryAsync();
            List<SelectListItem> selectListItems = (from x in values
                                                    select new SelectListItem
                                                    {
                                                        Text = x.CategoryName,
                                                        Value = x.CategoryId
                                                    }).ToList();
            ViewBag.Products = selectListItems;
            var value = await _productService.GetByIdProductAsync(id);
            return View(value);
        }

        [HttpPost]
        [Route("UpdateProduct/{id}")]
        public async Task<IActionResult> UpdateProduct(UpdateProductDto updateProductDto)
        {
            await _productService.UpdateProductAsync(updateProductDto);
            return RedirectToAction("Index", "Product", new { area = "Admin" });
        }

        [Route("DeleteProduct/{id}")]
        public async Task<IActionResult> DeleteProduct(string id)
        {
            await _productService.DeleteProductAsync(id);
            return RedirectToAction("Index", "Product", new { area = "Admin" });
        }

        [HttpGet]
        [Route("ProductListWithCategory")]
        public async Task<IActionResult> ProductListWithCategory()
        {
            ProductViewBagList();
            var values = await _productService.GetProductsWithCategoryAsync();
            return View(values);
        }

        void ProductViewBagList()
        {
            ViewBag.V0 = "Ürün İşlemleri";
            ViewBag.V1 = "Ana Sayfa";
            ViewBag.V2 = "Ürünler";
            ViewBag.V3 = "Ürün Listesi";
        }
    }
}