using EntityLayer.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules
{
    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(x => x.CategoryID).NotNull();
            RuleFor(x => x.CategoryID).NotEmpty();

            RuleFor(x => x.Name).NotNull();
            RuleFor(x => x.Name).NotEmpty();

            RuleFor(x => x.Price).NotNull();
            RuleFor(x => x.Price).NotEmpty();
        }
    }
}
