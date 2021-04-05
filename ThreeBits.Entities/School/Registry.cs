using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreeBits.Entities.School
{
    public class Registry
    {

        private int iTipoRol;
        public int tipoRol
        {
            get { return iTipoRol; }
            set { iTipoRol = value; }
        }
      
       
        private string sPASSWORD;
        public string password
        {
            get { return sPASSWORD; }
            set { sPASSWORD = value; }
        }

        private string sCPASSWORD;
        public string cpassword
        {
            get { return sCPASSWORD; }
            set { sCPASSWORD = value; }
        }

        private string sXAPPID;
        public string xAppId
        {
            get { return sXAPPID; }
            set { sXAPPID = value; }
        }

        private string sEMAIL;
        public string email
        {
            get { return sEMAIL; }
            set { sEMAIL = value; }
        }


    }
}
