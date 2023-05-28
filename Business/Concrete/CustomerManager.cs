using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants.Messages;
using Business.ValidationRules.FluentValidation;
using Castle.Core.Internal;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Performances;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.BusinessRules;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class CustomerManager : ICustomerService
    {
        ICustomerDal _customerDal;
        public CustomerManager(ICustomerDal customerDal)
        {
            _customerDal= customerDal;
        }
        //[CacheRemoveAspect("ICarService.Get")]
        [PerformanceAspect(10)]
        //[SecuredOperation("add,admin")]
        [ValidationAspect(typeof(CustomerValidator))]
        public IResult Add(Customer customer)
        {
            IResult result =  BusinessRules.Run(
                CheckIfCompanyExists(customer.UserId)
                );

            if (result != null)
            {
                return result;
            }
            _customerDal.Add(customer);
            return new SuccessResult(Messages.Succeed);
        }
        //[CacheRemoveAspect("ICarService.Get")]
        [PerformanceAspect(10)]
        //[SecuredOperation("delete,admin")]

        public IResult Delete(Customer customer)
        {
            
            _customerDal.Delete(customer);
            return new SuccessResult(Messages.Succeed);
        }

        public IResult DeleteById(int id)
        {
            Customer customerToDelete = _customerDal.Get(c=>c.CustomerId== id);
            if (customerToDelete != null)
            {
                _customerDal.Delete(customerToDelete);
                return new SuccessResult(Messages.Succeed);
            }
            return new ErrorResult(CustomerMessages.CustomerNotFound);
        }

        [PerformanceAspect(10)]
        //[CacheAspect]
        //[SecuredOperation("list,admin")]

        public IDataResult<List<Customer>> GetAll()
        {
            if (DateTime.Now.Hour == 05)
            {
                return new ErrorDataResult<List<Customer>>(Messages.MaintenanceTime);
            }
            return new SuccessDataResult<List<Customer>>(_customerDal.GetAll(),Messages.Succeed);
        }

        [PerformanceAspect(10)]
        //[SecuredOperation("list,admin")]
        //[CacheAspect]
        public IDataResult<Customer> GetCustomerById(int customerId)
        {
            if (DateTime.Now.Hour == 05)
            {
                return new ErrorDataResult<Customer>(Messages.MaintenanceTime);
            }
            return new SuccessDataResult<Customer>(_customerDal.Get(c=>c.CustomerId== customerId),Messages.Succeed);
        }
        [PerformanceAspect(10)]
        public IDataResult<Customer> GetCustomerByUserId(int userId)
        {
            return new SuccessDataResult<Customer>(_customerDal.Get(c => c.UserId == userId), Messages.Succeed);
        }

        

        //[SecuredOperation("update,admin")]
        [PerformanceAspect(10)]
        //[CacheRemoveAspect("ICarService.Get")]
        [ValidationAspect(typeof(CustomerValidator))]
        public IResult Update(Customer customer)
        {
            
            _customerDal.Update(customer);
            return new SuccessResult(Messages.Succeed);
        }

        public IResult Transaction(Customer customer)
        {
            throw new NotImplementedException();
        }
        private IResult CheckIfCompanyNameExists(string companyName)
        {
            var text = _customerDal.GetAll(c => c.CompanyName == companyName).Any();
            if (text)
            {
                return new ErrorResult(CustomerMessages.CompanyNameExists); 
            }
            return new SuccessResult();
        }
        private IResult CheckIfCompanyMailExists(string mail)
        {
            var text = _customerDal.GetAll(c => c.CompanyMail == mail).Any();
            if (text)
            {
                return new ErrorResult(CustomerMessages.CompanyMailExists);
            }
            return new SuccessResult();
        }
        private IResult CheckIfCompanyExists(int userId)
        {
            var text = _customerDal.GetAll(c=>c.UserId == userId).Any();
            if (text)
            {
                return new ErrorResult(CustomerMessages.CompanyAlreadyExists);
            }
            return new SuccessResult();

        }
    }
}
