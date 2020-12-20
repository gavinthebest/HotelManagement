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
    public class RoomtypeRepository : EfRepository<ROOMTYPE>, IRoomtypeRepository
    {
        public RoomtypeRepository(HotelManagementDbContext dbContext) : base(dbContext)
        {
        }
        public async Task<ROOMTYPE> GetRoomtypeById(int? id)
        {
            return await _dbContext.Roomtypes.FirstOrDefaultAsync(rt => rt.Id == id);
        }

    }
}