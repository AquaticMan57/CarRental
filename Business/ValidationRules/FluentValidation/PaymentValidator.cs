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
            RuleFor(p => p.CardNumber).Must(Has16Keys);;
            RuleFor(p => p.Cvc).Length(3);
            RuleFor(p => p.ExDate).NotEmpty();
            //RuleFor(p => p.ExDate).Must(LasterThanNowOrPast);
            
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
        private bool LasterThanNowOrPast(string exYear)
        {
            string[] date = exYear.Split("/");
            int month = Int16.Parse(date[0]);
            int year = Int16.Parse(date[1]);
            if (year > DateTime.Now.Year)
            {
                return true;
            }
            else if (year == DateTime.Now.Year)
            {
                if (month >= DateTime.Now.Hour)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }
        
    }
}
