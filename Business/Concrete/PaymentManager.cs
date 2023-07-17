using Business.Abstract;
using Business.Constants.Messages;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Performances;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.BusinessRules;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class PaymentManager : IPaymentService
    {
        private IPaymentDal _paymentDal;
        public PaymentManager(IPaymentDal paymentDal)
        {
            _paymentDal= paymentDal;
        }

        [ValidationAspect(typeof(PaymentValidator))]
        [PerformanceAspect(30)]
        public IResult Add(Payment payment)
        {
            IResult result = BusinessRules.Run(CheckIfExDateLasterThanNowOrPast(payment.ExDate));
            if (result != null)
            {
                return new ErrorResult(result.Message);
            }
            if(payment.Status == false)
            {
                return new SuccessResult(PaymentMessages.CardChecked);
            }
            _paymentDal.Add(payment);
            return new SuccessResult(PaymentMessages.Succeed);
        }

        [PerformanceAspect(30)]
        public IResult Delete(Payment payment)
        {
            if (DateTime.Now.Hour == 05)
            {
                return new ErrorResult(Messages.MaintenanceTime);
            }
            _paymentDal.Delete(payment);
            return new SuccessResult(Messages.Succeed);
        }

        [PerformanceAspect(30)]
        public IResult DeleteById(int id)
        {
            if (DateTime.Now.Hour == 05)
            {
                return new ErrorResult(Messages.MaintenanceTime);
            }
            Payment paymentToDelete = _paymentDal.Get(p=>p.Id == id);
            _paymentDal.Delete(paymentToDelete);
            return new SuccessResult(Messages.Succeed);
        }
        [ValidationAspect(typeof(PaymentValidator))]
        [PerformanceAspect(30)]
        public IResult Update(Payment payment)
        {
            if (DateTime.Now.Hour == 05)
            {
                return new ErrorResult(Messages.MaintenanceTime);
            }
            _paymentDal.Update(payment);
            return new SuccessResult(Messages.Succeed);
        }
        [PerformanceAspect(30)]
        public IDataResult<List<Payment>> GetAll()
        {
            return new SuccessDataResult<List<Payment>>(_paymentDal.GetAll(),Messages.Listed);
        }

        public IDataResult<List<Payment>> GetPaysByUserId(int userId)
        {
            var result = _paymentDal.GetAll(p=>p.UserId == userId);
            if (result == null)
            {
                return new ErrorDataResult<List<Payment>>(Messages.Error);
            }
            return new SuccessDataResult<List<Payment>>(result,Messages.Succeed);
        }

        public IDataResult<Payment> GetPayById(int id)
        {
            var result = _paymentDal.Get(p=>p.Id == id);
            if (result == null)
            {
                return new ErrorDataResult<Payment>(PaymentMessages.PayNotFound);
            }
            return new SuccessDataResult<Payment>(result,Messages.Succeed);
        }
        private IResult CheckIfExDateLasterThanNowOrPast(string exDate)
        {
            string[] date = exDate.Split("/");
            int month = Int16.Parse(date[0]);
            int year = Int16.Parse(date[1]);
            if (year > DateTime.Now.Year)
            {
                return new SuccessResult();
            }
            else if (year == DateTime.Now.Year)
            {
                if (month >= DateTime.Now.Hour)
                {
                    return new SuccessResult();
                }
                else
                {
                    return new ErrorResult(PaymentMessages.InvalidExDate);
                }
            }
            return new ErrorResult(PaymentMessages.InvalidExDate + DateTime.Now.Year + " " +year);
        }

    }
}
