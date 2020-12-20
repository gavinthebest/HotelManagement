using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using YunruiXie.HotelManagement.Core.Entities;

namespace YunruiXie.HotelManagement.Core.RepositoryInterfaces
{
    public interface IRoomtypeRepository : IAsyncRepository<ROOMTYPE>
    {
        Task<ROOMTYPE> GetRoomtypeById(int id);
    }
}
