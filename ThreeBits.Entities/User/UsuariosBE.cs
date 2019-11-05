using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ThreeBits.Entities.User
{
    [DataContract]
    public class UsuariosBE
    {
        private Int64 iIDUSUARIO;
        [DataMember]
        public Int64 IDUSUARIO
        {
            get { return iIDUSUARIO; }
            set { iIDUSUARIO = value; }
        }

        private Int64 iIDAPLICACION;
        [DataMember]
        public Int64 IDAPLICACION
        {
            get { return iIDAPLICACION; }
            set { iIDAPLICACION = value; }
        }

        private Int32 iIDSEXO;
        [DataMember]
        public Int32 IDSEXO
        {
            get { return iIDSEXO; }
            set { iIDSEXO = value; }
        }

        private Int32 iIDTIPOPERSONA;
        [DataMember]
        public Int32 IDTIPOPERSONA
        {
            get { return iIDTIPOPERSONA; }
            set { iIDTIPOPERSONA = value; }
        }

        private Int32 iIDESTADOCIVIL;
        [DataMember]
        public Int32 IDESTADOCIVIL
        {
            get { return iIDESTADOCIVIL; }
            set { iIDESTADOCIVIL = value; }
        }

        private Int32 iIDAREA;
        [DataMember]
        public Int32 IDAREA
        {
            get { return iIDAREA; }
            set { iIDAREA = value; }
        }

        private String sDESCAREA;
        [DataMember]
        public String DESCAREA
        {
            get { return sDESCAREA; }
            set { sDESCAREA = value; }
        }

        private Int32 iIDTIPOUSUARIO;
        [DataMember]
        public Int32 IDTIPOUSUARIO
        {
            get { return iIDTIPOUSUARIO; }
            set { iIDTIPOUSUARIO = value; }
        }

        private String sDESCTIPOUSUARIO;
        [DataMember]
        public String DESCTIPOUSUARIO
        {
            get { return sDESCTIPOUSUARIO; }
            set { sDESCTIPOUSUARIO = value; }
        }

        private String sIDUSUARIOAPP;
        [DataMember]
        public String IDUSUARIOAPP
        {
            get { return sIDUSUARIOAPP; }
            set { sIDUSUARIOAPP = value; }
        }

        private String sAPATERNO;
        [DataMember]
        public String APATERNO
        {
            get { return sAPATERNO; }
            set { sAPATERNO = value; }
        }

        private String sAMATERNO;
        [DataMember]
        public String AMATERNO
        {
            get { return sAMATERNO; }
            set { sAMATERNO = value; }
        }

        private String sNOMBRE;
        [DataMember]
        public String NOMBRE
        {
            get { return sNOMBRE; }
            set { sNOMBRE = value; }
        }

        private DateTime? dtFECHANACCONST;
        [DataMember]
        public DateTime? FECHANACCONST
        {
            get { return dtFECHANACCONST; }
            set { dtFECHANACCONST = value; }
        }

        private String sUSUARIO;
        [DataMember]
        public String USUARIO
        {
            get { return sUSUARIO; }
            set { sUSUARIO = value; }
        }

        private String sPASSWORD;
        [DataMember]
        public String PASSWORD
        {
            get { return sPASSWORD; }
            set { sPASSWORD = value; }
        }

        private String sRUTAFOTOPERFIL;
        [DataMember]
        public String RUTAFOTOPERFIL
        {
            get { return sRUTAFOTOPERFIL; }
            set { sRUTAFOTOPERFIL = value; }
        }

        private DateTime dtFECHAALTA;
        [DataMember]
        public DateTime FECHAALTA
        {
            get { return dtFECHAALTA; }
            set { dtFECHAALTA = value; }
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
