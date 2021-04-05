using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreeBits.Entities.Security
{
    public class TokenJwt
    {
        private string sTOKEN;
        public string tokenId
        {
            get { return sTOKEN; }
            set { sTOKEN = value; }
        }

        private string sPROFILEID;
        public string profileId
        {
            get { return sPROFILEID; }
            set { sPROFILEID = value; }
        }

        private string sTOKENREFRESH;
        public string tokenRefresh
        {
            get { return sTOKENREFRESH; }
            set { sTOKENREFRESH = value; }
        }

    }
}
