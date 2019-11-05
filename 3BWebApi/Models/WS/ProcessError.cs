using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _3BWebApi.Models.WS
{
    public class ProcessError
    {
        public int Code { get; set; }
        public string ErrorDescription { get; set; }

        public string UIErrorMessage { get; set; }
    }
}