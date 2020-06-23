using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomZfile.Models
{
	public class DriveConfig
	{
		public int? id { get; set; }
		public string name { get; set; }
		public string creationTime { get; set; }

		public DriveConfig(int id, string name, DateTime creationTime)
		{
			this.id = id;
			this.name = name;
			this.creationTime = creationTime.ToString("yyyy/MM/dd HH:mm");
		}

		public DriveConfig() { }
	}
}
