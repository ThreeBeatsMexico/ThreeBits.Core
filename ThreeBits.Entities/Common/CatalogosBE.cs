using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ThreeBits.Entities.Common
{
    [DataContract]
    public class CatalogosBE
    {
        private string sDESCRIPCION;
        [DataMember]
        public string DESCRIPCION
        {
            get { return sDESCRIPCION; }
            set { sDESCRIPCION = value; }
        }

        private string sID;
        [DataMember]
        public string ID
        {
            get { return sID; }
            set { sID = value; }
        }

    }
}
