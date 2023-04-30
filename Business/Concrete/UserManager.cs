using Business.Abstract;
using Business.BusinessAspect;
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
        [CacheRemoveAspect("IUserService.Get")]
        [SecuredOperation("add,admin")]
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

        
        [CacheAspect]
        [SecuredOperation("list,admin")]
        public IDataResult<List<User>> GetAll()
        {
            if (DateTime.Now.Hour == 18)
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
        [CacheAspect]
        [SecuredOperation("list,admin")]
        public IDataResult<User> GetByMail(string mail)
        {
            return new SuccessDataResult<User>(_userDal.Get(u=>u.Email == mail),Messages.Succeed);
        }

        
        [CacheAspect]
        [SecuredOperation("list,admin")]
        public IDataResult<List<OperationClaim>> GetOperationClaims(User user)
        {
            var operationResult = _userDal.GetOperationClaims(user);
            return new SuccessDataResult<List<OperationClaim>>(operationResult, OperationClaimsMessage.OperationClaimsListed);
        }

        

        [CacheAspect]
        [SecuredOperation("list,admin")]
        public IDataResult<User> GetUserById(int id)
        {
            if (DateTime.Now.Hour == 18)
            {
                return new ErrorDataResult<User>(Messages.MaintenanceTime);
            }
            return new SuccessDataResult<User>(_userDal.Get(c => c.Id == id), Messages.Succeed);
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

        
    }
}
