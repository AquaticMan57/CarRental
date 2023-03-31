using Autofac;
using Business.Abstract;
using Business.BusinessAspect;
using Business.Constants.Messages;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.BusinessRules;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Conctrete.InMemory;
using Entities.Concrete;
using Entities.DTO_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class CarManager : ICarService
    {
        ICarDal _carDal;
        
        public CarManager(ICarDal carDal)
        {
            _carDal = carDal;
            
        }

        //loglamak : yapilan operasyonda bir yerde kaydini tutmak
        
        //[SecuredOperation("product.add,admin")]
        [ValidationAspect(typeof(CarValidator))]
        public IResult Add(Car car)
        {
            var result = BusinessRules.Run(CheckIfCarNameExists(car.Description));
            if (result != null)
            {
                return new ErrorResult(result.Message);
            }
            if (DateTime.Now.Hour ==05)
            {
                return new ErrorResult(Messages.MaintenanceTime);
            }
            
            _carDal.Add(car);
            return new SuccessResult(Messages.Succeed);

        }

        public IResult Delete(Car car)
        {
             if (car.Description.Length <= 2)
             {
                 return new ErrorResult(Messages.InvalidNameError);
             }
             _carDal.Delete(car);
             return new SuccessResult(Messages.Succeed);
        }

        public IDataResult<List<Car>> GetCarByDailyPrice(decimal min, decimal max)
        {

            return new SuccessDataResult<List<Car>>(_carDal.GetAll(c=>c.DailyPrice>=min && c.DailyPrice<=max),Messages.Succeed);
        }

        public IDataResult<List<CarDetailDto>> GetCarDetails()
        {
            if (DateTime.Now.Hour ==17)
            {
                return new ErrorDataResult<List<CarDetailDto>>(Messages.MaintenanceTime);
            }
            return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetCarsDetail(),Messages.Succeed);
        }

        public IDataResult<List<Car>> GetCars()
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(),CarMessages.CarsListed);
        }

        public IDataResult<List<Car>> GetCarsByBrandId(int brandId)
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(c=>c.BrandId==brandId),Messages.Succeed);
        }

        public IDataResult<List<Car>> GetCarsByColorId(int colorId)
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(c => c.ColorId == colorId),Messages.Succeed);
        }

        public IResult Update(Car car)
        {
            if (car.Description.Length<=2)
            {
                return new ErrorResult(Messages.InvalidNameError);
            }
            _carDal.Update(car);
            return new SuccessResult(Messages.Succeed);
        }
        private IResult CheckIfCarNameExists(string description)
        {
            var text = _carDal.Get(c=>c.Description == description);

            if (text == null)
            {
                return new SuccessResult(Messages.Succeed);
            }
            return new ErrorResult(Messages.NameAlreadyExists);
        }

    }
}
