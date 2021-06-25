using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeDBSqlite.Models
{
    public class Furniture
    {
        [Key]
        public int OID { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
