using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualShopping.Domain.Entities;
using VirtualShopping.Domain.Requests.Customer;
using VirtualShopping.Domain.Responses.Customer;

namespace VirtualShopping.BLL.Interface
{
    public interface ICustomerService
    {
        Task<CustomerRegisterResModel> RegisterCustomerAsync(CustomerRegisterReqModel request);
        Task<CustomerUpdateResModel> UpdateCustomerAsync(CustomerUpdateReqModel request);
        Task<CustomerDeletedResModel> DeletedCustomerAsync(string customerId);
        Task<CustomerViewModel> LoginAsync(CustomerLoginReqModel request);
    }
}
