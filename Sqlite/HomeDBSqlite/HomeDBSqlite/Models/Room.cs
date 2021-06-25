using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeDBSqlite.Models
{
    public class Room
    {
        [Key]
        public int OID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public double Area { get; set; }
        public List<Door> Doors { get; set; }
        public List<Window> Windows { get; set; }
        public List<Furniture> Furniture { get; set; }
    }
}
