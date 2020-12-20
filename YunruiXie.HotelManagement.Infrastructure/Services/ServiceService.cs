using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using YunruiXie.HotelManagement.Core.Entities;
using YunruiXie.HotelManagement.Core.Models.Request;
using YunruiXie.HotelManagement.Core.Models.Response;
using YunruiXie.HotelManagement.Core.RepositoryInterfaces;
using YunruiXie.HotelManagement.Core.ServiceInterfaces;
using YunruiXie.HotelManagement.Infrastructure.Repositories;

namespace YunruiXie.HotelManagement.Infrastructure.Services
{
    public class ServiceService : IServiceService
    {
        private readonly IServiceRepository _serviceRepository;
        private readonly IRoomRepository _roomRepository;

        public ServiceService(IRoomRepository roomRepository, IServiceRepository serviceRepository)
        {
            _roomRepository = roomRepository;
            _serviceRepository = serviceRepository;
        }
        public async Task<ServiceResponseModel> CreateService(ServiceRequest serviceCreateRequest)
        {
            //If there is already a service exists with same Id, throw exception
            var dbService = await _serviceRepository.GetServiceById(serviceCreateRequest.Id);
            if (dbService != null && dbService.Id == serviceCreateRequest.Id)
                throw new Exception("Service Already Exists");

            //If room number of this service cannot be found, throw exception
            if (serviceCreateRequest.ROOMNO != null)
            {
                var dbRoom = await _roomRepository.GetRoomById(serviceCreateRequest.ROOMNO);
                if (dbRoom == null) throw new Exception("The Room You Ask To Have Service Does Not Exist");
            }
            var service = new SERVICE
            {
                Id = serviceCreateRequest.Id,
                ROOMNO = serviceCreateRequest.ROOMNO,
                SDESC = serviceCreateRequest.SDESC,
                AMOUNT = serviceCreateRequest.AMOUNT,
                ServiceDate = serviceCreateRequest.ServiceDate
            };
            var createService = await _serviceRepository.AddAsync(service);
            var response = new ServiceResponseModel
            {
                Id = createService.Id,
                ROOMNO = createService.ROOMNO,
                SDESC = createService.SDESC,
                AMOUNT = createService.AMOUNT,
                ServiceDate = createService.ServiceDate
            };
            return response;
        }

        public async Task DeleteService(int serviceId)
        {
            //If service not exists, throw exception
            var service = await _serviceRepository.GetServiceById(serviceId);
            if (service == null) throw new Exception("Service " + serviceId + " Not Exists");

            await _serviceRepository.DeleteAsync(service);
        }

        public async Task<IEnumerable<ServiceResponseModel>> ListAllServices()
        {
            var allService = await _serviceRepository.ListAllAsync();
            var responses = new List<ServiceResponseModel>();
            foreach (var service in allService)
            {
                var response = new ServiceResponseModel
                {
                    Id = service.Id,
                    ROOMNO = service.ROOMNO,
                    SDESC = service.SDESC,
                    AMOUNT = service.AMOUNT,
                    ServiceDate = service.ServiceDate
                };
                responses.Add(response);
            }
            return responses;
        }

        public async Task<ServiceResponseModel> UpdateService(ServiceRequest serviceUpdateRequest)
        {            
            //If there there is not a service exists with this Id, throw exception
            var dbService = await _serviceRepository.GetServiceById(serviceUpdateRequest.Id);
            if (dbService == null) throw new Exception("Service " + serviceUpdateRequest.Id + " Not Exists");

            //If room number of this service cannot be found, throw exception
            if (serviceUpdateRequest.ROOMNO != null)
            {
                var dbRoom = await _roomRepository.GetRoomById(serviceUpdateRequest.ROOMNO);
                if (dbRoom == null) throw new Exception("The Room You Ask To Have Service Does Not Exist");
            }

            var service = new SERVICE
            {
                Id = serviceUpdateRequest.Id,
                ROOMNO = serviceUpdateRequest.ROOMNO,
                SDESC = serviceUpdateRequest.SDESC,
                AMOUNT = serviceUpdateRequest.AMOUNT,
                ServiceDate = serviceUpdateRequest.ServiceDate
            };
            var updatedService = await _serviceRepository.UpdateAsync(service);
            var response = new ServiceResponseModel
            {
                Id = updatedService.Id,
                ROOMNO = updatedService.ROOMNO,
                SDESC = updatedService.SDESC,
                AMOUNT = updatedService.AMOUNT,
                ServiceDate = updatedService.ServiceDate
            };
            return response;
        }
    }
}
