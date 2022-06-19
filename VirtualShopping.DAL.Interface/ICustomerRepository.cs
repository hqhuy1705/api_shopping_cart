using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualShopping.Domain.Entities;
using VirtualShopping.Domain.Responses.Customer;

namespace VirtualShopping.DAL.Interface
{
    public interface ICustomerRepository
    {
        Task<bool> RegisterCustomerAsync(Customer newCustomer);
        Task<Customer> GetCustomerByIdAsync(string customerId);
        Task<CustomerViewModel> GetActiveCustomerByPhoneNumberAsync(string PhoneNumber);
        Task<bool> UpdateCustomerAsync(Customer customer);
    }
}
