using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using ThreeBits.Entities.Common;
using ThreeBits.Entities.Security;

namespace ThreeBits.WCFService
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "ISecurityService" en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public interface ISecurityService
    {
        [OperationContract]
        SecutityDC checkPermisoXMethServ(string MethodName, string ServiceName, Int64 App, string PasswordApp);

        [OperationContract]
        SecutityDC checkMetodoXApp(Int64 IdApp, string sServiceName, string sMethodName, Int64 App, string PasswordApp);

        [OperationContract]
        SecutityDC getObjetosXAppRolPage(long Rol, string Pagina, Int64 App, string PasswordApp);

        [OperationContract]
        SecutityDC getElementsObjectsXIdObj(long IdPermisosXObj, Int64 App, string PasswordApp);

        [OperationContract]
        SecutityDC getMenuXAppRol(long Rol, Int64 App, string PasswordApp);

        [OperationContract]
        SecutityDC getMenuXAppRolAdmin(long Rol, Int64 App, string PasswordApp);

        [OperationContract]
        SecutityDC getMenuxRol(long Rol, Int64 AppRol, Int64 App, string PasswordApp);

        [OperationContract]
        SecutityDC getMenuxRolAdmin(long Rol, Int64 AppRol, Int64 App, string PasswordApp);

        [OperationContract]
        SecutityDC getSubMenuXIdMenu(long IdPermisoMenu, Int64 App, string PasswordApp);

        [OperationContract]
        SecutityDC getSubMenuXIdMenuAdmin(long IdPermisoMenu, Int64 App, string PasswordApp);

        [OperationContract]
        SecutityDC encryptDesEncrypt(string Valor, int Tipo, Int64 App, string PasswordApp);

        [OperationContract]
        SecutityDC getAplicaciones(string idAplicacion, string txtBusqueda, Int64 App, string PasswordApp);

        [OperationContract]
        SecutityDC addAplicacion(AplicacionBE Aplicacion, Int64 App, string PasswordApp);
        [OperationContract]
        SecutityDC delMenu(Int64 idMenu, Int64 App, string PasswordApp);
        [OperationContract]
        SecutityDC delSubMenu(Int64 idSubMenu, Int64 App, string PasswordApp);

        [OperationContract]
        SecutityDC updAplicacion(AplicacionBE Aplicacion, Int64 App, string PasswordApp);
        [OperationContract]
        SecutityDC updMenuxAppRol(PermisosXMenuBE Menu, Int64 App, string PasswordApp);
        [OperationContract]
        SecutityDC updSubMenuxAppRol(PermisoXSubmenuBE SubMenu, Int64 App, string PasswordApp);

        [OperationContract]
        SecutityDC addMetodo(List<WCFMetodosBE> Metodos, Int64 App, string PasswordApp);

        [OperationContract]
        SecutityDC addRolxApp(string Rol, Int64 AppRol, Int64 App, string PasswordApp);

        [OperationContract]
        SecutityDC addServicio(string Servicio, Int64 IdApp, bool Recurrente, Int64 App, string PasswordApp);

        [OperationContract]
        SecutityDC addMenuxAppRol(Int64 Rol, Int64 IdApp, string Menu, string Img, string Obj, string Url, string Tool, Int64 Orden, Int64 App, string PasswordApp);

        [OperationContract]
        SecutityDC addSubMenuxAppRol(Int64 IdSubMenu, string SubMenu, string Img, string Obj, string Url, string Tool, Int64 Orden, Int64 App, string PasswordApp);

        [OperationContract]
        SecutityDC addPermisosxObjeto(Int64 IdRol, string Pagina, string Obj, string TipoObj, string Tool, Int64 App, string PasswordApp);

        [OperationContract]
        SecutityDC addPermisosxElementoObjeto(Int64 IdPermiosObj, string Elemento, string Tool, Int64 App, string PasswordApp);
    }

    [DataContract]
    public class SecutityDC
    {
        [DataMember]
        public RespuestaGralBE ResGral = new RespuestaGralBE();

        [DataMember]
        public List<PermisosXObjetosBE> PermisoXObjetos = new List<PermisosXObjetosBE>();

        [DataMember]
        public List<PermisoXElementosObjBE> ElementosObjetos = new List<PermisoXElementosObjBE>();

        [DataMember]
        public List<PermisosXMenuBE> PermisosXMenu = new List<PermisosXMenuBE>();

        [DataMember]
        public List<PermisoXSubmenuBE> PermisosXSubmenu = new List<PermisoXSubmenuBE>();

        [DataMember]
        public EncryptionBE Encriptacion = new EncryptionBE();

        [DataMember]
        public List<AplicacionBE> Aplicaciones = new List<AplicacionBE>();

        [DataMember]
        public List<WCFMetodosBE> Metodos = new List<WCFMetodosBE>();

    }
}
