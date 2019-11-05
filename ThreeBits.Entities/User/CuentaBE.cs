using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ThreeBits.Entities.User
{
    [DataContract]
    public class CuentaBE
    {
        [DataMember]
        public string IdCuenta { get; set; }
        [DataMember]
        public string IdAplicacion { get; set; }
        [DataMember]
        public string APaterno { get; set; }
        [DataMember]
        public string AMaterno { get; set; }
        [DataMember]
        public string Nombres { get; set; }
        [DataMember]
        public string RFCSiglas { get; set; }
        [DataMember]
        public string RFCFecha { get; set; }
        [DataMember]
        public string RFCHomo { get; set; }
        [DataMember]
        public string Activo { get; set; }
    }
}
