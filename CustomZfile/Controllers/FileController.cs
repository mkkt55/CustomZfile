using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CustomZfile.Models;
using CustomZfile.Services;

namespace CustomZfile.Controllers
{
	[Route("/api")]
	[ApiController]
	public class FileController : ControllerBase
	{
		private SystemManager systemManager = new SystemManager();
		private FileManager fileManager = new FileManager();

		private const int PAGE_SIZE = 30;

		[HttpGet("drive/list")]
		public ResultBean ListDrives()
		{
			return ResultBean.Success(systemManager.ListAllDrives());
		}

		[HttpGet("list/{driveId}")]
		public ResultBean List(int driveId, string path, string password, int page)
		{
			if (path == null)
			{
				path = "";
			}
			return ResultBean.Success(new { totalPage = 1, fileList = fileManager.ListFiles(driveId, path) });
		}

		[HttpGet("config/{driveId}")]
		public ResultBean GetDirectoryConfig(int driveId, string path)
		{
			return ResultBean.Success(new object());
		}

		[HttpGet("search/{driveId}")]
		public ResultBean Search(string name, string sortBy, string order, int page, int driveId)
		{
			return ResultBean.Error("暂不支持搜索功能");
		}

		[HttpGet("/directlink/{driveId}")]
		public ResultBean directlink(int driveId, string path)
		{
			return ResultBean.Success(fileManager.GetFileItem(driveId, path));
		}

		[HttpGet("audio-info")]
		public ResultBean GetAudioInfo(string url)
		{
			return ResultBean.Success(fileManager.GetAudioInfo(url));
		}
	}
}