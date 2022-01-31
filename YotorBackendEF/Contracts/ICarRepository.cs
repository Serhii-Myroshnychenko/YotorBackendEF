using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YotorContext.Models;

namespace YotorBackendEF.Contracts
{
    public interface ICarRepository
    {
        public Task<IEnumerable<Car>> GetCarsAsync();
    }
}
