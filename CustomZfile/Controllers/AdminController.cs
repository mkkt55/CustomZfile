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
    [Route("/admin")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private SystemManager systemManager = new SystemManager();

        [HttpGet("config")]
        public ResultBean GetConfig()
        {
            return ResultBean.Success(systemManager.GetSystemConfig());
        }

        [HttpGet("monitor")]
        public ResultBean monitor()
        {
            return ResultBean.Success(new SystemMonitorInfo());
        }

        [HttpPost("drive")]
        public ResultBean EditDrive(DriveConfig driveConfig)
        {
            if (driveConfig.id == null)
            {
                int userId;
                if (int.TryParse(SystemManager.Decrypt(HttpContext.Request.Cookies["userId"]), out userId))
                {
                    systemManager.SaveNewDrive(driveConfig.name, userId);
                    return ResultBean.Success();
                }
            }
            else
            {
                if (systemManager.EditDrive(driveConfig))
                {
                    return ResultBean.Success();
                }
            }
            return ResultBean.Error("未知错误");
        }

        [HttpDelete("drive/{id}")]
        public ResultBean DeleteDrive(int id)
        {
            if (systemManager.DeleteDriveById(id))
            {
                return ResultBean.Success();
            }
            return ResultBean.Error("删除失败");
        }

        [HttpPost("update-pwd")]
        public ResultBean UpdatePwd([FromForm] LoginForm loginForm)
        {
            if (systemManager.UpdatePassword(loginForm.username, loginForm.password, loginForm.newPassword))
            {
                return ResultBean.Success("密码修改成功");
            }
            return ResultBean.Error("原密码错误");
        }



        [HttpPost("/login")]
        public ResultBean Login([FromForm] LoginForm loginForm)
        {
            string username = loginForm.username;
            string password = loginForm.password;

            var co = new CookieOptions();
            co.MaxAge = TimeSpan.FromDays(180);
            User u;
            if (username != null && password != null && (u = systemManager.UserExist(username, password)) != null)
            {
                HttpContext.Response.Cookies.Append("userId", SystemManager.Encrypt(u.id.ToString()));
                HttpContext.Response.Cookies.Append("username", SystemManager.Encrypt(username));
                HttpContext.Response.Cookies.Append("password", SystemManager.Encrypt(password));
                return ResultBean.Success("登陆成功");
            }
            else
            {
                return ResultBean.Error("登录失败");
            }
        }
    }
}