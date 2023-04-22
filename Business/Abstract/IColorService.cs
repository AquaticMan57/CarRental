using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IColorService
    {
        IDataResult<List<Colors>> GetAll();
        IDataResult<Colors> GetColorById(int id);
        IResult Add(Colors colors);
        IResult Delete(Colors colors);
        IResult Update(Colors colors);
        IResult Transaction(Colors colors);

    }
}
