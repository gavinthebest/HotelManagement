using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YunruiXie.HotelManagement.Core.Entities;
using YunruiXie.HotelManagement.Core.Models.Response;
using YunruiXie.HotelManagement.Core.RepositoryInterfaces;
using YunruiXie.HotelManagement.Infrastructure.Data;

namespace YunruiXie.HotelManagement.Infrastructure.Repositories
{
    public class ServiceRepository : EfRepository<SERVICE>, IServiceRepository
    {
        public ServiceRepository(HotelManagementDbContext dbContext) : base(dbContext)
        {
        }
        public async Task<SERVICE> GetServiceById(int id)
        {
            return await _dbContext.Services.FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<IEnumerable<ServiceResponseModel>> GetServicesByRoom(int? roomNo)
        {
            return await _dbContext.Services.Where(m => m.ROOMNO == roomNo)
                .Select(r => new ServiceResponseModel
                {
                    Id = r.Id,
                    ROOMNO = r.ROOMNO,
                    SDESC = r.SDESC,
                    AMOUNT = r.AMOUNT,
                    ServiceDate = r.ServiceDate
                }).ToListAsync();
        }
    }
}