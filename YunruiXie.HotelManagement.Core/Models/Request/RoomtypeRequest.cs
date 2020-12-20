using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace YunruiXie.HotelManagement.Core.Models.Request
{
    public class RoomtypeRequest
    {
        public int Id { get; set; }
        [MaxLength(20)]
        public string? RTDESC { get; set; }
        public decimal? Rent { get; set; }
    }
}
