using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreeBits.Entities.Security
{
   public class Credential
    {
        private string sUSER;
        public string userName
        {
            get { return sUSER; }
            set { sUSER = value; }
        }

        private string sPASSWORD;
        public string password
        {
            get { return sPASSWORD; }
            set { sPASSWORD = value; }
        }

        private string sXAPPID;
        public string xAppId
        {
            get { return sXAPPID; }
            set { sXAPPID = value; }
        }

        private string sNAME;
        public string name
        {
            get { return sNAME; }
            set { sNAME = value; }
        }

        private string sIDUSUARIO;
        public string idUser
        {
            get { return sIDUSUARIO; }
            set { sIDUSUARIO = value; }
        }

        private int? sTIPOBUSQUEDA;
        public int? tipoBusqueda
        {
            get { return sTIPOBUSQUEDA; }
            set { sTIPOBUSQUEDA = value; }
        }

        private bool bENCRIPTAPASSWORD;
        public bool encriptaPassword
        {
            get { return bENCRIPTAPASSWORD; }
            set { bENCRIPTAPASSWORD = value; }
        }


        private string bROLID;
        public string rolId
        {
            get { return bROLID; }
            set { bROLID = value; }
        }

        private bool bRecordarme;
        public bool Recordarme
        {
            get { return bRecordarme; }
            set { bRecordarme = value; }
        }
    }
}
