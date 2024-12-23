﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.CatalogDtos.CategoryDtos;
using MultiShop.WebUI.Services.CatalogServices.CategoryServices;
using Newtonsoft.Json;
using System.Text;

namespace MultiShop.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/Category")] // URL'de kullanılacak isim. (Bu işlem bazı yönlendirmelerin yapılabilmesi için kullanılmıştır.)
    public class CategoryController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ICategoryService _categoryService;

        public CategoryController(IHttpClientFactory httpClientFactory, ICategoryService categoryService)
        {
            _httpClientFactory = httpClientFactory;
            _categoryService = categoryService;
        }

        [Route("Index")]
        public async Task<IActionResult> Index()
        {
            CategoryViewBagList();
            var values = await _categoryService.GettAllCategoryAsync();
            return View(values);
        }

        [Authorize]
        [HttpGet]
        [Route("CreateCategory")]
        public IActionResult CreateCategory()
        {
            CategoryViewBagList();
            return View();
        }

        [Authorize]
        [HttpPost]
        [Route("CreateCategory")]
        public async Task<IActionResult> CreateCategory(CreateCategoryDto createCategoryDto)
        {
            await _categoryService.CreateCategoryAsync(createCategoryDto);
            return RedirectToAction("Index", "Category", new { area = "Admin" });
        }

        [Authorize]
        [Route("DeleteCategory/{id}")]
        public async Task<IActionResult> DeleteCategory(string id)
        {
            await _categoryService.DeleteCategoryAsync(id);
            return RedirectToAction("Index", "Category", new { area = "Admin" });
        }

        [Authorize]
        [HttpGet]
        [Route("UpdateCategory/{id}")]
        public async Task<IActionResult> UpdateCategory(string id)
        {
            CategoryViewBagList();
            var values = await _categoryService.GetByIdCategoryAsync(id);
            var updateCategoryDto = new UpdateCategoryDto
            {
                CategoryId = values.CategoryId,
                CategoryName = values.CategoryName,
                ImageUrl = values.ImageUrl
            };
            return View(updateCategoryDto);
        }

        [Authorize]
        [HttpPost]
        [Route("UpdateCategory/{id}")]
        public async Task<IActionResult> UpdateCategory(UpdateCategoryDto updateCategoryDto)
        {
            await _categoryService.UpdateCategoryAsync(updateCategoryDto);
            return RedirectToAction("Index", "Category", new { area = "Admin" });
        }

        void CategoryViewBagList()
        {
            ViewBag.V0 = "Kategori İşlemleri";
            ViewBag.V1 = "Ana Sayfa";
            ViewBag.V2 = "Kategoriler";
            ViewBag.V3 = "Kategori Listesi";
        }
    }
}
