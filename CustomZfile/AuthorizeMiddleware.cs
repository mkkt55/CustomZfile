﻿using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CustomZfile.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CustomZfile
{
    public class AuthorizeMiddleware
    {
        private readonly RequestDelegate _next;

        private SystemManager systemManager = new SystemManager();

        public AuthorizeMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            string username = context.Request.Cookies["username"];
            string password = context.Request.Cookies["password"];

            username = SystemManager.Decrypt(username);
            password = SystemManager.Decrypt(password);

            if (username != null && password != null && null != systemManager.UserExist(username, password))
            {
                await _next(context);
            } 
            else
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Unauthorized");
            }
        }
    }
}
