using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YunruiXie.HotelManagement.Core.Models.Request;
using YunruiXie.HotelManagement.Core.ServiceInterfaces;

namespace YunruiXie.HotelManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;
        }
        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateCustomer(CustomerRequest customerCreateRequest)
        {
            var customer = await _customerService.CreateCustomer(customerCreateRequest);
            return Ok(customer);
        }
        [HttpPut]
        [Route("update/{id:int}")]
        public async Task<IActionResult> UpdateCustomer(int id, CustomerRequest customerUpdateRequest)
        {
            var customer = await _customerService.UpdateCustomer(customerUpdateRequest);
            return Ok(customer);
        }
        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> ListAllCustomers()
        {
            var customers = await _customerService.ListAllCustomers();
            if (!customers.Any())
            {
                return NotFound("No Customers Found");
            }
            return Ok(customers);
        }
        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetCustomerDetails(int id)
        {
            var customer = await _customerService.GetCustomerDetails(id);
            if (customer == null)
            {
                return NotFound("No Customer Found");
            }
            return Ok(customer);
        }
        [HttpDelete]
        [Route("delete/{id:int}")]
        public async Task DeleteCustomer(int id)
        {
            await _customerService.DeleteCustomer(id);
        }
    }
}
