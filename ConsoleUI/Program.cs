using Business.Concrete;
using DataAccess.Conctrete.EfMemory;
using DataAccess.Conctrete.InMemory;
using Entities.Concrete;
using System;
namespace ReCapProject
{
	class Program
	{
		static void Main(string[] args)
        {
            BrandManager brandManager = new BrandManager(new EfBrandDal());
            CarManager manager = new CarManager(new EfCarDal());
            ColorManager colorManager = new ColorManager(new EfColorDal());
            //AddingNewColor(colorManager);
            //manager.Add(new Car { BrandId = 2, Id = 3, ColorId = 1, DailyPrice = 400, Description = "Volvo S90", ModelYear = "2019" });
            //brandManager.Add(new Brand
            //{
            //    BrandId = 3,
            //    BrandName = "Mercedes"
            //});

            foreach (var item in manager.GetCarDetails())
            {
                Console.WriteLine(item.ColorName + "\t " + item.BrandName + "\t" + item.ModelYear + "\t" + item.DailyPrice);
            }
            //foreach (var item in colorManager.GetAllColors())
            //{
            //    Console.WriteLine(item.ColorName + " \t" + item.ColorId);
            //}
        }

        private static void AddingNewColor(ColorManager colorManager)
        {
            colorManager.Add(new Color
            {
                ColorId = 1,
                ColorName = "Beyaz"
            });
        }
    }
}