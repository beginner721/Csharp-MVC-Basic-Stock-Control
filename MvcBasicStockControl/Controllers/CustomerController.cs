using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MvcBasicStockControl.Constants;
using MvcBasicStockControl.Models;
using MvcBasicStockControl.ValidationRules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcBasicStockControl.Controllers
{
    public class CustomerController : Controller
    {
        ValidationResult result = new ValidationResult();
        CustomerValidator validator = new CustomerValidator();
        MvcWorkContext context = new MvcWorkContext();

        [Authorize]
        public IActionResult Index()
        {
            var customers = context.Customers.ToList();
            return View(customers);
        }
        [HttpGet]
        public IActionResult AddCustomer()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddCustomer(Customer customer)
        {
            result = validator.Validate(customer);
            if (!result.IsValid)
            {
                return BadRequest(Messages.Error);
            }
            context.Customers.Add(customer);
            context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Delete(int id)
        {
            var customer = context.Customers.Find(id);
            context.Customers.Remove(customer);
            context.SaveChanges();
            return RedirectToAction(nameof(Index));

        }
        public IActionResult Update(int id)
        {
            var customer = context.Customers.Find(id);
            return View(customer);
        }
        [HttpPost]
        public IActionResult Update(Customer customer)
        {
            context.Customers.Update(customer);
            context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
