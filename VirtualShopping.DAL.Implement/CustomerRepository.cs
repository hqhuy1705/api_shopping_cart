using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using VirtualShopping.DAL.Implement.Data;
using VirtualShopping.DAL.Interface;
using VirtualShopping.Domain.Entities;
using VirtualShopping.Domain.Responses.Customer;

namespace VirtualShopping.DAL.Implement
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly DataContext _context;

        public CustomerRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<CustomerViewModel> GetActiveCustomerByPhoneNumberAsync(string PhoneNumber)
        {
            return await _context.Customers.Where(c => c.PhoneNumber == PhoneNumber && c.IsActived == true)
                .Select(c => new CustomerViewModel
                {
                    Avatar = c.Avatar,
                    CustomerId = c.CustomerId,
                    PhoneNumber = c.PhoneNumber,
                    Name = c.Name
                }).FirstOrDefaultAsync();
        }

        public async Task<Customer> GetCustomerByIdAsync(string customerId)
        {
            return await _context.Customers.FindAsync(customerId);
        }

        public async Task<bool> RegisterCustomerAsync(Customer newCustomer)
        {
            await _context.AddAsync<Customer>(newCustomer);
            return (await _context.SaveChangesAsync()) > 0;
        }

        public async Task<bool> UpdateCustomerAsync(Customer customer)
        {
            return (await _context.SaveChangesAsync()) > 0;
        }
    }
}
