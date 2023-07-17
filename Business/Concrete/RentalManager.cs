using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants.Messages;
using Business.ValidationRules.FluentValidation;
using Castle.Core.Internal;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.BusinessRules;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTO_s;
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

        //[CacheRemoveAspect("IRentalService.Get")]
        //[SecuredOperation("add,admin")]
        [ValidationAspect(typeof(RentalValidator))]

        public IResult Add(Rental rental)
        {

            IResult result = BusinessRules.Run
                (
                CheckIfDateCorrectlyAdded(rental.RentDate, rental.ReturnDate),
                CheckIfRentalBusy(rental.RentDate,rental.ReturnDate),
                CheckIfDateLaterThanNow(rental.RentDate,rental.ReturnDate)
                );
            if (result != null)
            {
                return new ErrorResult(result.Message);
            }
            _rentalDal.Add(rental);
            return new SuccessResult(Messages.Succeed);
        }
        //[CacheRemoveAspect("IRentalService.Get")]
        //[SecuredOperation("delete,admin")]
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

        //[CacheAspect]
        //[SecuredOperation("list,admin")]
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
        //[CacheAspect]
        //[SecuredOperation("list,admin")]
        public IDataResult<List<RentalDetailsDto>> GetRentalDetails()
        {
            var result = _rentalDal.GetRentalDetailsDto();
            if (result.IsNullOrEmpty())
            {
                return new ErrorDataResult<List<RentalDetailsDto>>(RentalMessages.NoDto);
            }
            return new SuccessDataResult<List<RentalDetailsDto>>(result,RentalMessages.RentalDetailDtoListed);
        }

        public IDataResult<List<RentalDetailsDto>> GetRentalDetailsDtosByCarId(int carId)
        {
            var result = _rentalDal.GetRentalDetailsDtosByCarId(carId);
            return new SuccessDataResult<List<RentalDetailsDto>>(result, Messages.Succeed);

        }

        public IResult Transaction(Rental rental)
        {
            throw new NotImplementedException();
        }

        //[CacheRemoveAspect("ICarService.Get")]
        //[SecuredOperation("update,admin")]
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
        private IResult CheckIfDateCorrectlyAdded(string rentDate, string returnDate)
        {
            string[] rentDateSplits = rentDate.Split('-');
            string[] returnDateSplits = returnDate.Split("-");
            string rentDateToCheck = "";
            string returnDateToCheck = "";

            foreach (var item in rentDateSplits)
            {
                rentDateToCheck += item;
            }
            foreach (var item in returnDateSplits)
            {
                returnDateToCheck += item;
            }

            if (Int64.Parse(rentDateToCheck) > Int64.Parse(returnDateToCheck))
            {
                return new ErrorResult(RentalMessages.WrongRentDate);
            }
            return new SuccessResult(Messages.Succeed);

        }
        private IResult CheckIfRentalBusy(string rentDate,string returnDate)
        {
            string[] rentDateSplits = rentDate.Split('-');
            string[] returnDateSplits = returnDate.Split("-");

            string rentDateToCheck = "";
            string returnDateToCheck = "";

            foreach (var item in rentDateSplits)
            {
                rentDateToCheck += item;
            }
            foreach (var item in returnDateSplits)
            {
                returnDateToCheck += item;
            }

            List<Rental> rentals = _rentalDal.GetAll();

            foreach (var rental in rentals)
            {
                string[] rentDateSplitsData = rental.RentDate.Split('-');
                string[] returnDateSplitsData = rental.ReturnDate!.Split("-");
                string rentDateToCheckData = "";
                string returnDateToCheckData = "";
                foreach (var item in rentDateSplitsData)
                {
                    rentDateToCheckData += item;
                }
                foreach (var item in returnDateSplitsData)
                {
                    returnDateToCheckData += item;
                }

                if (Int64.Parse(rentDateToCheck) > Int64.Parse(rentDateToCheckData) && Int64.Parse(rentDateToCheck) < Int64.Parse(returnDateToCheckData))
                {
                    return new ErrorResult(RentalMessages.RentalBusyAlert);
                }
                
            }
            return new SuccessResult(Messages.Succeed);
        }
        private IResult CheckIfDateLaterThanNow(string rentDate , string returnDate)
        {
            string[] rentDateSplits = rentDate.Split('-');

            string rentDateToCheck = "";

            string dateNow = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString();
            foreach (var item in rentDateSplits)
            {
                rentDateToCheck += item;
            }
            

            if (Int64.Parse(rentDateToCheck) > Int64.Parse(dateNow))
            {
                return new SuccessResult(Messages.Succeed);
            }
            return new ErrorResult(RentalMessages.RentalNotLaterThanNow);
        }
    }
}
