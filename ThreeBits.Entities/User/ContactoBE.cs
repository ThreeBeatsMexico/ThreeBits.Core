using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ThreeBits.Entities.User
{
    [DataContract]
    public class ContactoBE
    {
        //Propiedades de Domicilio:
        private Int64 iIDCONTACTO;
        [DataMember]
        public Int64 IDCONTACTO
        {
            get { return iIDCONTACTO; }
            set { iIDCONTACTO = value; }
        }

        private Int64 iIDUSUARIO;
        [DataMember]
        public Int64 IDUSUARIO
        {
            get { return iIDUSUARIO; }
            set { iIDUSUARIO = value; }
        }

        private Int32 iIDTIPOCONTACTO;
        [DataMember]
        public Int32 IDTIPOCONTACTO
        {
            get { return iIDTIPOCONTACTO; }
            set { iIDTIPOCONTACTO = value; }
        }

        private string sTIPOCONTACTO;
        [DataMember]
        public string TIPOCONTACTO
        {
            get { return sTIPOCONTACTO; }
            set { sTIPOCONTACTO = value; }
        }


        private String sVALOR;
        [DataMember]
        public String VALOR
        {
            get { return sVALOR; }
            set { sVALOR = value; }
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


        [DataMember]
        public string RowIndex { get; set; }
        [DataMember]
        public bool Actualizado { get; set; }

    }
}
