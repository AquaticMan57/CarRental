using Business.Abstract;
using Business.Concrete;
using Business.Constants;
using Core.Entities.Concrete;
using DataAccess.Concrete.EfMemory;
using Entities.Concrete;
using System;
namespace ReCapProject
{
	class Program
	{
		static void Main(string[] args)
        {
            BrandManager brandManager = new BrandManager(new EfBrandDal());
            CarManager carManager = new CarManager(new EfCarDal());
            
            RentalManager rentalManager = new RentalManager(new EfRentalDal());
            UserManager userManager = new UserManager(new EfUserDal());
            CustomerManager customerManager = new CustomerManager(new EfCustomerDal());

            //DataTableAddingSystem();
            //customerManager.Add(new Customer
            //{
            //    CustomerId = 2,
            //    CompanyName ="Savas ltd sti"

            //});
            ColorManager colorManager = new ColorManager(new EfColorDal());

            var result = brandManager.Delete(new Brand
            {
                BrandId = 3,
                BrandName = "Volvo",
                
                
            });

            if (result.Success)
            {
                Console.WriteLine(result);
                foreach (var item in brandManager.GetAll().Data)
                {
                    Console.WriteLine(item.BrandName);
                }
            }
            else
            {
                Console.WriteLine(result.Message);
            }


            //rentalManager.Delete(new Rental
            //{
            //    Id = 3,
            //    CarId = 3,
            //    CustomerId = 3,
            //    RentDate = new DateTime(2023, 05, 05),
            //});




            //AddingNewColor(colorManager);
            //manager.Add(new Car { BrandId = 2, Id = 3, ColorId = 1, DailyPrice = 400, Description = "Volvo S90", ModelYear = "2019" });
            //brandManager.Add(new Brand
            //{
            //    BrandId = 3,
            //    BrandName = "Mercedes"
            //});
            // GetCarDetails();

            //foreach (var item in colorManager.GetAllColors())
            //{
            //    Console.WriteLine(item.ColorName + " \t" + item.ColorId);
            //}
            Console.ReadLine();
        }

        //private static void DataTableAddingSystem()
        //{
        //    BrandManager brandManager = new BrandManager(new EfBrandDal());
        //    CarManager carManager = new CarManager(new EfCarDal());
        //    ColorManager colorManager = new ColorManager(new EfColorDal());
        //    RentalManager rentalManager = new RentalManager(new EfRentalDal());
        //    UserManager userManager = new UserManager(new EfUserDal());
        //    CustomerManager customerManager = new CustomerManager(new EfCustomerDal());
        //    int i = 1;
        //    while (true)
        //    {
                
        //        string userFirstName="";
        //        string userLastName = "";
        //        string Email = "";
        //        string Password = "";
        //        Console.WriteLine("Isminizi giriniz :");
        //        userFirstName = Console.ReadLine();
        //        Console.WriteLine("Soyadinizi giriniz :");
        //        userLastName = Console.ReadLine();
        //        Console.WriteLine("Email giriniz :");
        //        Email = Console.ReadLine();
        //        Console.WriteLine("Sifre giriniz :");
        //        Password = Console.ReadLine();




        //        userManager.Add(new User()
        //        {
        //            Email = Email,
        //            FirstName = userFirstName,
        //            LastName = userLastName,
        //            Id = i,
        //            Password = Password
        //        });
        //        i++;
        //    }

            




        }

        //private static void GetCarDetails()
        //{
        //    CarManager manager = new CarManager(new EfCarDal());
        //    if (manager.GetCarDetails().Success == true)
        //    {
        //        foreach (var item in manager.GetCarDetails().Data)
        //        {
        //            Console.WriteLine(item.ColorName + "\t " + item.BrandName + "\t" + item.ModelYear + "\t" + item.DailyPrice);
        //        }
        //    }
        //    else
        //    {
        //        Console.WriteLine(manager.GetCarDetails().Message);
        //    }
        //}

        //private static void AddingNewColor(ColorManager colorManager)
        //{
        //    colorManager.Add(new Colors
        //    {
        //        Id = 1,
        //        ColorName = "Beyaz"
        //    });
        //}
    }
