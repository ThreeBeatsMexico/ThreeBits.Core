using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ThreeBits.Entities.Security
{
    [DataContract]
    public class PermisoXElementosObjBE
    {
        private Int64 iIDELEMENTOSXOBJ;
        [DataMember]
        public Int64 IDELEMENTOSXOBJ
        {
            get { return iIDELEMENTOSXOBJ; }
            set { iIDELEMENTOSXOBJ = value; }
        }

        private Int64 iIDPERMISOSOBJ;
        [DataMember]
        public Int64 IDPERMISOSOBJ
        {
            get { return iIDPERMISOSOBJ; }
            set { iIDPERMISOSOBJ = value; }
        }

        private String sELEMENTO;
        [DataMember]
        public String ELEMENTO
        {
            get { return sELEMENTO; }
            set { sELEMENTO = value; }
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
