using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ThreeBits.Entities.Security
{
    [DataContract]
    public class PermisosXObjetosBE
    {
        private Int64 iIDPERMISOSOBJ;
        [DataMember]
        public Int64 IDPERMISOSOBJ
        {
            get { return iIDPERMISOSOBJ; }
            set { iIDPERMISOSOBJ = value; }
        }

        private Int64 iIDROL;
        [DataMember]
        public Int64 IDROL
        {
            get { return iIDROL; }
            set { iIDROL = value; }
        }

        private String sPAGINA;
        [DataMember]
        public String PAGINA
        {
            get { return sPAGINA; }
            set { sPAGINA = value; }
        }

        private String sNOMBREOBJETO;
        [DataMember]
        public String NOMBREOBJETO
        {
            get { return sNOMBREOBJETO; }
            set { sNOMBREOBJETO = value; }
        }

        private String sTIPOOBJETO;
        [DataMember]
        public String TIPOOBJETO
        {
            get { return sTIPOOBJETO; }
            set { sTIPOOBJETO = value; }
        }

        private String sTOOLTIP;
        [DataMember]
        public String TOOLTIP
        {
            get { return sTOOLTIP; }
            set { sTOOLTIP = value; }
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
