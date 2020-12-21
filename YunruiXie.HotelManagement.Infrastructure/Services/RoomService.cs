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
    public class RoomService : IRoomService
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IRoomtypeRepository _roomtypeRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IServiceRepository _serviceRepository;

        public RoomService(IRoomRepository roomRepository, IRoomtypeRepository roomtypeRepository,
            ICustomerRepository customerRepository, IServiceRepository serviceRepository)
        {
            _roomRepository = roomRepository;
            _roomtypeRepository = roomtypeRepository;
            _customerRepository = customerRepository;
            _serviceRepository = serviceRepository;
        }
        public async Task<RoomResponseModel> CreateRoom(RoomRequest roomCreateRequest)
        {
            //If there is already a room exists with same Id, throw exception
            var dbRoom = await _roomRepository.GetRoomById(roomCreateRequest.Id);
            if (dbRoom != null && dbRoom.Id == roomCreateRequest.Id)
                throw new Exception("Room Already Exists");
            //If roomtype code not exists, throw exception
            var dbRoomtype = await _roomtypeRepository.GetRoomtypeById(roomCreateRequest.RTCODE);
            if (dbRoomtype == null)
                throw new Exception("Roomtype Not Exists");

            //When creating a room, the default value of booking status should be false
            var room = new ROOM
            {
                Id = roomCreateRequest.Id,
                RTCODE = roomCreateRequest.RTCODE,
                STATUS = false,
            };
            var createRoom = await _roomRepository.AddAsync(room);
            var services = await _serviceRepository.GetServicesByRoom(room.Id);
            var response = new RoomResponseModel
            {
                Id = createRoom.Id,
                RTCODE = createRoom.RTCODE,
                STATUS = createRoom.STATUS,
                Services = (List<ServiceResponseModel>)services
            };
            return response;
        }

        public async Task DeleteRoom(int roomId)
        {
            //If room not exists, throw exception
            var room = await _roomRepository.GetRoomById(roomId);
            if (room == null) throw new Exception("Room " + roomId + " Not Exists");

            //If room is booked, throw exception
            if (room.STATUS == true) throw new Exception("Customer Booked This Room, Cannot Delete Now");

            //Then We Can Delete This Room
            await _roomRepository.DeleteAsync(room);
        }

        public async Task<IEnumerable<RoomResponseModel>> ListAllRooms()
        {
            var allRooms = await _roomRepository.ListAllAsync();
            var responses = new List<RoomResponseModel>();
            foreach(var room in allRooms)
            {
                var services = await _serviceRepository.GetServicesByRoom(room.Id);
                var response = new RoomResponseModel
                {
                    Id = room.Id,
                    RTCODE = room.RTCODE,
                    STATUS = room.STATUS,
                    Services = (List<ServiceResponseModel>)services
                };
                responses.Add(response);
            }
            return responses;
        }
        public async Task<RoomResponseModel> GetRoomDetails(int id)
        {
            var room = await _roomRepository.GetRoomById(id);
            if (room == null) throw new Exception("Room " + id + " Not Exists");

            var services = await _serviceRepository.GetServicesByRoom(room.Id);
            var response = new RoomResponseModel
            {
                Id = room.Id,
                RTCODE = room.RTCODE,
                STATUS = room.STATUS,
                Services = (List<ServiceResponseModel>)services
            };
            return response;
        }
        public async Task<RoomResponseModel> UpdateRoom(RoomRequest roomUpdateRequest)
        {
            //If room not exists, throw exception
            var dbRoom = await _roomRepository.GetRoomById(roomUpdateRequest.Id);
            if (dbRoom != null && dbRoom.Id == roomUpdateRequest.Id)
                throw new Exception("Room Not Exists");
            //If roomtype code not exists, throw exception
            var dbRoomtype = await _roomtypeRepository.GetRoomtypeById(roomUpdateRequest.RTCODE);
            if (dbRoomtype == null)
                throw new Exception("Roomtype Not Exists");

            dbRoom.RTCODE = roomUpdateRequest.RTCODE;
            var updatedRoom = await _roomRepository.UpdateAsync(dbRoom);

            var services = await _serviceRepository.GetServicesByRoom(dbRoom.Id);
            var response = new RoomResponseModel
            {
                Id = updatedRoom.Id,
                RTCODE = updatedRoom.RTCODE,
                STATUS = updatedRoom.STATUS,
                Services = (List<ServiceResponseModel>)services
            };
            return response;
        }

    }
}
