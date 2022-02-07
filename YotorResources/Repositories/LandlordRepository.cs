using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YotorContext.Models;
using YotorResources.Contracts;

namespace YotorResources.Repositories
{
    public class LandlordRepository : ILandlordRepository
    {
        private readonly YotorDbContext _yotorDbContext;
        public LandlordRepository(YotorDbContext yotorDbContext)
        {
            _yotorDbContext = yotorDbContext;
        }

        public async Task CreateLandlordAsync(int user_id, int organization_id, string name)
        {
            var landlord = new Landlord(user_id, organization_id, name);
            await _yotorDbContext.AddAsync(landlord);
            await _yotorDbContext.SaveChangesAsync();
        }

        public async Task<Landlord> GetLandlordAsync(int id)
        {
            return await _yotorDbContext.Landlords.Where(l => l.LandlordId == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Landlord>> GetLandlordsAsync()
        {
            return await _yotorDbContext.Landlords.ToListAsync();
        }

        public async Task UpdateLandlordAsync(int id, Landlord landlord)
        {
            var newLandlord = await _yotorDbContext.Landlords.FirstOrDefaultAsync(l => l.LandlordId == id);
            newLandlord.UserId = landlord.UserId;
            newLandlord.OrganizationId = landlord.OrganizationId;
            newLandlord.Name = landlord.Name;
            
            _yotorDbContext.Entry(newLandlord).State = EntityState.Modified;
            await _yotorDbContext.SaveChangesAsync();
        }
    }
}
