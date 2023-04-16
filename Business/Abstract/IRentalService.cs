using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTO_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IRentalService
    {
        IResult Add(Rental rental);
        IResult Delete(Rental rental);
        IResult Update(Rental rental);
        IDataResult<Rental> GetRentalById(int Id);
        IDataResult<List<Rental>> GetAll();
        IResult Transaction(Rental rental);
        IDataResult<List<RentalDetailsDto>> GetRentalDetails();
 
    }
}
