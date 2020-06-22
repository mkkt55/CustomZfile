using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomZfile.Models
{
    public class DriveAllowUser
    {
        public int drive_id { get; set; }
        public int user_id { get; set; }
        public int allow_read { get; set; }
        public int allow_modify { get; set; }
        public int allow_download { get; set; }
        public int allow_upload { get; set; }
    }
}
