using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CustomZfile.Models;
using CustomZfile.Services;
using ClrLibCustomZfile;
using System.IO;
using System.Net.Http;
using System.Net;

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

		// Useless but avoiding front-end bug.
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

		[HttpGet("audio-info")]
		public ResultBean GetAudioInfo(string url)
		{
			return ResultBean.Success(fileManager.GetAudioInfo(url));
		}

		[HttpDelete("del-file")]
		public ResultBean DeleteFile(int driveId, string pathToDrive)
		{
			if (LocalFileManager.DelFile(SystemManager.BasePath + "/" + driveId.ToString() + "/" + pathToDrive))
			{
				return ResultBean.Success();
			}
			return ResultBean.Error("删除失败");
		}

		[HttpDelete("del-dir")]
		public ResultBean DeleteDir(int driveId, string pathToDrive)
		{
			if (LocalFileManager.DelDir(combineDrivePath(driveId, pathToDrive)))
			{
				return ResultBean.Success();
			}
			return ResultBean.Error("删除失败");
		}

		[HttpPost("update-folder")]
		public ResultBean updateFolder(int driveId, string pathToDrive)
		{
			if (LocalFileManager.CreateDir(combineDrivePath(driveId, pathToDrive)))
			{
				return ResultBean.Success();
			}
			return ResultBean.Error("创建失败");
		}

		[HttpGet("download-file")]
		public IActionResult downloadFile(int driveId, string pathToDrive)
		{
			string fileName = pathToDrive;
			if (pathToDrive.Contains("/"))
			{
				fileName = pathToDrive.Substring(pathToDrive.LastIndexOf('/') + 1);
			}

			byte[] byteArray = System.IO.File.ReadAllBytes(combineDrivePath(driveId, pathToDrive));

			var f = new FileContentResult(byteArray, "application/octet-stream");
			f.FileDownloadName = fileName;
			return f;
		}

		[HttpPost("upload-file")]
		public async Task<ResultBean> uploadFile([FromForm] FileUploadModel fm)
		{
			if (fm.file.Length > 0)
			{
				var filePath = combineDrivePath(fm.driveId, fm.pathToDrive) + "/" + fm.file.FileName;
				using (var fileStream = new FileStream(filePath, FileMode.Create))
				{
					try
					{
						await fm.file.CopyToAsync(fileStream);
					}
					catch(Exception e)
					{
						return ResultBean.Error("保存失败");
					}
				}
			}
			return ResultBean.Success();
		}

		private string combineDrivePath(int driveId, string pathToDrive)
		{
			string path = SystemManager.BasePath + "/" + driveId.ToString();
			if (pathToDrive !=null && pathToDrive != "")
			{
				path += "/" + pathToDrive;
			}
			return path;
		}


	}
}