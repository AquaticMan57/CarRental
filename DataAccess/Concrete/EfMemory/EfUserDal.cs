using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTO_s;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EfMemory
{
    public class EfUserDal : EfEntityRepositoryBase<User, NorthwindContext>, IUserDal
    {
        public List<OperationClaim> GetClaims(User user)
        {
            using (var context = new NorthwindContext())
            {
                var result = from operationClaim in context.OperationClaims
                             join userOperationClaim in context.UserOperationClaims
                                 on operationClaim.Id equals userOperationClaim.OperationClaimId
                             where userOperationClaim.UserId == user.Id
                             select new OperationClaim { Id = operationClaim.Id, Name = operationClaim.Name };
                return result.ToList();

            }
        }
        public List<UserDetailDto> GetUserByCarId(int carId)
        {
            using (var context = new NorthwindContext())
            {
                var result = from u in context.Users
                             join cu in context.Customers
                             on u.Id equals cu.UserId
                             join r in context.Rentals
                             on cu.CustomerId equals r.CustomerId
                             join c in context.Cars
                             on r.CarId equals c.Id
                             where c.Id == carId
                             select new UserDetailDto
                             {
                                 CarId = carId,
                                 CustomerId = r.CustomerId,
                                 Email = u.Email,
                                 UserId = u.Id,
                                 UserName = u.FirstName +" "+ u.LastName,

                             };
                return result.ToList();

            }
        }
    }
}
