using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectDatabase.Model
{
    public class Home
    {
        public int OID { get; set; }
        public List<Owner> Owners { get; set; }
        public List<Room> Rooms { get; set; }
        public Address Address { get; set; }
        public List<Occupant> Occupants { get; set; }
    }
}
