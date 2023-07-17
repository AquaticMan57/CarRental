using Core.DataAccess;
using Core.Entities.Concrete;
using Entities.Concrete;
using Entities.DTO_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IUserDal : IEntityRepository<User>
    {
        List<OperationClaim> GetClaims(User user);
        List<UserDetailDto> GetUserDtoByUserId(int userId);
        List<UserDetailDto> GetUserDtoByCustomerId(int customerId);
        List<UserDetailDto> GetUserDtos();
    }
}
