using Castle.Core.Resource;
using Core.DataAccess.EntityFramework;
using Core.Entities.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTO_s;
using FluentValidation.Validators;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.ConstrainedExecution;
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
                             on c.ColorId equals co.ColorId
                             select new CarDetailDto
                             {
                                 CarId = c.Id,
                                 BrandId = b.BrandId,
                                 ColorId = c.Id,
                                 BrandName = b.BrandName,
                                 ColorName = co.ColorName,
                                 ModelYear = c.ModelYear,
                                 Description = c.Description,
                                 DailyPrice = c.DailyPrice,
                                 ImagePath = (from ci in context.CarImages where c.Id == ci.CarId select ci.ImagePath).FirstOrDefault()!

                             };
                return result.ToList();
            }

        }
        public List<CarDetailDto> GetCarsDetailByBrandId(int brandId)
        {
            using (NorthwindContext context = new NorthwindContext())
            {
                var result = from car in context.Cars
                             join b in context.Brands
                             on car.BrandId equals b.BrandId
                             join c in context.Colors
                             on car.ColorId equals c.ColorId
                             where b.BrandId == brandId
                             select new CarDetailDto
                             {
                                 CarId = car.Id,
                                 BrandId = b.BrandId,
                                 ColorId = c.ColorId,
                                 BrandName = b.BrandName,
                                 ColorName = c.ColorName,
                                 ModelYear = car.ModelYear,
                                 Description = car.Description,
                                 DailyPrice = car.DailyPrice,
                                 ImagePath = (from ci in context.CarImages where car.Id == ci.CarId select ci.ImagePath).FirstOrDefault()!
                             };
                return result.ToList();
            }
        }

        public List<CarDetailDto> GetCarsDetailByColorId(int colorId)
        {
            using (var context = new NorthwindContext())
            {
                var result = from car in context.Cars
                             join b in context.Brands
                             on car.BrandId equals b.BrandId
                             join co in context.Colors
                             on car.ColorId equals co.ColorId
                             where co.ColorId == colorId
                             select new CarDetailDto
                             {
                                 CarId = car.Id,
                                 ColorId = co.ColorId,
                                 BrandId = b.BrandId,
                                 BrandName = b.BrandName,
                                 ColorName = co.ColorName,
                                 ModelYear = car.ModelYear,
                                 Description = car.Description,
                                 DailyPrice = car.DailyPrice,
                                 ImagePath = (from ci in context.CarImages where car.Id == ci.CarId select ci.ImagePath).FirstOrDefault()!
                             };
                return result.ToList();
            }


        }
        public List<CarDetailDto> GetCarsDetailBrandAndColorId(int brandId, int colorId)
        {
            using (var context = new NorthwindContext())
            {
                var result = from c in context.Cars
                             join co in context.Colors
                             on c.ColorId equals co.ColorId
                             join b in context.Brands
                             on c.BrandId equals b.BrandId
                             where b.BrandId == brandId && c.Id == colorId
                             select new CarDetailDto
                             {
                                 CarId = c.Id,
                                 ColorId = co.ColorId,
                                 BrandId = b.BrandId,
                                 BrandName = b.BrandName,
                                 ColorName = co.ColorName,
                                 DailyPrice = c.DailyPrice,
                                 Description = c.Description,
                                 ModelYear = c.ModelYear,
                                 ImagePath = (from ci in context.CarImages where c.Id == ci.CarId select ci.ImagePath).FirstOrDefault()!,


                             };
                return result.ToList();
            }
        }

        public List<CarDetailDto> GetCarDetailByCarId(int carId)
        {
            using (var context = new NorthwindContext())
            {
                var result = from c in context.Cars
                             join co in context.Colors
                             on c.ColorId equals co.ColorId
                             join b in context.Brands
                             on c.BrandId equals b.BrandId
                             join ci in context.CarImages
                             on c.Id equals ci.CarId
                             where c.Id == carId
                             select new CarDetailDto
                             {
                                 CarId = c.Id,
                                 ColorId = co.ColorId,
                                 BrandId = b.BrandId,
                                 BrandName = b.BrandName,
                                 ColorName = co.ColorName,
                                 DailyPrice = c.DailyPrice,
                                 Description = c.Description,
                                 ModelYear = c.ModelYear,
                                 ImagePath = ci.ImagePath,


                             };
                return result.ToList();
            }
        }
    }


}

