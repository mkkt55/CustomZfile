using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomZfile.Models
{
    public class Drive
    {
        public int id { get; set; }
        public string name { get; set; }
        public string net_address { get; set; }
        public DateTime creation_time { get; set; }
        public int creator_id { get; set; }
        public string password { get; set; }
    }
}
