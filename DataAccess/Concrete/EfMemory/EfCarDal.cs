using Castle.Core.Resource;
using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTO_s;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EfMemory

{
    public class EfCarDal : EfEntityRepositoryBase<Car, NorthwindContext>, ICarDal
    {
        public List<CarDetailDto> GetCarsDetail()
        {
            using (NorthwindContext context = new NorthwindContext())
            {
                var result = from c in context.Cars
                             join b in context.Brands
                             on c.BrandId equals b.BrandId
                             join co in context.Colors
                             on c.ColorId equals co.Id
                             select new CarDetailDto 
                             {
                                 CarId=c.Id,
                                 BrandName=b.BrandName,
                                 DailyPrice=c.DailyPrice,
                                 
                                 ModelYear=c.ModelYear,
                                 ColorName=co.ColorName
                                 
                             };
                return result.ToList();
            }
        }
    }
}
