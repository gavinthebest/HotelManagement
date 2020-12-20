using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using YunruiXie.HotelManagement.Core.Models.Request;
using YunruiXie.HotelManagement.Core.Models.Response;

namespace YunruiXie.HotelManagement.Core.ServiceInterfaces
{
    public interface IRoomService
    {
        Task<IEnumerable<RoomResponseModel>> ListAllRooms();
        Task<RoomResponseModel> CreateRoom(RoomRequest roomCreateRequest);
        Task<RoomResponseModel> UpdateRoom(RoomRequest roomUpdateRequest);
        Task DeleteRoom(int roomId);
    }
}
