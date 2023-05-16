using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants.Messages;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Validation;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTO_s;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class UserManager : IUserService
    {
        IUserDal _userDal;
        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }

        [ValidationAspect(typeof(UserValidator))]
        public IResult Add(User user)
        {
            _userDal.Add(user);
            return new SuccessResult(UserMessages.UserAdded);
        }

        [ValidationAspect(typeof(UserValidator))]
        [CacheRemoveAspect("IUserService.Get")]
        [SecuredOperation("delete,admin")]
        public IResult Delete(User user)
        {
            _userDal.Delete(user);
            return new SuccessResult(UserMessages.UserDeleted);
        }

        
        //[CacheAspect]
        //[SecuredOperation("list,admin")]
        public IDataResult<List<User>> GetAll()
        {
            if (DateTime.Now.Hour == 05)
            {
                return new ErrorDataResult<List<User>>(Messages.MaintenanceTime);
            }
            return new SuccessDataResult<List<User>>(_userDal.GetAll(), Messages.Succeed);
        }
        public IDataResult<List<UserDetailDto>> GetUserByCarId(int carId)
        {
            var result = _userDal.GetUserByCarId(carId);
            return new SuccessDataResult<List<UserDetailDto>>(result);
        }

        public IDataResult<User> GetByMail(string mail)
        {
            return new SuccessDataResult<User>(_userDal.Get(u=>u.Email == mail),Messages.Succeed);
        }

        
        
        public IDataResult<List<OperationClaim>> GetOperationClaims(User user)
        {
            var operationResult = _userDal?.GetClaims(user);
            return new SuccessDataResult<List<OperationClaim>>(operationResult, OperationClaimsMessage.OperationClaimsListed);
        }

        

        public IDataResult<List<User>> GetUserById(int id)
        {
            if (DateTime.Now.Hour == 05)
            {
                return new ErrorDataResult<List<User>>(Messages.MaintenanceTime);
            }
            return new SuccessDataResult<List<User>>(_userDal.GetAll(c => c.Id == id), Messages.Succeed);
        }

        public IResult Transaction(User user)
        {
            throw new NotImplementedException();
        }

        [ValidationAspect(typeof(UserValidator))]
        [CacheRemoveAspect("IUserService.Get")]
        [SecuredOperation("update,admin")]
        public IResult Update(User user)
        {
            _userDal.Update(user);
            return new SuccessResult(UserMessages.UserUpdated);
        }

        public IDataResult<User> GetUserByUserName(string userName)
        {
            var result = _userDal.Get(u => u.LastName + " " + u.FirstName == userName)!;
            if (result != null)
            {
                return new SuccessDataResult<User>(result);
            }
            return new ErrorDataResult<User>(Messages.InvalidNameError);
        }

        public IDataResult<List<UserDetailDto>> GetUserDetailsByUserId(int userId)
        {
            var result = _userDal.GetUserDtoByUserId(userId);
            if (result != null)
            {
                return new SuccessDataResult<List<UserDetailDto>>(result);
            }
            return new ErrorDataResult<List<UserDetailDto>>(UserMessages.UserNotFound);
        }

        public IDataResult<List<UserDetailDto>> GetAllUserDetails()
        {
            var result = _userDal.GetUserDtos();
            if (result != null)
            {
                return new SuccessDataResult<List<UserDetailDto>>(result);
            }
            return new ErrorDataResult<List<UserDetailDto>>(UserMessages.NotAvailable);
        }
    }
}
