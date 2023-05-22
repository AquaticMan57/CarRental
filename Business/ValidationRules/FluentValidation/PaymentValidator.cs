using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation
{
    public class PaymentValidator : AbstractValidator<Payment>
    {
        public PaymentValidator()
        {
            RuleFor(p => p.CardNumber).Must(Has16Keys);
            RuleFor(p => p.CardNumber).CreditCard();
            RuleFor(p => p.Cvc).Length(3);
        }
        private bool Has16Keys(string arg)
        {
            bool result = arg.Length == 16;
            return result;
        }
    }
}
