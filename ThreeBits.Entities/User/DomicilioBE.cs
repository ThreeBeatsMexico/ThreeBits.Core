using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ThreeBits.Entities.User
{
    [DataContract]
    public class DomicilioBE
    {
        //Propiedades de Domicilio:
        private Int64 iIDDOMICILIO;
        [DataMember]
        public Int64 IDDOMICILIO
        {
            get { return iIDDOMICILIO; }
            set { iIDDOMICILIO = value; }
        }

        private Int64 iIDUSUARIO;
        [DataMember]
        public Int64 IDUSUARIO
        {
            get { return iIDUSUARIO; }
            set { iIDUSUARIO = value; }
        }

        private String sCALLE;
        [DataMember]
        public String CALLE
        {
            get { return sCALLE; }
            set { sCALLE = value; }
        }

        private String sNUMEXT;
        [DataMember]
        public String NUMEXT
        {
            get { return sNUMEXT; }
            set { sNUMEXT = value; }
        }

        private String sNUMINT;
        [DataMember]
        public String NUMINT
        {
            get { return sNUMINT; }
            set { sNUMINT = value; }
        }

        private string iDESTADO;
        [DataMember]
        public string IDESTADO
        {
            get { return iDESTADO; }
            set { iDESTADO = value; }
        }

        private String sESTADO;
        [DataMember]
        public String ESTADO
        {
            get { return sESTADO; }
            set { sESTADO = value; }
        }

        private string iIDMUNICIPIO;
        [DataMember]
        public string IDMUNICIPIO
        {
            get { return iIDMUNICIPIO; }
            set { iIDMUNICIPIO = value; }
        }

        private string sMUNICIPIO;
        [DataMember]
        public string MUNICIPIO
        {
            get { return sMUNICIPIO; }
            set { sMUNICIPIO = value; }
        }

        private string iIDCOLONIA;
        [DataMember]
        public string IDCOLONIA
        {
            get { return iIDCOLONIA; }
            set { iIDCOLONIA = value; }
        }

        private String sCOLONIA;
        [DataMember]
        public String COLONIA
        {
            get { return sCOLONIA; }
            set { sCOLONIA = value; }
        }

        private String sCP;
        [DataMember]
        public String CP
        {
            get { return sCP; }
            set { sCP = value; }
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
