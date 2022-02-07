using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YotorContext.Models;

namespace YotorBackendEF.Contracts
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<Customer>> GetCustomersAsync();
        Task<Customer> GetCustomerAsync(string email, string password);
        Task RegistrationAsync(string full_name, string email, string phone, string password, bool is_admin);
      


    }
}
