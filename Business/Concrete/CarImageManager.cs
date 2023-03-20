using Business.Abstract;
using Business.Constants.Messages;
using Business.ValidationRules.FluentValidation;
using Castle.Core.Internal;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.BusinessRules;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class CarImageManager : ICarImageService
    {
        ICarImageDal _carImageDal;
        public CarImageManager(ICarImageDal carImageDal)
        {
            _carImageDal= carImageDal;
        }

        [ValidationAspect(typeof(CarImageValidator))]
        public IResult Add(CarImage carImage)
        {

            var result = BusinessRules.Run(CheckIfCarImagesLimit(carImage.CarId),
                InsertDefaultImageIfNotExists(carImage.ImagePath));

            if (result != null)
            {
                return result;
            }

            carImage.Date = DateTime.Now;
            _carImageDal.Add(carImage);
            return new SuccessResult(Messages.Succeed);
            
        }
        [ValidationAspect(typeof(CarImageValidator))]
        public IResult Delete(CarImage carImage)
        {
            if (DateTime.Now.Hour == 05)
            {
                return new ErrorResult(Messages.MaintenanceTime);
            }
            _carImageDal.Delete(carImage);
            return new SuccessResult(Messages.Succeed);
        }
        [ValidationAspect(typeof(CarImageValidator))]
        public IDataResult<List<CarImage>> GetAll()
        {
            if (DateTime.Now.Hour == 05)
            {
                return new ErrorDataResult<List<CarImage>>(_carImageDal.GetAll());
            }
            return new SuccessDataResult<List<CarImage>>(_carImageDal.GetAll());
        }
        [ValidationAspect(typeof(CarImageValidator))]
        public IDataResult<CarImage> GetById(int id)
        {
            
            IResult result = BusinessRules.Run(CheckTheMaintenanceTime(id));
            if (result != null)
            {
                return new ErrorDataResult<CarImage>();
            }
            return new SuccessDataResult<CarImage>(_carImageDal.Get(c=>c.Id == id));
        }
        

        public IResult Update(CarImage carImage)
        {
            throw new NotImplementedException();
        }

        private IResult CheckIfCarImagesLimit(int carId)
        {
            var result = _carImageDal.GetAll(c=>c.CarId== carId).Count;
            if (result < 5)
            {
                return new SuccessResult(Messages.Succeed);
            }
            return new ErrorResult(Messages.CarImagesLimitExceded);
        }
        private IResult InsertDefaultImageIfNotExists(string imagePath)
        {
            if (imagePath == null)
            {
                imagePath = "default.jpg";
                
            }
            return new SuccessResult(Messages.Succeed);

        }
        private IResult CheckTheMaintenanceTime(int id)
        {
            var result = _carImageDal.Get(c=>c.Id== id);
            if (result.Date.Hour == 05)
            {
                return new ErrorResult(Messages.MaintenanceTime);
            }
            return new SuccessResult(Messages.Succeed);
        }
        
        
    }
}
