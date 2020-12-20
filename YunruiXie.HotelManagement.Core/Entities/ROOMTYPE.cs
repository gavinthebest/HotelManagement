using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace YunruiXie.HotelManagement.Core.Entities
{
    [Table("ROOMTYPES")]
    public class ROOMTYPE
    {
        public int Id { get; set; }
        [MaxLength(20)]
        public string? RTDESC { get; set; }
        public decimal? Rent { get; set; }
        public ICollection<ROOM> Rooms { get; set; }
    }
}
