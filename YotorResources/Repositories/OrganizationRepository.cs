using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YotorContext.Models;
using YotorResources.Contracts;

namespace YotorResources.Repositories
{
    public class OrganizationRepository : IOrganizationRepository
    {
        private readonly YotorDbContext _yotorDbContext;
        public OrganizationRepository(YotorDbContext yotorDbContext)
        {
            _yotorDbContext = yotorDbContext;
        }

        public async Task CreateOrganizationAsync(Organization organization)
        {
            await _yotorDbContext.AddAsync(organization);
            await _yotorDbContext.SaveChangesAsync();
        }

        public async Task DeleteOrganizationAsync(int id)
        {
            var organization = await _yotorDbContext.Organizations.Where(o => o.OrganizationId == id).FirstOrDefaultAsync();
            if (organization != null)
            {
                _yotorDbContext.Organizations.Remove(organization);
                await _yotorDbContext.SaveChangesAsync();
            }
        }

        public async Task EditOrganizationAsync(int id, Organization organization)
        {
            var newOrganization = await _yotorDbContext.Organizations.FirstOrDefaultAsync(o => o.OrganizationId == id);
            newOrganization.Name = organization.Name;
            newOrganization.Email = organization.Email;
            newOrganization.Phone = organization.Phone;
            newOrganization.Code = organization.Code;
            newOrganization.Taxes = organization.Taxes;
            newOrganization.Address = organization.Address;
            newOrganization.Founder = organization.Founder;
            newOrganization.Account = organization.Account;
            _yotorDbContext.Entry(newOrganization).State = EntityState.Modified;
            await _yotorDbContext.SaveChangesAsync();

        }

        public async Task<int> GetCountOfOrganizationsAsync()
        {
            return await _yotorDbContext.Organizations.CountAsync();
        }

        public async Task<Organization> GetOrganizationAsync(int id)
        {
            return await _yotorDbContext.Organizations.Where(o => o.OrganizationId == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Organization>> GetOrganizationsAsync()
        {
            return await _yotorDbContext.Organizations.ToListAsync();
        }
    }
}
