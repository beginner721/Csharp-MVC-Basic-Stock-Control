using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MvcBasicStockControl.Models;
using MvcBasicStockControl.ValidationRules;
using FluentValidation.Results;

namespace MvcBasicStockControl.Controllers
{
    public class CategoryController : Controller
    {
        ValidationResult result = new ValidationResult();
        CategoryValidator validator = new CategoryValidator();
        MvcWorkContext context = new MvcWorkContext();

        public IActionResult Index()
        {
            var categories = context.Categories.ToList();
            return View(categories);
        }

        [HttpGet]
        public IActionResult AddCategory()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddCategory(Category category)
        {
            result = validator.Validate(category);
            if (!result.IsValid)
            {
                return View("Hata");
            }
            context.Categories.Add(category);
            context.SaveChanges();
            return View();
        }
        public IActionResult Delete(int id)
        {
            var category = context.Categories.Find(id);
            context.Categories.Remove(category);
            context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Update(int id)
        {

            var category = context.Categories.Find(id);
            return View(category);
        }
        [HttpPost]
        public IActionResult Update(Category category)
        {
            //var categoryToUpdate = context.Categories.Find(category.CategoryId);
            //categoryToUpdate.CategoryName = category.CategoryName;
            context.Categories.Update(category);
            context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }


    }

}
