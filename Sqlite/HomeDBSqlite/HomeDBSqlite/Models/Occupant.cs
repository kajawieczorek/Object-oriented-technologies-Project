using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeDBSqlite.Models
{
    public class Occupant
    {
        [Key]
        public int OID { get; set; }
        public Person PersonOccupant { get; set; }
    }
}
