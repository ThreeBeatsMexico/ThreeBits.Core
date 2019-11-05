using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ThreeBits.Entities.User
{
    [DataContract]
    public class DatosUsuarioBE
    {
        [DataMember]
        public UsuariosBE Usuario = new UsuariosBE();
        [DataMember]
        public List<DomicilioBE> Domicilios = new List<DomicilioBE>();
        [DataMember]
        public List<ContactoBE> Contactos = new List<ContactoBE>();
        [DataMember]
        public List<RolesXUsuarioBE> RolesXUsuario = new List<RolesXUsuarioBE>();
        [DataMember]
        public List<RolesXUsuarioBE> RolesVSUsuario = new List<RolesXUsuarioBE>();
        [DataMember]
        public List<UsuariosBE> Usuarios = new List<UsuariosBE>();
    }
}
