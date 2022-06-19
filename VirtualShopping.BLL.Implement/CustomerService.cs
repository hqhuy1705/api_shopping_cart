using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;
using VirtualShopping.BLL.Interface;
using VirtualShopping.DAL.Interface;
using VirtualShopping.Domain.Entities;
using VirtualShopping.Domain.Requests.Customer;
using VirtualShopping.Domain.Responses.Customer;
using VirtualShopping.Domain.Utilities;

namespace VirtualShopping.BLL.Implement
{
    public class CustomerService : ICustomerService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _config;

        public CustomerService(IUnitOfWork unitOfWork,
                                IConfiguration config)
        {
            _unitOfWork = unitOfWork;
            _config = config;
        }

        public async Task<CustomerDeletedResModel> DeletedCustomerAsync(string customerId)
        {
            Customer existCustomer = await _unitOfWork.CustomerRepository.GetCustomerByIdAsync(customerId);
            if (existCustomer != null)
            {
                existCustomer.IsActived = false;
                var result = await _unitOfWork.CustomerRepository.UpdateCustomerAsync(existCustomer);
                if (result)
                {
                    return new CustomerDeletedResModel
                    {
                        CustomerId = existCustomer.CustomerId
                    };
                }

                return new CustomerDeletedResModel
                {
                    ErrorMessage = "Deleted customer process is not sucessfully"
                };
            }

            return new CustomerDeletedResModel
            {
                ErrorMessage = "Account not found"
            };
        }

        public async Task<CustomerRegisterResModel> RegisterCustomerAsync(CustomerRegisterReqModel request)
        {
            var duplicateCustomer = await _unitOfWork.CustomerRepository.GetActiveCustomerByPhoneNumberAsync(request.PhoneNumber);

            if (duplicateCustomer != null)
            {
                return new CustomerRegisterResModel
                {
                    ErrorMessage = "Please using another Phone Number"
                };
            }

            var customer = new Customer()
            {
                Avatar = !String.IsNullOrEmpty(request.Avatar?.FileName) ? await Helper.ImgConvertor(request.Avatar) : "",
                CustomerId = Helper.IdGenerator(),
                Name = request.Name,
                PhoneNumber = request.PhoneNumber,
                IsActived = true
            };

            Customer existCustomer = await _unitOfWork.CustomerRepository.GetCustomerByIdAsync(customer.CustomerId);

            while (existCustomer != null)
            {
                customer.CustomerId = Helper.IdGenerator();
                existCustomer = await _unitOfWork.CustomerRepository.GetCustomerByIdAsync(customer.CustomerId);
            };

            try
            {
                var result = await _unitOfWork.CustomerRepository.RegisterCustomerAsync(customer);
                if (result)
                {
                    return new CustomerRegisterResModel
                    {
                        CustomerId = customer.CustomerId
                    };
                }

                return new CustomerRegisterResModel();
            }
            catch (Exception ex)
            {
                return new CustomerRegisterResModel
                {
                    ErrorMessage = "Unknown error, please contact administrator"
                };
            }
        }

        public async Task<CustomerUpdateResModel> UpdateCustomerAsync(CustomerUpdateReqModel request)
        {
            Customer existCustomer = await _unitOfWork.CustomerRepository.GetCustomerByIdAsync(request.CustomerId);
            var duplicatePhoneNumberCustomer = await _unitOfWork.CustomerRepository.GetActiveCustomerByPhoneNumberAsync(request.PhoneNumber);

            if (duplicatePhoneNumberCustomer != null && existCustomer.CustomerId != duplicatePhoneNumberCustomer.CustomerId)
            {
                return new CustomerUpdateResModel
                {
                    ErrorMessage = ErrorConstants.PhoneNumberIsUsed
                };
            }

            if (existCustomer != null)
            {
                existCustomer.Name = request.Name;
                existCustomer.PhoneNumber = request.PhoneNumber;

                if (!String.IsNullOrEmpty(request.Avatar?.FileName))
                {
                    existCustomer.Avatar = await Helper.ImgConvertor(request.Avatar);
                }

                try
                {
                    var result = await _unitOfWork.CustomerRepository.UpdateCustomerAsync(existCustomer);
                    if (result)
                    {
                        return new CustomerUpdateResModel
                        {
                            CustomerId = existCustomer.CustomerId
                        };
                    }
                }
                catch (Exception ex)
                {
                    return new CustomerUpdateResModel
                    {
                        ErrorMessage = "Unknown error, please contact administrator"
                    };
                }
            }

            return new CustomerUpdateResModel
            {
                ErrorMessage = "Your update information process is not successful, please try again later"
            };
        }

        public async Task<CustomerViewModel> LoginAsync(CustomerLoginReqModel request)
        {
            var existsCustomer = await _unitOfWork.CustomerRepository.GetActiveCustomerByPhoneNumberAsync(request.PhoneNumber);
            if (existsCustomer != null)
            {
                return new CustomerViewModel
                {
                    CustomerId = existsCustomer.CustomerId,
                    Avatar = existsCustomer.Avatar,
                    Name = existsCustomer.Name,
                    PhoneNumber = existsCustomer.PhoneNumber
                };
            }

            return new CustomerViewModel();
        }
    }
}
