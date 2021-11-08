using FluentValidation;
using MvcBasicStockControl.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcBasicStockControl.ValidationRules
{
    public class CustomerValidator : AbstractValidator<Customer>
    {
        public CustomerValidator()
        {
            RuleFor(c => c.FirstName).NotNull();
            RuleFor(c => c.FirstName).MaximumLength(49);
            RuleFor(c => c.LastName).NotNull();
            RuleFor(c => c.LastName).MaximumLength(49);
            
        }
    }
}
