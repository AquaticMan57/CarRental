using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants.Messages;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Performances;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
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
            
            _customerDal.Add(customer);
            return new SuccessResult(Messages.Succeed);
        }
        //[CacheRemoveAspect("ICarService.Get")]
        [PerformanceAspect(10)]
        //[SecuredOperation("delete,admin")]

        public IResult Delete(Customer customer)
        {
            if (DateTime.Now.Hour == 05)
            {
                return new ErrorResult(Messages.MaintenanceTime);
            }
            _customerDal.Delete(customer);
            return new SuccessResult(Messages.Succeed);
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
        public IDataResult<List<Customer>> GetCustomersByUserId(int userId)
        {
            return new SuccessDataResult<List<Customer>>(_customerDal.GetAll(c => c.UserId == userId), Messages.Succeed);
        }

        public IResult Transaction(Customer customer)
        {
            throw new NotImplementedException();
        }

        //[SecuredOperation("update,admin")]
        [PerformanceAspect(10)]
        //[CacheRemoveAspect("ICarService.Get")]

        public IResult Update(Customer customer)
        {
            if (DateTime.Now.Hour == 05)
            {
                return new ErrorResult(Messages.MaintenanceTime);
            }
            _customerDal.Update(customer);
            return new SuccessResult(Messages.Succeed);
        }
    }
}
