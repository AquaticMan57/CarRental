using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTO_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EfMemory
{
    public class EfRentalDal : EfEntityRepositoryBase<Rental, NorthwindContext>, IRentalDal
    {
        public List<RentalDetailsDto> GetRentalDetailsDto()
        {
            using (NorthwindContext context = new NorthwindContext())
            {
                var result = from c in context.Cars
                             join b in context.Brands
                             on c.BrandId equals b.BrandId
                             join r in context.Rentals
                             on c.Id equals r.CarId
                             join co in context.Colors
                             on c.ColorId equals co.ColorId
                             join cus in context.Customers
                             on r.CustomerId equals cus.CustomerId
                             join u in context.Users
                             on cus.UserId equals u.Id
                             select new RentalDetailsDto
                             {

                                 BrandId = b.BrandId,
                                 BrandName = b.BrandName,
                                 CustomerId = cus.CustomerId,
                                 CarId = c.Id,
                                 ColorId = c.ColorId,
                                 ColorName = co.ColorName,
                                 Description = c.Description,
                                 RentalId = r.Id,
                                 RentDate = r.RentDate,
                                 ReturnDate = r.ReturnDate,
                                 UserName = u.FirstName + " " + u.LastName,
                             };
                return result.ToList();
            }
        }
        public List<RentalDetailsDto> GetRentalDetailsDtosByCarId(int carId)
        {
            using (var context = new NorthwindContext())
            {
                var result = from c in context.Cars
                             join b in context.Brands
                             on c.BrandId equals b.BrandId
                             join r in context.Rentals
                             on c.Id equals r.CarId
                             join co in context.Colors
                             on c.ColorId equals co.ColorId
                             join cus in context.Customers
                             on r.CustomerId equals cus.CustomerId
                             join u in context.Users
                             on cus.UserId equals u.Id
                             where c.Id == carId
                             select new RentalDetailsDto
                             {
                                 CarId = carId,
                                 BrandId = b.BrandId,
                                 BrandName = b.BrandName,
                                 ColorId = c.ColorId,
                                 ColorName = co.ColorName,
                                 CustomerId = cus.CustomerId,
                                 Description = c.Description,
                                 RentalId = r.Id,
                                 RentDate = r.RentDate,
                                 ReturnDate = r.ReturnDate,
                                 UserName = u.FirstName + " " + u.LastName,



                             };

                return result.ToList();
            }
        }
        
    }
}
