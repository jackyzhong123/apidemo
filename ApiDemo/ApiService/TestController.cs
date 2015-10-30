using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ApiDemo.ApiService
{
     [RoutePrefix("api/test")]
    public class TestController : ApiController
    {
          [HttpGet]
        [Route("get")] 
         public IHttpActionResult xxx(string xx, int uu)
         {
                  return Json(new
            {
                Code = 10000,
                Detail = new
                {
                   xxxx =xx,
                   uuu = uu
                }
            });
         }

          [HttpPost]
          [Route("post")]
          public IHttpActionResult xxx([FromBody] TD_Test model)
          {
              return Json(new
              {
                  Code = 10000,
                  Detail = new
                  {
                      xxxx = model.xx,
                      uuu = model.uu
                  }
              });
          }
    }
}
