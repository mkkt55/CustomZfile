using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CustomZfile.Models;
using Microsoft.AspNetCore.Mvc;

namespace CustomZfile.Services
{
	public class FileManager
	{
		public static FileItem GetFileItem(int driveId, string path)
		{
			return new FileItem();
		}

		public static List<FileItem> ListFiles(int driveId, string path)
		{
			if (path != "" && path.IndexOf("/") == 0)
			{
				path = path.Substring(1, path.Length - 1);
			}
			if (path != "" && path.LastIndexOf("/") == path.Length - 1)
			{
				path = path.Substring(0, path.Length - 1);
			}

			string webUrl = SystemManager.FileRoot + "/" + driveId.ToString();
			if (path != "")
			{
				webUrl += "/" + path;
			}
			string actualPath = SystemManager.WwwRoot + "/" + webUrl;

			DirectoryInfo folder = new DirectoryInfo(actualPath);
			if (!folder.Exists)
			{
				return null;
			}
			FileInfo[] fileInfos = folder.GetFiles("*");
			DirectoryInfo[] directoryInfos = folder.GetDirectories();

			List<FileItem> returnedFiles = new List<FileItem>();

			foreach (DirectoryInfo dir in directoryInfos)
			{
				returnedFiles.Add(new FileItem(dir.Name, 0, dir.CreationTime, true, path, webUrl + "/" + dir.Name));
			}

			foreach (FileInfo file in fileInfos)
			{
				returnedFiles.Add(new FileItem(file.Name, file.Length, file.CreationTime, false, path, webUrl + "/" + file.Name));
			}

			return returnedFiles;
		}

		public static AudioInfo GetAudioInfo(string url)
		{
			return new AudioInfo();
		}
	}
}
