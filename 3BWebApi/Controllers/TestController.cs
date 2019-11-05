using _3BWebApi.Models.WS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace _3BWebApi.Controllers
{
    public class TestController : ApiController
    {
        [HttpGet]
        public ProcessResult HelloWorld()
        {
            ProcessResult oRes = new ProcessResult();

            oRes.HasError = false;
            oRes.TextData = "Hola Mundo";
            oRes.ReturnValue = 1;

            return oRes;
        }

    }
}
