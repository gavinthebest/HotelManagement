using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using YunruiXie.HotelManagement.Core.Models.Request;
using YunruiXie.HotelManagement.Core.Models.Response;

namespace YunruiXie.HotelManagement.Core.ServiceInterfaces
{
    public interface IRoomtypeService
    {
        Task<IEnumerable<RoomtypeResponseModel>> ListAllRoomtypes();
        Task<RoomtypeResponseModel> CreateRoomtype(RoomtypeRequest roomtypeCreateRequest);
        Task<RoomtypeResponseModel> UpdateRoomtype(RoomtypeRequest roomtypeUpdateRequest);
        Task DeleteRoomtype(int roomtypeId);
    }
}
