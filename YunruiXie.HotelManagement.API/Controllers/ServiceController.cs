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
    public class ServicesController : ControllerBase
    {
        private readonly IServiceService _serviceService;
        public ServicesController(IServiceService serviceService)
        {
            _serviceService = serviceService;
        }
        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateService(ServiceRequest serviceCreateRequest)
        {
            var service = await _serviceService.CreateService(serviceCreateRequest);
            return Ok(service);
        }
        [HttpPut]
        [Route("update")]
        public async Task<IActionResult> UpdateService(ServiceRequest serviceUpdateRequest)
        {
            var service = await _serviceService.UpdateService(serviceUpdateRequest);
            return Ok(service);
        }
        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> ListAllServices()
        {
            var services = await _serviceService.ListAllServices();
            if (!services.Any())
            {
                return NotFound("No Services Found");
            }
            return Ok(services);
        }
        [HttpDelete]
        [Route("delete")]
        public async Task DeleteService(int id)
        {
            await _serviceService.DeleteService(id);
        }
    }
}
