using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace YunruiXie.HotelManagement.Core.Entities
{
    [Table("SERVICES")]
    public class SERVICE
    {
        public int Id { get; set; }
        public int? ROOMNO { get; set; }
        [MaxLength(50)]
        public string? SDESC { get; set; }
        public decimal? AMOUNT { get; set; }
        public DateTime? ServiceDate { get; set; }
        public ROOM? Room { get; set; }
    }
}
