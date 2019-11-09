using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using ThreeBits.Entities.Common;
using ThreeBits.Entities.Security;
using ThreeBits.Entities.User;

namespace ThreeBits.WCFService
{
    // NOTA: puede usar el comando 'Rename' del menú 'Refactorizar' para cambiar el nombre de interfaz 'IUserSecurityService' en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public interface IUserSecurityService
    {
        [OperationContract]
        UsuarioDC addUsuario(ReglasBE Reglas, UsuariosBE Usuario, List<DomicilioBE> Domicilios, List<ContactoBE> Contactos, List<RolesXUsuarioBE> RolesXUsuario, Int64 App, string PasswordApp);

        [OperationContract]
        UsuarioDC addUsuarioXAplicacion(ReglasBE Reglas, List<UsuarioXAppBE> lstUsuarioXApp, Int64 App, string PasswordApp);

        [OperationContract]
        UsuarioDC actDeactivateUsuario(ReglasBE Reglas, Int64 App, string PasswordApp);

        [OperationContract]
        UsuarioDC updateUsuario(ReglasBE Reglas, UsuariosBE Usuario, List<DomicilioBE> Domicilios, List<ContactoBE> Contactos, List<RolesXUsuarioBE> RolesXUsuario, Int64 App, string PasswordApp);

        [OperationContract]
        UsuarioDC addRolesXUsuario(ReglasBE Reglas, List<RolesXUsuarioBE> RolesXUsuario, Int64 App, string PasswordApp);

        [OperationContract]
        UsuarioDC updateRol(ReglasBE Reglas, RolesXUsuarioBE RolXUsuario, Int64 App, string PasswordApp);

        [OperationContract]
        UsuarioDC getUsuarioFull(ReglasBE Reglas, Int64 App, string PasswordApp);
        [OperationContract]
        UsuarioDC GetUsuarios(UsuariosBE item, Int64 App, string PasswordApp);
        [OperationContract]
        UsuarioDC GetUsuario(UsuariosBE item, Int64 App, string PasswordApp);

        [OperationContract]
        UsuarioDC GetRolesVsUser(ReglasBE Reglas, Int64 App, string PasswordApp);

        [OperationContract]
        UsuarioDC getRolesXApp(ReglasBE Reglas, Int64 App, string PasswordApp);

        [OperationContract]
        UsuarioDC checkUsrXApp(ReglasBE Reglas, Int64 App, string PasswordApp);

        [OperationContract]
        UsuarioDC getAppXUsuario(ReglasBE Reglas, Int64 App, string PasswordApp);

        [OperationContract]
        UsuarioDC getEstacionesXApp(ReglasBE Reglas, Int64 App, string PasswordApp);

        [OperationContract]
        UsuarioDC getRelTipoUsuario(ReglasBE Reglas, Int64 App, string PasswordApp);

        [OperationContract]
        UsuarioDC getCatSelection(int IdCatGeneral, int IdSubCatalogo, Int64 App, string PasswordApp);




    }

    [DataContract]
    public class UsuarioDC
    {
        [DataMember]
        public RespuestaGralBE ResGral = new RespuestaGralBE();

        [DataMember]
        public DatosUsuarioBE DatosUsuario = new DatosUsuarioBE();

        [DataMember]
        public List<AplicacionBE> ListaApps = new List<AplicacionBE>();

        [DataMember]
        public List<EstacionesXAppBE> ListaEstaciones = new List<EstacionesXAppBE>();

        [DataMember]
        public List<RolesBE> ListaRolesXApp = new List<RolesBE>();

        [DataMember]
        public List<CatalogosBE> ListaCatalogos = new List<CatalogosBE>();

        [DataMember]
        public List<RelacionTipoUsuarioBE> ListaRelTipoUsuario = new List<RelacionTipoUsuarioBE>();

        [DataMember]
        public List<UsuarioXAppBE> lstUsuarioXApp = new List<UsuarioXAppBE>();

        [DataMember]
        public List<CuentaBE> lstCuenta = new List<CuentaBE>();

    }

}
