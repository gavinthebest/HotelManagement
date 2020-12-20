using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using YunruiXie.HotelManagement.Core.Entities;
using YunruiXie.HotelManagement.Core.Models.Response;

namespace YunruiXie.HotelManagement.Core.RepositoryInterfaces
{
    public interface IServiceRepository : IAsyncRepository<SERVICE>
    {
        Task<SERVICE> GetServiceById(int id);
        Task<IEnumerable<ServiceResponseModel>> GetServicesByRoom(int? roomNo);
    }
}
