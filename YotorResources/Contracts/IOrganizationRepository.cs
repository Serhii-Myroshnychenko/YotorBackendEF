using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YotorContext.Models;

namespace YotorResources.Contracts
{
    public interface IOrganizationRepository
    {
        Task<IEnumerable<Organization>> GetOrganizationsAsync();
        Task<Organization> GetOrganizationAsync(int id);
        Task EditOrganizationAsync(int id, Organization organization);
        Task CreateOrganizationAsync(Organization organization);
        Task DeleteOrganizationAsync(int id);
        Task<int> GetCountOfOrganizationsAsync();
    }
}
