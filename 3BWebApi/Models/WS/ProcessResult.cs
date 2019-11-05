using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _3BWebApi.Models.WS
{
    public class ProcessResult
    {
        public Process Process { get; set; }
        public bool HasError { get; set; }
        public ProcessError Error { get; set; }
        public int ReturnValue { get; set; }
        public string TextData { get; set; }

    }
}