using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomZfile.Models
{
	public static class ResultBeanCode
	{
		public const int SUCCESS = 0;
		public const int FAIL = -1;
        public const int REQUIRED_PASSWORD = -2;
        public const int INVALID_PASSWORD = -3;
    }
    public class ResultBean
    {
        public string msg { get; set; }
        public int code { get; set; }
        public object data { get; set; }

        public ResultBean(string msg, int code, object data)
        {
            this.msg = msg;
            this.code = code;
            this.data = data;
        }

        public static ResultBean Success()
        {
            return new ResultBean("操作成功", ResultBeanCode.SUCCESS, null);
        }
        public static ResultBean Success(object data)
        {
            return new ResultBean("操作成功", ResultBeanCode.SUCCESS, data);
        }

        public static ResultBean Success(string msg, object data)
        {
            return new ResultBean(msg, ResultBeanCode.SUCCESS, data);
        }

        public static ResultBean Success(string msg, int code, object data)
        {
            return new ResultBean(msg, code, data);
        }

        public static ResultBean Error(string msg)
        {
            return new ResultBean(msg, ResultBeanCode.FAIL, null);
        }

		public static ResultBean Error(string msg, int code)
        {
            return new ResultBean(msg, code, null);
        }
    }
}
