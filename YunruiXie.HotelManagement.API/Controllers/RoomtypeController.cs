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
    public class RoomtypesController : ControllerBase
    {
        private readonly IRoomtypeService _roomtypeService;
        public RoomtypesController(IRoomtypeService roomtypeService)
        {
            _roomtypeService = roomtypeService;
        }
        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateRoomtype(RoomtypeRequest roomtypeCreateRequest)
        {
            var roomtype = await _roomtypeService.CreateRoomtype(roomtypeCreateRequest);
            return Ok(roomtype);
        }
        [HttpPut]
        [Route("update")]
        public async Task<IActionResult> UpdateRoomtype(RoomtypeRequest roomtypeUpdateRequest)
        {
            var roomtype = await _roomtypeService.UpdateRoomtype(roomtypeUpdateRequest);
            return Ok(roomtype);
        }
        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> ListAllRoomtypes()
        {
            var roomtypes = await _roomtypeService.ListAllRoomtypes();
            if (!roomtypes.Any())
            {
                return NotFound("No Roomtypes Found");
            }
            return Ok(roomtypes);
        }
        [HttpDelete]
        [Route("delete")]
        public async Task DeleteRoomtype(int id)
        {
            await _roomtypeService.DeleteRoomtype(id);
        }
    }
}
