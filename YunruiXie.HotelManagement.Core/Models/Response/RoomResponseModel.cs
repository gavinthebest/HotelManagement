using System;
using System.Collections.Generic;
using System.Text;
using YunruiXie.HotelManagement.Core.Entities;

namespace YunruiXie.HotelManagement.Core.Models.Response
{
    public class RoomResponseModel
    {
        public int Id { get; set; }
        public int? RTCODE { get; set; }
        public bool? STATUS { get; set; }
        public List<ServiceResponseModel>? Services { get; set; }
    }
}
