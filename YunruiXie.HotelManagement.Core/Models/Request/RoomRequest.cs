using System;
using System.Collections.Generic;
using System.Text;
using YunruiXie.HotelManagement.Core.Entities;

namespace YunruiXie.HotelManagement.Core.Models.Request
{
    public class RoomRequest
    {
        public int Id { get; set; }
        public int? RTCODE { get; set; }
    }
}
