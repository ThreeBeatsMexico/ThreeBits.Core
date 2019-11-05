using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ThreeBits.Entities.Common
{
    [DataContract]
    public class CatGeneralesBE
    {
        [DataMember]
        public string psIDCATGENERALES { get; set; }
        [DataMember]
        public string psNOMBRECATALOGO { get; set; }
        [DataMember]
        public string psIDCATALOGO { get; set; }
        [DataMember]
        public string psDESCRIPCION { get; set; }
        [DataMember]
        public string psFILTRO { get; set; }
        [DataMember]
        public string psACTIVO { get; set; }
        [DataMember]
        public string psIDSUBCATALOGO { get; set; }
        [DataMember]
        public string psVALORFILTRO { get; set; }
    }
}
