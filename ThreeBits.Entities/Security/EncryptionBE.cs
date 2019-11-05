using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ThreeBits.Entities.Security
{
    [DataContract]
    public class EncryptionBE
    {
        private string sVALORIN;
        [DataMember]
        public string VALORIN
        {
            get { return sVALORIN; }
            set { sVALORIN = value; }
        }

        private string sVALOROUT;
        [DataMember]
        public string VALOROUT
        {
            get { return sVALOROUT; }
            set { sVALOROUT = value; }
        }
    }
}
