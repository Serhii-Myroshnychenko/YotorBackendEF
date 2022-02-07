using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YotorContext.Models;
using YotorResources.Contracts;

namespace YotorResources.Repositories
{
    public class CarRepository : ICarRepository

    { 
        private readonly YotorDbContext _yotorDbContext;
        public CarRepository(YotorDbContext yotorDbContext)
        {
            _yotorDbContext = yotorDbContext;
        }

        public async Task CreateCarAsync(int organization_id, string model, string brand, string year, string transmission, string address, bool status, string type, int price, string photo, string description, string number)
        {
            var car = new Car(organization_id, model, brand, year, transmission, address, status, type, price, photo, description, number);
            await _yotorDbContext.AddAsync(car);
            await _yotorDbContext.SaveChangesAsync();
        }

        public async Task<Car> GetCarAsync(int id)
        {
            return await _yotorDbContext.Cars.Where(c => c.CarId == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Car>> GetCarsAsync()
        {
            return await _yotorDbContext.Cars.ToListAsync();
        }

        public async Task UpdateCarAsync(int id, string model, string brand, string year, string transmission, string address, bool status, string type, int price, string photo, string description, string number)
        {
            var car = await _yotorDbContext.Cars.FirstOrDefaultAsync(c => c.CarId == id);
            car.Model = model;
            car.Brand = brand;
            car.Year = year;
            car.Transmission = transmission;
            car.Address = address;
            car.Status = status;
            car.Type = type;
            car.Price = price;
            car.Photo = photo;
            car.Description = description;
            car.Number = number;
            _yotorDbContext.Entry(car).State = EntityState.Modified;
            await _yotorDbContext.SaveChangesAsync();

        }
    }
}
