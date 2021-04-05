using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreeBits.Entities.Common
{
   public class ProcessResult
    {
        private bool bFLAG;
        public bool flag
        {
            get { return bFLAG; }
            set { bFLAG = value; }
        }

        private string sERRORMESSAGE;
        public string errorMessage
        {
            get { return sERRORMESSAGE; }
            set { sERRORMESSAGE = value; }
        }

        private object sData;
        public object data
        {
            get { return sData; }
            set { sData = value; }
        }
    }
}
