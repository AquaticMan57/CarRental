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
    public interface ICarService
    {
        IDataResult<List<CarDetailDto>> GetCarDetails();
        IDataResult<List<Car>> GetAll();
        IDataResult<List<Car>> GetCarsByColorId(int colorId);
        IDataResult<List<Car>> GetCarsByBrandId(int brandId);
        IDataResult<List<Car>> GetCarByDailyPrice(decimal min,decimal max);
        IDataResult<List<CarDetailDto>> GetCarsDetailByBrandId(int brandId);
        IDataResult<List<CarDetailDto>> GetCarsDetailByColorId(int brandId);
        IDataResult<List<CarDetailDto>> GetCarsDetailByBrandAndColorId(int brandId,int colorId);
        IDataResult<List<CarDetailDto>> GetCarDetailByCarId(int carId);
        IResult Update(Car car);
        IResult Delete(Car car);
        IResult Add(Car car);
        IResult Transaction(Car car);
    }
}
