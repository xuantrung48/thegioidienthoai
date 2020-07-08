﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TheGioiDienThoai.Models.ProductModel;

namespace TheGioiDienThoai.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CategoriesManagerController : Controller
    {
        private readonly ICategoryRepository categoryRepository;
        public CategoriesManagerController(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }
        public IActionResult Index()
        {
            return View(categoryRepository.Get());
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Category newCategory)
        {
            if (ModelState.IsValid)
            {
                categoryRepository.Create(newCategory);
                return RedirectToAction("Index", "CategoriesManager");
            }
            return View();
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var category = categoryRepository.Get(id);
            if (category == null)
                return View("~/Views/Error/PageNotFound.cshtml");
            return View(category);
        }
        [HttpPost]
        public IActionResult Edit(Category category)
        {
            if (categoryRepository.Edit(category) != null)
            {
                return RedirectToAction("Index", "CategoriesManager");
            }
            return View();
        }
        public IActionResult Remove(int id)
        {
            var category = categoryRepository.Get(id);
            if (category == null)
                return View("~/Views/Error/PageNotFound.cshtml");
            if (categoryRepository.Remove(id))
            {
                return RedirectToAction("Index", "CategoriesManager");
            }
            return View();
        }
    }
}