using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ThreeBits.Entities.User
{
    [DataContract]
    public class RolesBE
    {
        private Int64 iIDROL;
        [DataMember]
        public Int64 IDROL
        {
            get { return iIDROL; }
            set { iIDROL = value; }
        }

        private Int64 iIDAPLICACION;
        [DataMember]
        public Int64 IDAPLICACION
        {
            get { return iIDAPLICACION; }
            set { iIDAPLICACION = value; }
        }

        private String sDESCRIPCION;
        [DataMember]
        public String DESCRIPCION
        {
            get { return sDESCRIPCION; }
            set { sDESCRIPCION = value; }
        }

        private bool bACTIVO;
        [DataMember]
        public bool ACTIVO
        {
            get { return bACTIVO; }
            set { bACTIVO = value; }
        }

    }
}
