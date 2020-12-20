using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using YunruiXie.HotelManagement.Core.Entities;

namespace YunruiXie.HotelManagement.Core.RepositoryInterfaces
{
    public interface ICustomerRepository : IAsyncRepository<CUSTOMER>
    {
        Task<CUSTOMER> GetCustomerById(int customerId);
        Task<CUSTOMER> GetCustomerByEmail(string customerEmail);

    }
}
