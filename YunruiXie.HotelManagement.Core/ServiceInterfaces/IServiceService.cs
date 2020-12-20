using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using YunruiXie.HotelManagement.Core.Models.Request;
using YunruiXie.HotelManagement.Core.Models.Response;

namespace YunruiXie.HotelManagement.Core.ServiceInterfaces
{
    public interface IServiceService
    {
        Task<IEnumerable<ServiceResponseModel>> ListAllServices();
        Task<ServiceResponseModel> CreateService(ServiceRequest serviceCreateRequest);
        Task<ServiceResponseModel> UpdateService(ServiceRequest serviceUpdateRequest);
        Task DeleteService(int serviceId);
        Task<ServiceResponseModel> GetServiceDetails(int id);
    }
}
