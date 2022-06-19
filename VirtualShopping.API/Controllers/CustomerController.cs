using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using VirtualShopping.BLL.Interface;
using VirtualShopping.Domain.Entities;
using VirtualShopping.Domain.Requests.Customer;
using VirtualShopping.Domain.Utilities;

namespace VirtualShopping.Controllers
{
    public class CustomerController : BaseController
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        /// <summary>
        /// Reigster a new customer
        /// </summary>
        /// <param name="request"></param>
        /// <returns>An object of new customer</returns>
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromForm] CustomerRegisterReqModel request)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _customerService.RegisterCustomerAsync(request));
            }
            return BadRequest();
        }

        /// <summary>
        /// Update customer information
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Response of update customer information process</returns>
        [HttpPut]
        public async Task<IActionResult> Update([FromForm] CustomerUpdateReqModel request)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _customerService.UpdateCustomerAsync(request));
            }
            return BadRequest();
        }

        /// <summary>
        /// Customer login by Phone Number
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Response for deleted customer process</returns>
        [HttpPost("login")]
        public async Task<IActionResult> Login(CustomerLoginReqModel request)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _customerService.LoginAsync(request));
            }
            return BadRequest();
        }

        /// <summary>
        /// Delete a exists customer
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns>Response for deleted customer process</returns>
        [HttpDelete("delete")]
        public async Task<IActionResult> Deleted(string customerId)
        {
            return Ok(await _customerService.DeletedCustomerAsync(customerId));
        }
    }
}
