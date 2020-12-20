using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using YunruiXie.HotelManagement.Core.Entities;
using YunruiXie.HotelManagement.Core.Models.Request;
using YunruiXie.HotelManagement.Core.Models.Response;
using YunruiXie.HotelManagement.Core.RepositoryInterfaces;
using YunruiXie.HotelManagement.Core.ServiceInterfaces;
using YunruiXie.HotelManagement.Infrastructure.Repositories;

namespace YunruiXie.HotelManagement.Infrastructure.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IRoomRepository _roomRepository;

        public CustomerService(IRoomRepository roomRepository, ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
            _roomRepository = roomRepository;
        }
        public async Task<CustomerResponseModel> CreateCustomer(CustomerRequest customerCreateRequest)
        {
            //If there is already a Customer exists with same Id, throw exception
            var dbCustomer = await _customerRepository.GetCustomerById(customerCreateRequest.Id);
            if (dbCustomer != null && dbCustomer.Id == customerCreateRequest.Id)
                throw new Exception("Customer Already Exists");

            //If there is already a Customer exists with same Email, throw exception
            dbCustomer = await _customerRepository.GetCustomerByEmail(customerCreateRequest.EMAIL);
            if (dbCustomer != null && dbCustomer.EMAIL == customerCreateRequest.EMAIL)
                throw new Exception("Email Already Exists");
            
            var dbRoom = await _roomRepository.GetRoomById(customerCreateRequest.ROOMNO);
            //If the room does not exists, throw exception
            if (dbRoom == null) throw new Exception("Room You Want To Book Does Not Exist");
            //If there is already a room booked, throw exception
            if (dbRoom != null && dbRoom.Id == customerCreateRequest.ROOMNO && dbRoom.STATUS == true)
                throw new Exception("Room Already Booked, Please Book Another Room");

            //Set the room status to booked and save it to repository
            dbRoom.STATUS = true;
            await _roomRepository.UpdateAsync(dbRoom);

            // Then create the new customer
            var customer = new CUSTOMER
            {
                Id = customerCreateRequest.Id,
                ROOMNO = customerCreateRequest.ROOMNO,
                CNAME = customerCreateRequest.CNAME,
                ADDRESS = customerCreateRequest.ADDRESS,
                PHONE = customerCreateRequest.PHONE,
                EMAIL = customerCreateRequest.EMAIL,
                CHECKIN = customerCreateRequest.CHECKIN,
                TotalPERSONS = customerCreateRequest.TotalPERSONS,
                BookingDays = customerCreateRequest.BookingDays,
                ADVANCE = customerCreateRequest.ADVANCE
            };
            var createCustomer = await _customerRepository.AddAsync(customer);
            
            // Create response model to return
            var response = new CustomerResponseModel
            {
                Id = createCustomer.Id,
                ROOMNO = createCustomer.ROOMNO,
                CNAME = createCustomer.CNAME,
                ADDRESS = createCustomer.ADDRESS,
                PHONE = createCustomer.PHONE,
                EMAIL = createCustomer.EMAIL,
                CHECKIN = createCustomer.CHECKIN,
                TotalPERSONS = createCustomer.TotalPERSONS,
                BookingDays = createCustomer.BookingDays,
                ADVANCE = createCustomer.ADVANCE,
                Room = new RoomResponseModel
                {
                    Id = dbRoom.Id,
                    RTCODE = dbRoom.RTCODE,
                    STATUS = dbRoom.STATUS,
                }
            };
            return response;
        }

        public async Task DeleteCustomer(int customerId)
        {
            //If customers not exists, throw exception
            var customer = await _customerRepository.GetCustomerById(customerId);
            if (customer == null) throw new Exception("Customer " + customerId + " Not Exists");
            
            //Update status for room to check out
            var preRoom = await _roomRepository.GetRoomById(customer.ROOMNO);
            preRoom.STATUS = false;
            await _roomRepository.UpdateAsync(preRoom);

            //Then delete this customer
            await _customerRepository.DeleteAsync(customer);
        }

        public async Task<CustomerResponseModel> GetCustomerDetails(int id)
        {
            var customer = await _customerRepository.GetCustomerById(id);
            if (customer == null) throw new Exception("Customer " + id + " Not Exists");

            var room = await _roomRepository.GetRoomById(customer.ROOMNO);
            var response = new CustomerResponseModel
            {
                Id = customer.Id,
                ROOMNO = customer.ROOMNO,
                CNAME = customer.CNAME,
                ADDRESS = customer.ADDRESS,
                PHONE = customer.PHONE,
                EMAIL = customer.EMAIL,
                CHECKIN = customer.CHECKIN,
                TotalPERSONS = customer.TotalPERSONS,
                BookingDays = customer.BookingDays,
                ADVANCE = customer.ADVANCE,
                Room = new RoomResponseModel
                {
                    Id = room.Id,
                    RTCODE = room.RTCODE,
                    STATUS = room.STATUS,
                }
            };
            return response;
        }

        public async Task<IEnumerable<CustomerResponseModel>> ListAllCustomers()
        {
            var allCustomers = await _customerRepository.ListAllAsync();
            var responses = new List<CustomerResponseModel>();

            // Add customers one by one to response model
            foreach (var customer in allCustomers)
            {
                var room = await _roomRepository.GetRoomById(customer.ROOMNO);
                var response = new CustomerResponseModel
                {
                    Id = customer.Id,
                    ROOMNO = customer.ROOMNO,
                    CNAME = customer.CNAME,
                    ADDRESS = customer.ADDRESS,
                    PHONE = customer.PHONE,
                    EMAIL = customer.EMAIL,
                    CHECKIN = customer.CHECKIN,
                    TotalPERSONS = customer.TotalPERSONS,
                    BookingDays = customer.BookingDays,
                    ADVANCE = customer.ADVANCE,
                    Room = new RoomResponseModel
                    {
                        Id = room.Id,
                        RTCODE = room.RTCODE,
                        STATUS = room.STATUS,
                    }
                };
                responses.Add(response);
            }
            return responses;
        }

        public async Task<CustomerResponseModel> UpdateCustomer(CustomerRequest customerUpdateRequest)
        {
            // We update customer with the same Id, otherwise get rejected
            //If customers not exists, throw exception
            var customer = await _customerRepository.GetCustomerById(customerUpdateRequest.Id);
            if (customer == null) throw new Exception("Customer " + customerUpdateRequest.Id + " Not Exists");

            //If there is already a Customer exists with same Email, throw exception
            var emailCustomer = await _customerRepository.GetCustomerByEmail(customerUpdateRequest.EMAIL);
            if (emailCustomer != null && emailCustomer.Id != customerUpdateRequest.Id)
                throw new Exception("Email Already Exists");

            //When updating room selection, make sure this room can be booked
            var dbRoom = await _roomRepository.GetRoomById(customerUpdateRequest.ROOMNO);
            if (customerUpdateRequest.ROOMNO != null && customer.ROOMNO != customerUpdateRequest.ROOMNO)
            {
                //If the room does not exists, throw exception
                if (dbRoom == null) throw new Exception("Room You Want To Book Does Not Exist");
                //If there is already a room booked, throw exception
                if (dbRoom != null && dbRoom.Id == customerUpdateRequest.ROOMNO && dbRoom.STATUS == true)
                    throw new Exception("Room Already Booked, Please Book Another Room");

                //Set the previous room empty and new room status to booked and save them to repository
                var preRoom = await _roomRepository.GetRoomById(customer.ROOMNO);
                preRoom.STATUS = false;
                await _roomRepository.UpdateAsync(preRoom);
                dbRoom.STATUS = true;
                await _roomRepository.UpdateAsync(dbRoom);
            }

            // Then create the new customer
            var newCustomer = new CUSTOMER
            {
                Id = customerUpdateRequest.Id,
                ROOMNO = customerUpdateRequest.ROOMNO,
                CNAME = customerUpdateRequest.CNAME,
                ADDRESS = customerUpdateRequest.ADDRESS,
                PHONE = customerUpdateRequest.PHONE,
                EMAIL = customerUpdateRequest.EMAIL,
                CHECKIN = customerUpdateRequest.CHECKIN,
                TotalPERSONS = customerUpdateRequest.TotalPERSONS,
                BookingDays = customerUpdateRequest.BookingDays,
                ADVANCE = customerUpdateRequest.ADVANCE
            };
            var updatedCustomer = await _customerRepository.UpdateAsync(newCustomer);

            // Create response model to return
            var response = new CustomerResponseModel
            {
                Id = updatedCustomer.Id,
                ROOMNO = updatedCustomer.ROOMNO,
                CNAME = updatedCustomer.CNAME,
                ADDRESS = updatedCustomer.ADDRESS,
                PHONE = updatedCustomer.PHONE,
                EMAIL = updatedCustomer.EMAIL,
                CHECKIN = updatedCustomer.CHECKIN,
                TotalPERSONS = updatedCustomer.TotalPERSONS,
                BookingDays = updatedCustomer.BookingDays,
                ADVANCE = updatedCustomer.ADVANCE,
                Room = new RoomResponseModel
                {
                    Id = dbRoom.Id,
                    RTCODE = dbRoom.RTCODE,
                    STATUS = dbRoom.STATUS,
                }
            };
            return response;
        }
    }
}
