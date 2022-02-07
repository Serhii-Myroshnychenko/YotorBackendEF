using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YotorContext.Models;
using YotorResources.Contracts;

namespace YotorResources.Repositories
{
    public class VerifyRepository : IVerifyRepository
    {
        private readonly YotorDbContext _yotorDbContext;
        public VerifyRepository(YotorDbContext yotorDbContext)
        {
            _yotorDbContext = yotorDbContext;
        }

        public async Task<Car> GetCarByCarNameAsync(string name)
        {
            return await _yotorDbContext.Cars.Where(c => c.Model == name && c.Status == true).FirstOrDefaultAsync();
        }

        public async Task<Customer> GetCustomerByIdAsync(int id)
        {
            return await _yotorDbContext.Customers.Where(c => c.UserId == id).FirstOrDefaultAsync();
        }

        public async Task<Customer> GetCustomerByNameAsync(string name)
        {
            return await _yotorDbContext.Customers.Where(c => c.FullName == name).FirstOrDefaultAsync();
        }

        public async Task<Organization> GetOrganizationByNameAsync(string name)
        {
            return await _yotorDbContext.Organizations.Where(o => o.Name == name).FirstOrDefaultAsync();
        }

        public async Task<Restriction> GetRestrictionByCarNameAsync(string name)
        {
            return await _yotorDbContext.Restrictions.Where(r => r.CarName == name).FirstOrDefaultAsync();
        }

        public async Task<bool> IsAdminAsync(int id)
        {
            var user = await _yotorDbContext.Customers.Where(c => c.UserId == id && c.IsAdmin == true).FirstOrDefaultAsync();
            if (user != null)
            {
                return true;
            }
            return false;
        }

        public async Task<Landlord> IsLandlordAsync(int id)
        {
            return await _yotorDbContext.Landlords.Where(l => l.UserId == id).FirstOrDefaultAsync();
        }

        public async Task<bool> IsOrganizationAsync(int id)
        {
            var organization = await _yotorDbContext.Organizations.Where(o => o.OrganizationId == id).FirstOrDefaultAsync();
            if (organization != null)
            {
                return true;
            }
            return false;
        }

        public async Task<Landlord> IsThisCarOfHisOrganizationAsync(string name)
        {
            var landlord = await (from Car in _yotorDbContext.Cars
                       join Landlord in _yotorDbContext.Landlords on Car.OrganizationId equals Landlord.OrganizationId
                       where
                       Car.Model == name
                       select new Landlord
                       {
                            LandlordId = Landlord.LandlordId,
                            UserId = Landlord.UserId,
                            OrganizationId = Landlord.OrganizationId,
                            Name = Landlord.Name
                       }).FirstOrDefaultAsync();

            return landlord;
        }

        public async Task<bool> IsUserAsync(int id)
        {
            var user = await _yotorDbContext.Customers.Where(c => c.UserId == id).FirstOrDefaultAsync();
            if (user != null)
            {
                return true;
            }
            return false;
        }

        public async Task UpdateStatusCarAsync(int id)
        {
            var car = await _yotorDbContext.Cars.FirstOrDefaultAsync(c => c.CarId == id);
            car.Status = false;
            _yotorDbContext.Entry(car).State = EntityState.Modified;
            await _yotorDbContext.SaveChangesAsync();
        }
    }
}
