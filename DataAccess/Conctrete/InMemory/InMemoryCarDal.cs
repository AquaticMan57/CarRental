using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTO_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Conctrete.InMemory
{
    public class InMemoryCarDal : ICarDal

    {
        List<Car> _cars;
        
        public InMemoryCarDal() { 

            _cars = new List<Car>
            {
                new Car {ModelYear="1994",Id=1,Description="Tofas Sahin",DailyPrice=100,ColorId=1,BrandId=1},
                new Car {ModelYear="2008",Id=2,Description="Volkswagen Caddy",DailyPrice=250,ColorId=1,BrandId=2},
                new Car {ModelYear="2016",Id=3,Description="Volkswagen Tiguan",DailyPrice=350,ColorId=1,BrandId=2},
                new Car {ModelYear="2018",Id=4,Description="Volkswagen Passat",DailyPrice=375,ColorId=2,BrandId=2},
                new Car {ModelYear="2019",Id=5,Description="Fiat Egea",DailyPrice=300,ColorId=3,BrandId=3}

            };
            
        
        }
        public void Add(Car car)
        {
            _cars.Add(car);
        }

        public void Delete(Car car)
        {
            var result = _cars.SingleOrDefault(c=> c.Id == car.Id);
            _cars.Remove(car);
        }

        public Car Get(Expression<Func<Car, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public List<Car> GetAll(Expression<Func<Car, bool>> filter = null)
        {
            throw new NotImplementedException();
        }

        public List<Car> GetByAllByBrandId(int id)
        {
            return _cars.Where(c=>c.BrandId== id).ToList();
        }

        public Car GetById(int id)
        {
            var result = _cars.SingleOrDefault(c => c.Id == id);
            if (result != null)
            {
                return result;
            }
            return null;
            
        }

        public List<Car> GetCars()
        {
            return _cars;
        }

        public List<CarDetailDto> GetCarsDetail()
        {
            throw new NotImplementedException();
        }

        public void Update(Car car)
        {
            Car _carToUpdate = _cars.SingleOrDefault(c => c.Id == car.Id);
            if (_carToUpdate != null)
            {
                _carToUpdate.DailyPrice = car.DailyPrice;
                _carToUpdate.BrandId = car.BrandId;
                _carToUpdate.ColorId = car.ColorId;
                _carToUpdate.ModelYear = car.ModelYear;
                _carToUpdate.Description = car.Description;

            }

        }
    }
}
