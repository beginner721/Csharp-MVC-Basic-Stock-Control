using Microsoft.AspNetCore.Mvc;
using MvcBasicStockControl.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcBasicStockControl.Controllers
{
    public class CustomerController : Controller
    {
        MvcWorkContext context = new MvcWorkContext();
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
            context.Customers.Add(customer);
            context.SaveChanges();
            return View();
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
