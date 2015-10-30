using ApiDemo.Infrastructure;
using ApiDemo.Models.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace ApiDemo.ApiService
{
    /// <summary>
    /// 系统通用模块
    /// </summary>
    [RoutePrefix("api/system")]
    public class SystemController : ApiController
    {
         

        /// <summary>
        /// 获取系统配置
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("config/mobile")]
        public async Task<IHttpActionResult> ConfigMobile([FromBody] TD_Register model)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// 获取验证码
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("vcode/send/sms")]
        public async Task<IHttpActionResult> VcodeSendSMS(string m)
        {
            if (UtilityHelper.ConstVar.testAccount.Any(u => u == m))
            {
                return Json(new
                {
                    Code = 10000,
                    Detail = new { }
                });
            }
            if (!UtilityHelper.IsMobilePhone(m))
            {
                return Json(new
                {
                    Code = 1,
                    Message = "手机格式不正确"
                });
            }

            Random ran = new Random();
            int RandKey = ran.Next(1000, 9999);

            try
            {

                string mobile = m,
                message = "验证码：" + RandKey.ToString() + " ，两分钟内有效【活动邮】",
                username = ConfigurationManager.AppSettings["SMSUsername"],
                password = ConfigurationManager.AppSettings["SMSKey"],
                url = ConfigurationManager.AppSettings["SMSUrl"];
                byte[] byteArray = Encoding.UTF8.GetBytes("mobile=" + mobile + "&message=" + message);
                HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(new Uri(url));
                string auth = "Basic " + Convert.ToBase64String(System.Text.Encoding.Default.GetBytes(username + ":" + password));
                webRequest.Headers.Add("Authorization", auth);
                webRequest.Method = "POST";
                webRequest.ContentType = "application/x-www-form-urlencoded";
                webRequest.ContentLength = byteArray.Length;

                Stream newStream = webRequest.GetRequestStream();
                newStream.Write(byteArray, 0, byteArray.Length);
                newStream.Close();
                HttpWebResponse response = (HttpWebResponse)webRequest.GetResponse();
                StreamReader php = new StreamReader(response.GetResponseStream(), Encoding.Default);
                string Message = php.ReadToEnd();
            }
            catch
            {
                return Json(new
                {
                    Code = 1,
                    Message = "验证码服务器有误"
                });
            }

            DataBaseEntities db = new DataBaseEntities();

            var verify = new cm_SMS_Verify
            {
                Id = Guid.NewGuid().ToString(),
                Code = RandKey,
                CreateDate = DateTime.Now,
                Mobile = m
            };

            db.cm_SMS_Verify.Add(verify);
            db.SaveChanges();

            return Json(new
            {
                Code = 10000,
                Detail = new
                {

                }
            }); 
        }

        /// <summary>
        /// 验证手机验证码
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("vcode/verify/sms")]
        public async Task<IHttpActionResult> VcodeVerifySMS(string mp, int vcode)
        {
            if (UtilityHelper.VerifyMobileCode(mp, vcode, false))
            {
                DataBaseEntities db = new DataBaseEntities();
                return Json(new
                {
                    Code = 10000,
                    Detail = db.AspNetUsers.Any(u => u.MyMobilePhone == mp)
                });
            }
            else
            {
                return Json(new
                {
                    Code = 1,
                    Message = "验证码不正确"
                });
            }
        }


         

    }
}
