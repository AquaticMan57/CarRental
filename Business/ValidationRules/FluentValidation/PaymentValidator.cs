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
            RuleFor(p=>p.ExDate).NotEmpty();
            RuleFor(p => p.Cvc).Length(3);
        }
        private bool Has16Keys(string arg)
        {
            bool result;
            if (arg.Length == 16)
            {
                result = true;
            }
            else
            {
                result = false;
            }
            return result;
        }
        private bool LasterThanNowOrPast(DateTime date)
        {
            
            if (date>DateTime.Now)
            {
                return true;
            }
            return false;
        }
    }
}
