using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace YunruiXie.HotelManagement.Core.Models.Request
{
    public class CustomerRequest
    {
        public int Id { get; set; }
        public int? ROOMNO { get; set; }
        [MaxLength(20)]
        public string? CNAME { get; set; }
        [MaxLength(100)]
        public string? ADDRESS { get; set; }
        [MaxLength(20)]
        public string? PHONE { get; set; }
        [MaxLength(40)]
        [EmailAddress]
        public string? EMAIL { get; set; }
        public DateTime? CHECKIN { get; set; }
        public int? TotalPERSONS { get; set; }
        public int? BookingDays { get; set; }
        public decimal? ADVANCE { get; set; }
    }
}
