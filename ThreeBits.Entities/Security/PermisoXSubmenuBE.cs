using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ThreeBits.Entities.Security
{
    [DataContract]
    public class PermisoXSubmenuBE
    {

        private Int64 iIDPERMISOSXSUBMENU;
        [DataMember]
        public Int64 IDPERMISOSXSUBMENU
        {
            get { return iIDPERMISOSXSUBMENU; }
            set { iIDPERMISOSXSUBMENU = value; }
        }

        private Int64 iIDPERMISOSMENU;
        [DataMember]
        public Int64 IDPERMISOSMENU
        {
            get { return iIDPERMISOSMENU; }
            set { iIDPERMISOSMENU = value; }
        }

        private String sNOMBRESUBMENU;
        [DataMember]
        public String NOMBRESUBMENU
        {
            get { return sNOMBRESUBMENU; }
            set { sNOMBRESUBMENU = value; }
        }

        private String sIMAGEN;
        [DataMember]
        public String IMAGEN
        {
            get { return sIMAGEN; }
            set { sIMAGEN = value; }
        }

        private String sTIPOOBJETO;
        [DataMember]
        public String TIPOOBJETO
        {
            get { return sTIPOOBJETO; }
            set { sTIPOOBJETO = value; }
        }

        private String sURL;
        [DataMember]
        public String URL
        {
            get { return sURL; }
            set { sURL = value; }
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

        private Int32 iORDENSUBMENU;
        [DataMember]
        public Int32 ORDENSUBMENU
        {
            get { return iORDENSUBMENU; }
            set { iORDENSUBMENU = value; }
        }
    }
}
