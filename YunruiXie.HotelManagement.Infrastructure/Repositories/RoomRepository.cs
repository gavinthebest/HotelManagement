using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using YunruiXie.HotelManagement.Core.Entities;
using YunruiXie.HotelManagement.Core.RepositoryInterfaces;
using YunruiXie.HotelManagement.Infrastructure.Data;

namespace YunruiXie.HotelManagement.Infrastructure.Repositories
{
    public class RoomRepository : EfRepository<ROOM>, IRoomRepository
    {
        public RoomRepository(HotelManagementDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<ROOM> GetRoomById(int? id)
        {
            return await _dbContext.Rooms.FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<ROOM> GetRoomByRTCode(int id)
        {
            return await _dbContext.Rooms.FirstOrDefaultAsync(r => r.RTCODE == id);
        }
    }
}