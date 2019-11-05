using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ThreeBits.Entities.User
{
    [DataContract]
    public class RelacionTipoUsuarioBE
    {
        private Int64 iIDRELACION;
        [DataMember]
        public Int64 IDRELACION
        {
            get { return iIDRELACION; }
            set { iIDRELACION = value; }
        }

        private Int64 iIDUSUARIOFROM;
        [DataMember]
        public Int64 IDUSUARIOFROM
        {
            get { return iIDUSUARIOFROM; }
            set { iIDUSUARIOFROM = value; }
        }

        private Int64 iIDUSUARIOTO;
        [DataMember]
        public Int64 IDUSUARIOTO
        {
            get { return iIDUSUARIOTO; }
            set { iIDUSUARIOTO = value; }
        }

        private String sDESCRIPCION;
        [DataMember]
        public String DESCRIPCION
        {
            get { return sDESCRIPCION; }
            set { sDESCRIPCION = value; }
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
