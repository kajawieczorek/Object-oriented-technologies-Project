using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectDatabase.Model
{
    public class Room
    {
        public int OID { get; set; }
        public string Name { get; set; }
        public double Area { get; set; }
        public List<Door> Doors { get; set; }
        public List<Window> Windows { get; set; }
        public List<Furniture> Furniture { get; set; }
    }
}
