using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YotorContext.Models;

namespace YotorResources.Contracts
{
    public interface ICarRepository
    {
        Task<IEnumerable<Car>> GetCarsAsync();
        Task<Car> GetCarAsync(int id);
        Task CreateCarAsync(int organization_id, string model, string brand, string year, string transmission, string address, bool status, string type, int price, string phote, string description, string number);
        Task UpdateCarAsync(int id, string model, string brand, string year, string transmission, string address, bool status, string type, int price, string phote, string description, string number);
        Task<IEnumerable<Car>> GetMostPopularCarsAsync();


    }
}
