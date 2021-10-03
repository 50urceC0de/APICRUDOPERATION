using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.Http.Results;
using TestEmployee.Models;

namespace TestEmployee.Controllers
{
    public class FormBodyAPIController : ApiController
    {
        public IEnumerable<string> Get()
        {
            var data = Request.GetQueryNameValuePairs().ToList();
            return new string[] { "value1", "value2" };
        }
        public string Get(int id)
        {
            var data = Request.GetQueryNameValuePairs().ToList();


            return "value";
        }
        public JsonResult<string> Post([FromBody] string value)
        {
            var data = Request.GetQueryNameValuePairs().ToList();

            var context = (HttpContextBase)Request.Properties["MS_HttpContext"];
            context.Request.InputStream.Seek(0, SeekOrigin.Begin);
            MemoryStream ms = new MemoryStream();
            context.Request.InputStream.CopyTo(ms);
            var jsonData = Encoding.ASCII.GetString(ms.ToArray());

            return Json(jsonData);

        }
    }
}
