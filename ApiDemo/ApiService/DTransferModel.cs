using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiDemo.ApiService
{
    public class DTransferModel
    {

    }

   
   
    public class TD_Register
    {
        public string Mobile { get; set; }
        public string Password { get; set; }
        public int Code { get; set; }
    }

     

    public class TD_Test
    {
        public string xx { get; set; }
        public int uu { get; set; }
    }

    public class TD_Portrait
    {
        public string image { get; set; }
    }

    public class TD_Login
    {
        /// <summary>
        /// 手机号码
        /// </summary>
        public string Mobile { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 验证码
        /// </summary>
        public int Code { get; set; }
    }
}