using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ThreeBits.Entities.Security
{
    [DataContract]
    public class AplicacionBE
    {
        private Int64 iIDAPLICACION;
        [DataMember]
        public Int64 IDAPLICACION
        {
            get { return iIDAPLICACION; }
            set { iIDAPLICACION = value; }
        }

        private Int64 iIDUSRSXAPP;
        [DataMember]
        public Int64 IDUSRSXAPP
        {
            get { return iIDUSRSXAPP; }
            set { iIDUSRSXAPP = value; }
        }

        private Int64 iIDUSUARIO;
        [DataMember]
        public Int64 IDUSUARIO
        {
            get { return iIDUSUARIO; }
            set { iIDUSUARIO = value; }
        }

        private String sDESCRIPCION;
        [DataMember]
        public String DESCRIPCION
        {
            get { return sDESCRIPCION; }
            set { sDESCRIPCION = value; }
        }

        private String sURLINICIO;
        [DataMember]
        public String URLINICIO
        {
            get { return sURLINICIO; }
            set { sURLINICIO = value; }
        }
        private String sPASSWORD;
        [DataMember]
        public String PASSWORD
        {
            get { return sPASSWORD; }
            set { sPASSWORD = value; }
        }

        private bool bACTIVO;
        [DataMember]
        public bool ACTIVO
        {
            get { return bACTIVO; }
            set { bACTIVO = value; }
        }
        private String sJWTKEY;

        public String jwtKey
        {
            get { return sJWTKEY; }
            set { sJWTKEY = value; }
        }

        private string sXAPPID;

        public string xAppId
        {
            get { return sXAPPID; }
            set { sXAPPID = value; }
        }

        private int iJWTEXPIRATION;

        public int jwtExpirationTime
        {
            get { return iJWTEXPIRATION; }
            set { iJWTEXPIRATION = value; }
        }

    }
}
