using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomZfile.Models
{
	public class AudioInfo
	{
		public string title { set; get; }
		public string artist { set; get; }
		public string cover { set; get; }
		public string source { set; get; }

		public AudioInfo(string title = "Unknown", string artist = "Unknown", string cover = "img/audio.png", string source = null)
		{
			this.title = title;
			this.artist = artist;
			this.cover = cover;
			this.source = source;
		}
	}
}
