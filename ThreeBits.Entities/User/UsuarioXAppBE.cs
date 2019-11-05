using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ThreeBits.Entities.User
{
    [DataContract]
    public class UsuarioXAppBE
    {
        [DataMember]
        public string IDUSRSXAPP { get; set; }
        [DataMember]
        public string IDAPLICACION { get; set; }
        [DataMember]
        public string IDUSUARIO { get; set; }
        [DataMember]
        public string DESCRIPCION { get; set; }
        [DataMember]
        public string URLINICIO { get; set; }
        [DataMember]
        public string ACTIVO { get; set; }


    }
}
