using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CustomZfile.Models;
using CustomZfile.Services;
using ClrLibCustomZfile;

namespace CustomZfile.Controllers
{
    [Route("/api")]
    [ApiController]
    public class DriveManageController : ControllerBase
    {
        private SystemManager systemManager = new SystemManager();

        [Route("/drives")]
        [HttpGet]
        public ResultBean DriveList()
		{
			return ResultBean.Success(systemManager.ListAllDrives());
		}


		[Route("/drives/{id}")]
        [HttpGet]
        public ResultBean DriveItem(int id)
        {
            Console.WriteLine(id);
            DriveConfig driveConfig = systemManager.GetDriveConfigById(id);
            return ResultBean.Success(driveConfig);
        }


        [Route("/drive")]
        [HttpPost]
        public ResultBean SaveDriveItem(Drive driveConfig)
        {
            int userId;
            if (int.TryParse(SystemManager.Decrypt(HttpContext.Request.Cookies["userId"]), out userId))
            {
                systemManager.SaveNewDrive(driveConfig.name, userId);
                return ResultBean.Success();
            }
            return ResultBean.Error("userId error");
        }


        [Route("/drive/{id}")]
        [HttpDelete]
        public ResultBean DeleteDriveItem(int id)
        {
            int userId;
            if (int.TryParse(SystemManager.Decrypt(HttpContext.Request.Cookies["userId"]), out userId)){
                systemManager.DeleteDriveById(id);
                return ResultBean.Success();
            }
            return ResultBean.Error("userId error");
        }

    }
}