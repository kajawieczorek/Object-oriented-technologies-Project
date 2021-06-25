using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectDatabase.Model
{
    public class Owner
    {
        public int OID { get; set; }
        public Person PersonOwner { get; set; }
        public Company CompanyOwner { get; set; }
        public int Share { get; set; }
    }
}
