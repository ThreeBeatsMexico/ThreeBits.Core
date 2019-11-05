using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ThreeBits.Entities.Common
{
    [DataContract]
    public class RespuestaGralBE
    {

        private bool bFLAG;
        [DataMember]
        public bool FLAG
        {
            get { return bFLAG; }
            set { bFLAG = value; }
        }

        private string sERRORMESSAGE;
        [DataMember]
        public string ERRORMESSAGE
        {
            get { return sERRORMESSAGE; }
            set { sERRORMESSAGE = value; }
        }

        private string sTRACE;
        [DataMember]
        public string TRACE
        {
            get { return sTRACE; }
            set { sTRACE = value; }
        }
    }
}
