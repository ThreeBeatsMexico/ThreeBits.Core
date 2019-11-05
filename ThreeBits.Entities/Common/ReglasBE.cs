using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ThreeBits.Entities.Common
{
    [DataContract]
    public class ReglasBE
    {
        private bool bACTIVO;
        [DataMember]
        public bool ACTIVO
        {
            get { return bACTIVO; }
            set { bACTIVO = value; }
        }

        private string sUSUARIO;
        [DataMember]
        public string USUARIO
        {
            get { return sUSUARIO; }
            set { sUSUARIO = value; }
        }

        private Int32 iTIPOBUSQUEDA;
        [DataMember]
        public Int32 TIPOBUSQUEDA
        {
            get { return iTIPOBUSQUEDA; }
            set { iTIPOBUSQUEDA = value; }
        }

        private Int64 iIDUSRMODIF;
        [DataMember]
        public Int64 IDUSRMODIF
        {
            get { return iIDUSRMODIF; }
            set { iIDUSRMODIF = value; }
        }

        private Int64 iIDAPP;
        [DataMember]
        public Int64 IDAPP
        {
            get { return iIDAPP; }
            set { iIDAPP = value; }
        }

    }
}
