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
        public ResultBean EditDrive(Drive driveConfig)
        {
            return ResultBean.Success();
        }

        [HttpDelete("drive/{id}")]
        public ResultBean DeleteDrive(int id)
        {
            if (systemManager.DeleteDriveById(id))
            {
                return ResultBean.Success();
            }
            return ResultBean.Error("Deletion fail");
        }

        [HttpPost("update-pwd")]
        public ResultBean UpdatePwd(string username, string oldPassword, string rePassword)
        {
            return ResultBean.Success();
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
                HttpContext.Response.Cookies.Append("userId", systemManager.Encrypt(u.id.ToString()));
                HttpContext.Response.Cookies.Append("username", systemManager.Encrypt(username));
                HttpContext.Response.Cookies.Append("password", systemManager.Encrypt(password));
                return ResultBean.Success("登陆成功");
            }
            else
            {
                return ResultBean.Error("登录失败");
            }
        }
    }
}