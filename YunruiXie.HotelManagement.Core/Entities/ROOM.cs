using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace YunruiXie.HotelManagement.Core.Entities
{
    [Table("ROOMS")]
    public class ROOM
    {
        public int Id { get; set; }
        public int? RTCODE { get; set; }
        public bool? STATUS { get; set; }
        public CUSTOMER? Customer { get; set; }
        public ICollection<SERVICE>? Services { get; set; }
        public ROOMTYPE Roomtype { get; set; }
    }
}
