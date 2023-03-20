using Business.Abstract;
using Business.Constants.Messages;
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

        public IResult Add(Customer customer)
        {
            if (customer.CompanyName.Length < 3)
            {
                return new ErrorResult(Messages.InvalidNameError);
            }
            _customerDal.Add(customer);
            return new SuccessResult(Messages.Succeed);
        }

        public IResult Delete(Customer customer)
        {
            if (DateTime.Now.Hour == 18)
            {
                return new ErrorResult(Messages.MaintenanceTime);
            }
            _customerDal.Delete(customer);
            return new SuccessResult(Messages.Succeed);
        }

        public IDataResult<List<Customer>> GetAll()
        {
            if (DateTime.Now.Hour == 18)
            {
                return new ErrorDataResult<List<Customer>>(Messages.MaintenanceTime);
            }
            return new SuccessDataResult<List<Customer>>(_customerDal.GetAll(),Messages.Succeed);
        }
        public IDataResult<Customer> GetCustomerById(int customerId)
        {
            if (DateTime.Now.Hour == 18)
            {
                return new ErrorDataResult<Customer>(Messages.MaintenanceTime);
            }
            return new SuccessDataResult<Customer>(_customerDal.Get(c=>c.CustomerId== customerId),Messages.Succeed);
        }

        public IResult Update(Customer customer)
        {
            if (DateTime.Now.Hour == 18)
            {
                return new ErrorResult(Messages.MaintenanceTime);
            }
            _customerDal.Update(customer);
            return new SuccessResult(Messages.Succeed);
        }
    }
}
