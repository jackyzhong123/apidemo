using ApiDemo.Infrastructure;
using ImageProcessor;
using ImageProcessor.Imaging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace ApiDemo.ApiService
{
    /// <summary>
    /// 上传图片
    /// </summary>
   
    public class UploadImageController : ApiController
    {
        /// <summary>
        /// 上传图片
        /// </summary>
        /// <returns></returns>
        public async Task<IHttpActionResult> Post()
        {
             
            try
            {
                string fileName = Guid.NewGuid().ToString();
                string fileOrginalFile = HttpContext.Current.Server.MapPath("~/MyUpload/" + Guid.NewGuid().ToString() + ".jpg");


               ;
               string fileResized800Name = HttpContext.Current.Server.MapPath("~/MyUpload/" + Guid.NewGuid().ToString() + ".jpg");
               
                System.Drawing.Bitmap image = new System.Drawing.Bitmap(HttpContext.Current.Request.InputStream);

                image.Save(fileOrginalFile);


                using (var imageFactory = new ImageFactory(preserveExifData: true))
                {
                    
                 System.Drawing.Size     size = new System.Drawing.Size(800, 1600);
                    ResizeLayer resize = new ResizeLayer(size, ResizeMode.Max);
                    imageFactory.Load(fileOrginalFile).Resize(resize).Save(fileResized800Name);


                }
                BlobHelper blob = new BlobHelper(BlobString.Portrait);
                string contentType = "image/jpeg";
               
                await blob.UploadFile(fileResized800Name, fileName , contentType);

                //删除文件
                File.Delete(fileOrginalFile);
             
                File.Delete(fileResized800Name);



                return Json(new
                {
                    Code = 10000,
                    Detail = "http://hdy.awblob.com/portrait/" + fileName
                });

            }

            catch (Exception ex)
            {
                return Json(new
                {
                    Code = 10,
                    Message = "上传失败"

                });
            }

        }
    }
}
