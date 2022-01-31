using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YotorBackendEF.Contracts;
using YotorContext.Models;

namespace YotorBackendEF.Repositories
{
    public class CarRepository : ICarRepository
    {
        private readonly YotorDbContext _yotorDbContext;
        public CarRepository(YotorDbContext yotorDbContext)
        {
            _yotorDbContext = yotorDbContext;
        }
        public async Task<IEnumerable<Car>> GetCarsAsync()
        {
            return await _yotorDbContext.Cars.ToListAsync();
        }
    }
}
