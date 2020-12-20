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
    public class CustomerRepository : EfRepository<CUSTOMER>, ICustomerRepository
    {
        public CustomerRepository(HotelManagementDbContext dbContext) : base(dbContext)
        {
        }
        public async Task<CUSTOMER> GetCustomerById(int customerId)
        {
            return await _dbContext.Customers.FirstOrDefaultAsync(c => c.Id == customerId);
        }
        public async Task<CUSTOMER> GetCustomerByEmail(string customerEmail)
        {
            return await _dbContext.Customers.FirstOrDefaultAsync(c => c.EMAIL == customerEmail);
        }
    }
}
