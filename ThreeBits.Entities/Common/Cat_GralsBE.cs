using System;
using System.Runtime.Serialization;



namespace ThreeBits.Entities.Common
{


    [DataContract]
    public class Cat_GralsBE
    {
        private Int64 iIDCATGRAL;
        [DataMember]
        public Int64 IDCATGRAL
        {
            get { return iIDCATGRAL; }
            set { iIDCATGRAL = value; }
        }

        private string sNOMBRETABLA;
        [DataMember]
        public string NOMBRETABLA
        {
            get { return sNOMBRETABLA; }
            set { sNOMBRETABLA = value; }
        }

        private string sIDTABLA;
        [DataMember]
        public string IDTABLA
        {
            get { return sIDTABLA; }
            set { sIDTABLA = value; }
        }

        private string sDESCRIPCIONTABLA;
        [DataMember]
        public string DESCRIPCIONTABLA
        {
            get { return sDESCRIPCIONTABLA; }
            set { sDESCRIPCIONTABLA = value; }
        }

        private string sIDFILTRO;
        [DataMember]
        public string IDFILTRO
        {
            get { return sIDFILTRO; }
            set { sIDFILTRO = value; }
        }
    }
}
