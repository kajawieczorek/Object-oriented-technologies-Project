using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace HomeDBSqlite.Models
{
    public class Address
    {
        [Key]
        public int OID { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        [Required]
        public string Street { get; set; }
        [Required]
        public string BuildingNumber { get; set; }
        public string ApartmentNumber { get; set; }
        public string ZIPCode { get; set; }
    }
}
