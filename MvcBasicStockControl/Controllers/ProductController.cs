using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MvcBasicStockControl.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using FluentValidation.Results;
using MvcBasicStockControl.ValidationRules;

namespace MvcBasicStockControl.Controllers
{
    public class ProductController : Controller
    {
        ValidationResult result = new ValidationResult();
        ProductValidator validator = new ProductValidator();
        MvcWorkContext context = new MvcWorkContext();
        public IActionResult Index()
        {
            var products = context.Products.ToList();
            foreach (var product in products)
            {
                context.Entry(product).Reference(db => db.Category).Load();
            }
            return View(products);
        }

        [HttpGet]
        public IActionResult AddProduct()
        {
            //We need Product with Category Name
            var categoryList = context.Categories.Select(a => new Category
            { CategoryId = a.CategoryId, CategoryName = a.CategoryName }).ToList();

            ViewBag.CategoryNames = categoryList;
            return View();
        }
        [HttpPost]
        public IActionResult AddProduct(Product product)
        {
            result = validator.Validate(product);
            if (!result.IsValid)
            {
                return BadRequest("Hata");
            }
            var category = context.Categories.Where(a => a.CategoryId == product.CategoryId).FirstOrDefault();
            product.Category = category;
            context.Products.Add(product);
            context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Delete(int id)
        {
            var product = context.Products.Find(id);
            context.Products.Remove(product);
            context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Update(int id)
        {
            var product = context.Products.Find(id);
            return View(product);
        }
        [HttpPost]
        public IActionResult Update(Product product)
        {
            context.Products.Update(product);
            context.SaveChanges();
            return View(product);
        }
    }
}
