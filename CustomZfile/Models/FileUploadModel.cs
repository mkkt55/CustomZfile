using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomZfile.Models
{
	public class FileUploadModel
	{
		public int driveId { get; set; }
		public string pathToDrive { get; set; }
		public IFormFile file { get; set; }
	}
}
