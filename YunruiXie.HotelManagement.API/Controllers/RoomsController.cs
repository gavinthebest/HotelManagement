using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using YunruiXie.HotelManagement.Core.Models.Request;
using YunruiXie.HotelManagement.Core.ServiceInterfaces;
using System.Linq;

namespace YunruiXie.HotelManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomsController : ControllerBase
    {
        private readonly IRoomService _roomService;
        public RoomsController(IRoomService roomService)
        {
            _roomService = roomService;
        }
        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateRoom(RoomRequest roomCreateRequest)
        {
            var room = await _roomService.CreateRoom(roomCreateRequest);
            return Ok(room);
        }
        [HttpPut]
        [Route("update")]
        public async Task<IActionResult> UpdateRoom(RoomRequest roomUpdateRequest)
        {
            var room = await _roomService.UpdateRoom(roomUpdateRequest);
            return Ok(room);
        }
        [HttpGet]
        [Route("all")] 
        public async Task<IActionResult> ListAllRooms()
        {
            var rooms = await _roomService.ListAllRooms();
            if (!rooms.Any())
            {
                return NotFound("No Rooms Found");
            }
            return Ok(rooms);
        }
        [HttpDelete]
        [Route("delete")]
        public async Task DeleteRoom(int id)
        {
             await _roomService.DeleteRoom(id);
        }
    }
}
