using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeDBSqlite.Models
{
    public class Door
    {
        [Key]
        public int OID { get; set; }
        public string Type { get; set; }
    }
}
