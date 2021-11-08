using FluentValidation;
using MvcBasicStockControl.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcBasicStockControl.ValidationRules
{
    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(p => p.ProductName).NotNull();
            RuleFor(p => p.Brand).NotNull();
            RuleFor(p => p.Price).NotNull();
            RuleFor(p => p.Price).GreaterThanOrEqualTo(1);
            RuleFor(p => p.Stock).NotNull();
            RuleFor(p => p.Stock).GreaterThanOrEqualTo(1);
        }
    }
}
