using Business.Abstract;
using Business.Constants.Messages;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Conctrete.EfMemory;
using Entities.Concrete;
using System;
using System.Collections.Generic;
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
        public IResult Add(User user)
        {
            if (DateTime.Now.Hour == 18)
            {
                return new ErrorResult(Messages.MaintenanceTime);
            }
            _userDal.Add(user);
            return new SuccessResult(Messages.Succeed);
        }

        public IResult Delete(User user)
        {
            if (DateTime.Now.Hour == 18)
            {
                return new ErrorResult(Messages.MaintenanceTime);
            }
            _userDal.Delete(user);
            return new SuccessResult(Messages.Succeed);
        }

        public IDataResult<List<User>> GetAll()
        {
            if (DateTime.Now.Hour == 18)
            {
                return new ErrorDataResult<List<User>>(Messages.MaintenanceTime);
            }
            return new SuccessDataResult<List<User>>(_userDal.GetAll(), Messages.Succeed);
        }

        public IDataResult<User> GetUserById(int id)
        {
            if (DateTime.Now.Hour == 18)
            {
                return new ErrorDataResult<User>(Messages.MaintenanceTime);
            }
            return new SuccessDataResult<User>(_userDal.Get(c => c.Id == id), Messages.Succeed);
        }

        public IResult Update(User user)
        {
            if (DateTime.Now.Hour == 18)
            {
                return new ErrorResult(Messages.MaintenanceTime);
            }
            _userDal.Update(user);
            return new SuccessResult(Messages.Succeed);
        }
    }
}
