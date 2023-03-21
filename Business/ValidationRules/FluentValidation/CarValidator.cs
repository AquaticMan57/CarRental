using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation
{
    public class CarValidator : AbstractValidator<Car>
    {

        public CarValidator()
        {
            
            RuleFor(c => c.Description).NotEmpty();
            RuleFor(c=>c.ModelYear).Must(StartsWith2);
            RuleFor(c=>c.DailyPrice).GreaterThan(100);
            RuleFor(c => c.DailyPrice).GreaterThanOrEqualTo(400).When(c => c.BrandId == 2);
        }

        private bool StartsWith2(string arg)
        {
            return arg.StartsWith("2");
        }
    }
}
