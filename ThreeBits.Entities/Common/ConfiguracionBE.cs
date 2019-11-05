using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ThreeBits.Entities.Common
{
    [DataContract]
    public class ConfiguracionBE
    {
        [DataMember]
        public string psIDCONFIGAPP { get; set; }
        [DataMember]
        public string psIDCONFIGAPPNEW { get; set; }
        [DataMember]
        public string psDESCRIPCION { get; set; }
        [DataMember]
        public string psVALOR { get; set; }
        [DataMember]
        public string psACTIVO { get; set; }
    }
}
