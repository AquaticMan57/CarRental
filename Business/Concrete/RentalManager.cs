using Business.Abstract;
using Business.BusinessAspect;
using Business.Constants.Messages;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
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
    public class RentalManager : IRentalService
    {
        IRentalDal _rentalDal;
        public RentalManager(IRentalDal rentalDal)
        {
            _rentalDal= rentalDal;
        }

        [CacheRemoveAspect("IRentalService.Get")]
        [SecuredOperation("add,admin")]
        [ValidationAspect(typeof(RentalValidator))]

        public IResult Add(Rental rental)
        {
            if (DateTime.Now.Hour == 18)
            {
                return new ErrorResult(Messages.MaintenanceTime);
            }
            _rentalDal.Add(rental);
            return new SuccessResult(Messages.Succeed);
        }
        [CacheRemoveAspect("IRentalService.Get")]
        [SecuredOperation("delete,admin")]
        [ValidationAspect(typeof(RentalValidator))]
        public IResult Delete(Rental rental)
        {
            if (DateTime.Now.Hour == 18)
            {
                return new ErrorResult(Messages.MaintenanceTime);
            }
            _rentalDal.Delete(rental);
            return new SuccessResult(Messages.Succeed);
        }

        [CacheAspect]
        [SecuredOperation("list,admin")]
        public IDataResult<List<Rental>> GetAll()
        {
            if (DateTime.Now.Hour == 18)
            {
                return new ErrorDataResult<List<Rental>>(Messages.MaintenanceTime);
            }
            return new SuccessDataResult<List<Rental>>(_rentalDal.GetAll(), Messages.Succeed);
        }
        [CacheAspect]
        [SecuredOperation("list,admin")]
        public IDataResult<Rental> GetRentalById(int id)
        {
            if (DateTime.Now.Hour == 18)
            {
                return new ErrorDataResult<Rental>(Messages.MaintenanceTime);
            }
            return new SuccessDataResult<Rental>(_rentalDal.Get(c => c.Id == id), Messages.Succeed);
        }

        public IResult Transaction(Rental rental)
        {
            throw new NotImplementedException();
        }

        [CacheRemoveAspect("ICarService.Get")]
        [SecuredOperation("update,admin")]
        [ValidationAspect(typeof(RentalValidator))]

        public IResult Update(Rental rental)
        {
            if (DateTime.Now.Hour == 18)
            {
                return new ErrorResult(Messages.MaintenanceTime);
            }
            _rentalDal.Update(rental);
            return new SuccessResult(Messages.Succeed);
        }
    }
}
