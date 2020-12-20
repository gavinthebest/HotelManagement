using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using YunruiXie.HotelManagement.Core.Models.Request;
using YunruiXie.HotelManagement.Core.Models.Response;

namespace YunruiXie.HotelManagement.Core.ServiceInterfaces
{
    public interface ICustomerService
    {
        Task<IEnumerable<CustomerResponseModel>> ListAllCustomers();
        Task<CustomerResponseModel> CreateCustomer(CustomerRequest roomCreateRequest);
        Task<CustomerResponseModel> UpdateCustomer(CustomerRequest roomUpdateRequest);
        Task DeleteCustomer(int roomId);
        Task<CustomerResponseModel> GetCustomerDetails(int id);
    }
}
