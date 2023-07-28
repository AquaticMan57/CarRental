using Core.Entities.Concrete;
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
    public interface IUserService
    {
        IResult DeleteById(int id);
        IResult Delete(User user);
        IResult Update(UserForUpdateDto userForUpdateDto);
        IResult UpdatePassword(UserForChangePasswordDto userForChangePasswordDto);
        IResult Add(User user);
        IDataResult<User> GetUserById(int id);
        IDataResult<List<User>> GetAll();
        IDataResult<List<OperationClaim>> GetOperationClaims(User user);
        IDataResult<User> GetByMail(string mail);
        IResult Transaction(User user);
        IDataResult<User> GetUserByUserName(string userName);
        IDataResult<List<UserDetailDto>> GetUserDetailsByUserId(int userId);
        IDataResult<List<UserDetailDto>> GetUserDetailsByCustomerId(int customerId);
        IDataResult<List<UserDetailDto>> GetAllUserDetails();

    }
}
