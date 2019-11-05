using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ThreeBits.Entities.User
{
    [DataContract]
    public class RolesXUsuarioBE
    {
        private Int64 iIDROLXUSUARIO;
        [DataMember]
        public Int64 IDROLXUSUARIO
        {
            get { return iIDROLXUSUARIO; }
            set { iIDROLXUSUARIO = value; }
        }

        private Int64 iIDROL;
        [DataMember]
        public Int64 IDROL
        {
            get { return iIDROL; }
            set { iIDROL = value; }
        }

        private String sROL;
        [DataMember]
        public String ROL
        {
            get { return sROL; }
            set { sROL = value; }
        }

        private String sDESCROL;
        [DataMember]
        public String DESCROL
        {
            get { return sDESCROL; }
            set { sDESCROL = value; }
        }

        private Int64 iIDUSUARIO;
        [DataMember]
        public Int64 IDUSUARIO
        {
            get { return iIDUSUARIO; }
            set { iIDUSUARIO = value; }
        }

        private Int64 iIDESTACIONXAPP;
        [DataMember]
        public Int64 IDESTACIONXAPP
        {
            get { return iIDESTACIONXAPP; }
            set { iIDESTACIONXAPP = value; }
        }

        private bool bACTIVO;
        [DataMember]
        public bool ACTIVO
        {
            get { return bACTIVO; }
            set { bACTIVO = value; }
        }

        private string sIdAplicacion;
        [DataMember]
        public string IDAPLICACION
        {
            get { return sIdAplicacion; }
            set { sIdAplicacion = value; }
        }

        private string sAplicacion;
        [DataMember]
        public string APLICACION
        {
            get { return sAplicacion; }
            set { sAplicacion = value; }
        }

        [DataMember]
        public string RowIndex { get; set; }
    }
}
