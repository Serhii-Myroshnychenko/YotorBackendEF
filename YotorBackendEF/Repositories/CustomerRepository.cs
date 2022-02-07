using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YotorBackendEF.Contracts;
using YotorBackendEF.Models;
using YotorContext.Models;

namespace YotorBackendEF.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly YotorDbContext _yotorDbContext;

        public CustomerRepository(YotorDbContext yotorDbContext)
        {
            _yotorDbContext = yotorDbContext;
        }
        

        public async Task<Customer> GetCustomerAsync(string email, string password)
        {
            var customer =  await _yotorDbContext.Customers.Where(c => c.Email == email).FirstOrDefaultAsync();
            if (customer != null)
            {
                bool isValid = BCrypt.Net.BCrypt.Verify(password, customer.Password);
                if (isValid)
                {
                    return customer;
                }
            }
            return null;
        }

        public async Task<IEnumerable<Customer>> GetCustomersAsync()
        {
            
            return await _yotorDbContext.Customers.ToListAsync();
        }
        

        public async Task RegistrationAsync(string full_name, string email, string phone, string password, bool is_admin)
        {
            var customer = new Customer(full_name,email,phone, BCrypt.Net.BCrypt.HashPassword(password), is_admin);
            await _yotorDbContext.Customers.AddAsync(customer);
            await _yotorDbContext.SaveChangesAsync();
        }
        
    }
}
