using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CustomZfile.Models
{

    public class FileItem
	{
        public string name { get; set; }
        public string time { get; set; }
        public long size { get; set; }
        public string type { get; set; }
        public string path { get; set; }
        public string url { get; set; }

        public FileItem(string name, long size, DateTime lastModifyTime, bool isDir, string path, string url)
        {
            this.name = name;
            time = lastModifyTime.ToString("yyyy/MM/dd HH:mm");
            if (isDir)
            {
                type = "FOLDER";
            }
            else
            {
                type = "FILE";
            }
            this.size = size;
            this.path = path;
            this.url = url;
        }

        public FileItem() { }
    }
}
