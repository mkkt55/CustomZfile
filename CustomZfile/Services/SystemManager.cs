using CustomZfile.Models;
using CustomZfile.DbTools;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Reflection;
using ClrLibCustomZfile;


namespace CustomZfile.Services
{
	public class SystemManager
	{
		public const string WwwRoot = "wwwroot";
		public const string FileRoot = "fileroot";
		public const string BasePath = "wwwroot/fileroot";

		private const string EncryptionAssemblyName = "GlobalAssemblyCustomZfile, Version=1.0.0.0, Culture=neutral, PublicKeyToken=747f6a76d53f4723";

		public static string StartTime { get; } = DateTime.Now.ToString("yyyy/MM/dd HH:mm");

		private SystemConfig systemConfig;

		private CustomZfileDbContext dbContext = new CustomZfileDbContext(); 

		public SystemManager()
		{
			systemConfig = new SystemConfig();
		}

		public DriveConfig GetDriveConfigById(int id)
		{
			return systemConfig.driveConfigs[id];
		}

		public List<DriveConfig> ListAllDrives()
		{
			List<DriveConfig> driveConfigs = new List<DriveConfig>();
			using (var context = new CustomZfileDbContext())
			{
				var result = (from d in context.drive select d).ToList(); 
				foreach (Drive d in result)
				{
					driveConfigs.Add(new DriveConfig(d.id, d.name, d.creation_time));
				}
			}
			return driveConfigs;
		}

		public bool EditDrive(DriveConfig driveConfig)
		{
			using (var context = new CustomZfileDbContext())
			{
				Drive d = context.drive.Find(driveConfig.id);
				if (d == null)
				{
					return false;
				}
				d.name = driveConfig.name;
				context.drive.Update(d);
				context.SaveChanges();
			}
			return true;
		}
		
		public int SaveNewDrive(string driveName, int userId)
		{
			Drive newDrive = new Drive { name = driveName, creator_id = userId };
			int id;
			using (var context = new CustomZfileDbContext())
			{
				var result = context.drive.Add(newDrive);
				context.SaveChanges();
				id = result.Entity.id;
			}
			LocalFileManager.CreateDir(BasePath + "/" + id.ToString());
			return id;
		}

		public bool DeleteDriveById(int driveId)
		{
			using (var context = new CustomZfileDbContext())
			{
				context.Remove(new Drive { id = driveId });
				context.SaveChanges();
			}
			LocalFileManager.DelDir(BasePath + "/" + driveId.ToString());
			return true;
		}


		public SystemConfig GetSystemConfig()
		{
			return systemConfig;
		}

		public bool UpdatePassword(string username, string password, string newPassword)
		{
			User u = UserExist(username, password);
			if (u == null)
			{
				return false;
			}
			u.password = newPassword;
			using (var context = new CustomZfileDbContext())
			{
				context.user.Update(u);
				context.SaveChanges();
			}
			return true;
		}

		public User UserExist(string username, string password)
		{
			using (var context = new CustomZfileDbContext())
			{
				return context.user.SingleOrDefault(user => user.username == username && user.password == password);
			}
		}

		// Implement by assembly in GAC.
		public static string Encrypt(string str)
		{
			var asm = Assembly.Load(EncryptionAssemblyName);
			Type t = asm.GetType("EncryptionLib.Encryption");

			if (t == null)
			{
				throw new Exception("No such class exists.");
			}

			var methodInfoStatic = t.GetMethod("AesEncrypt");
			if (methodInfoStatic == null)
			{
				throw new Exception("No such static method exists.");
			}

			object[] staticParameters = new object[1];
			staticParameters[0] = str;

			// Invoke static method
			return (string)methodInfoStatic.Invoke(null, staticParameters);
		}

		// Implement by assembly in GAC.
		public static string Decrypt(string str)
		{
			var asm = Assembly.Load(EncryptionAssemblyName);
			Type t = asm.GetType("EncryptionLib.Encryption");
			
			if (t == null)
			{
				throw new Exception("No such class exists.");
			}

			var methodInfoStatic = t.GetMethod("AesDecrypt");
			if (methodInfoStatic == null)
			{
				throw new Exception("No such static method exists.");
			}

			object[] staticParameters = new object[1];
			staticParameters[0] = str;

			// Invoke static method
			return (string)methodInfoStatic.Invoke(null, staticParameters);
		}

	}
}
