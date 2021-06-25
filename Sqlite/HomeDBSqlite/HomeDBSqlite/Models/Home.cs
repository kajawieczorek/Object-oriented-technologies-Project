using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeDBSqlite.Models
{
    public class Home
    {
        [Key]
        public int OID { get; set; }
        [Required]
        public List<Owner> Owners { get; set; }
        [Required]
        public List<Room> Rooms { get; set; }
        [Required]
        public Address Address { get; set; }
        public List<Occupant> Occupants { get; set; }
    }
}
