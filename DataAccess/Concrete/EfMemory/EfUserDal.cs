using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using Core.Utilities.Results;
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
        

        public List<UserDetailDto> GetUserDtoByCustomerId(int customerId)
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
                             where cu.CustomerId == customerId
                             && r.CustomerId == customerId
                             select new UserDetailDto
                             {
                                 CarId = customerId,
                                 CustomerId = r.CustomerId,
                                 Email = u.Email,
                                 UserId = u.Id,
                                 CompanyMail = cu.CompanyMail,
                                 CompanyName = cu.CompanyName,
                                 Status = u.Status,
                                 UserName = u.FirstName + " " + u.LastName,

                             };
                return result.ToList();
            }
        }

        public List<UserDetailDto> GetUserDtoByUserId(int userId)
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
                             where u.Id == userId
                             select new UserDetailDto
                             {
                                 CarId = c.Id,
                                 CustomerId = r.CustomerId,
                                 Email = u.Email,
                                 UserId = u.Id,
                                 UserName = u.FirstName + " " + u.LastName,
                                 Status = u.Status,
                                 CompanyName = cu.CompanyName,
                                 CompanyMail = cu.CompanyMail,

                             };
                return result.ToList();
            }
        }

        public List<UserDetailDto> GetUserDtos()
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
                             select new UserDetailDto
                             {
                                 CarId = c.Id,
                                 CustomerId = r.CustomerId,
                                 Email = u.Email,
                                 UserId = u.Id,
                                 UserName = u.FirstName + " " + u.LastName,
                                 Status = u.Status,
                                 CompanyName = cu.CompanyName
                                 
            
                             };
                return result.ToList();
            }

            
        }

        
    }
}
