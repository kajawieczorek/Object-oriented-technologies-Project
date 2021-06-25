using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeDBSqlite.Models
{
    public class Owner
    {
        [Key]
        public int OID { get; set; }
        public Person PersonOwner { get; set; }
        public Company CompanyOwner { get; set; }
        [Required]
        public int Share { get; set; }
    }
}
