using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomZfile.Models
{
    public class User
    {
        public int id { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public DateTime creation_time { get; set; }
    }
}
