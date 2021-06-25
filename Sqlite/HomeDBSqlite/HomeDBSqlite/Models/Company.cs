using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeDBSqlite.Models
{
    public class Company
    {
        [Key]
        public int OID { get; set; }
        [Required]
        public string Name { get; set; }
        public Address Headquaters { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
    }
}
