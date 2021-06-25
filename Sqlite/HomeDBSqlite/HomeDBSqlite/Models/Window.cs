using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeDBSqlite.Models
{
    public class Window
    {
        [Key]
        public int OID { get; set; }
        [Required]
        public double Area { get; set; }
        [Required]
        public string Side { get; set; }
    }
}
