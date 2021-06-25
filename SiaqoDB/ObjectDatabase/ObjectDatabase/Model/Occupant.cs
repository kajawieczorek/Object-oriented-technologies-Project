using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectDatabase.Model
{
    public class Occupant
    {
        public int OID { get; set; }
        public Person PersonOccupant { get; set; }
    }
}
