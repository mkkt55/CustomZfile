using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomZfile.Models
{
    public class SystemConfig
	{
        public const string FILEROOT = "fileroot";

        public string username { get; set; }

        public string password { get; set; }

        public List<DriveConfig> driveConfigs { get; set; }
    }

}
