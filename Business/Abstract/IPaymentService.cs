using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IPaymentService
    {
        IResult Add(Payment payment);
        IResult Delete(Payment payment);
        IResult DeleteById(int id);
        IResult Update(Payment payment);
        IResult CheckCard(Payment payment);
        IDataResult<List<Payment>> GetAll();
        IDataResult<List<Payment>> GetPaysByUserId(int userId);
        IDataResult<Payment> GetPayById(int id);
    }
}
