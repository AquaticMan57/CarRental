using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ICustomerService
    {
        IResult Add(Customer customer);
        IResult Delete(Customer customer);
        IResult Update(Customer customer);
        IResult DeleteById(int id);
        IDataResult<List<Customer>> GetAll();
        IDataResult<Customer> GetCustomerById(int id);
        IDataResult<Customer> GetCustomerByUserId(int userId);
        IResult Transaction(Customer customer);

    }
}
