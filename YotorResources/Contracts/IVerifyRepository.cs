﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YotorContext.Models;

namespace YotorResources.Contracts
{
    public interface IVerifyRepository
    {
        Task<Landlord> IsLandlordAsync(int id);
        Task<bool> IsAdminAsync(int id);
        Task<bool> IsUserAsync(int id);
        Task<bool> IsOrganizationAsync(int id);
        Task<Landlord> IsThisCarOfHisOrganizationAsync(string name);
        Task<Restriction> GetRestrictionByCarNameAsync(string name);
        Task<Car> GetCarByCarNameAsync(string name);
        Task UpdateStatusCarAsync(int id);
        Task<Customer> GetCustomerByNameAsync(string name);
        Task<Organization> GetOrganizationByNameAsync(string name);
        Task<Customer> GetCustomerByIdAsync(int id);
    }
}
