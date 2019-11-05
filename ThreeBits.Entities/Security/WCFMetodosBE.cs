using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ThreeBits.Entities.Security
{
    [DataContract]
    public class WCFMetodosBE
    {
        private Int64 iIDMETODOS;
        [DataMember]
        public Int64 IDMETODOS
        {
            get { return iIDMETODOS; }
            set { iIDMETODOS = value; }
        }

        private Int64 iIDAPLICACION;
        [DataMember]
        public Int64 IDAPLICACION
        {
            get { return iIDAPLICACION; }
            set { iIDAPLICACION = value; }
        }

        private Int64 iIDSERVICIOS;
        [DataMember]
        public Int64 IDSERVICIOS
        {
            get { return iIDSERVICIOS; }
            set { iIDSERVICIOS = value; }
        }

        private String sNOMBREMETODO;
        [DataMember]
        public String NOMBREMETODO
        {
            get { return sNOMBREMETODO; }
            set { sNOMBREMETODO = value; }
        }

        private bool bRECURRENTE;
        [DataMember]
        public bool RECURRENTE
        {
            get { return bRECURRENTE; }
            set { bRECURRENTE = value; }
        }

        private bool bACTIVO;
        [DataMember]
        public bool ACTIVO
        {
            get { return bACTIVO; }
            set { bACTIVO = value; }
        }

        [DataMember]
        public string RowIndex { get; set; }
        [DataMember]
        public bool Actualizado { get; set; }
    }
}
