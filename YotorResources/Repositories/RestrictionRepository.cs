using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YotorContext.Models;
using YotorResources.Contracts;

namespace YotorResources.Repositories
{
    public class RestrictionRepository : IRestrictionRepository
    {
        private readonly YotorDbContext _yotorDbContext;
        public RestrictionRepository(YotorDbContext yotorDbContext)
        {
            _yotorDbContext = yotorDbContext;
        }

        public async Task CreateRestrictionAsync(int landlord_id, string car_name, string description)
        {
            var restriction = new Restriction(landlord_id, car_name, description);
            await _yotorDbContext.AddAsync(restriction);
            await _yotorDbContext.SaveChangesAsync();
        }

        public async Task DeleteRestrictionAsync(int id)
        {
            var restriction = await _yotorDbContext.Restrictions.Where(r => r.RestrictionId == id).FirstOrDefaultAsync();
            if (restriction != null)
            {
                _yotorDbContext.Remove(restriction);
                await _yotorDbContext.SaveChangesAsync();
            }
        }

        public async Task<Restriction> GetRestrictionAsync(int id)
        {
            return await _yotorDbContext.Restrictions.Where(r => r.RestrictionId == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Restriction>> GetRestrictionsAsync()
        {
            return await _yotorDbContext.Restrictions.ToListAsync();
        }

        public async Task<IEnumerable<Restriction>> GetRestrictionsOfSomePersonAsync(int id)
        {
            var restrictions = await(from Restriction in _yotorDbContext.Restrictions
                                 join Landlord in _yotorDbContext.Landlords on Restriction.LandlordId equals Landlord.LandlordId
                                 where
                                 Landlord.UserId == id
                                 select new Restriction
                                 {
                                     RestrictionId = Restriction.RestrictionId,
                                     LandlordId = Restriction.LandlordId,
                                     CarName = Restriction.CarName,
                                     Description = Restriction.Description
                                 }).ToListAsync();

            return restrictions;
        }

        public async Task UpdateRestrictionAsync(int id, Restriction restriction)
        {
            var newRestiction = await _yotorDbContext.Restrictions.FirstOrDefaultAsync(r => r.RestrictionId == id);
            newRestiction.LandlordId = restriction.LandlordId;
            newRestiction.CarName = restriction.CarName;
            newRestiction.Description = restriction.Description;

            _yotorDbContext.Entry(newRestiction).State = EntityState.Modified;
            await _yotorDbContext.SaveChangesAsync();
        }
    }
}
