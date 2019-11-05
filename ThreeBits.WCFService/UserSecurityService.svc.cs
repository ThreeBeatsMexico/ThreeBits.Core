using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using ThreeBits.Business.Security;
using ThreeBits.Business.User;
using ThreeBits.Entities.Common;
using ThreeBits.Entities.User;

namespace ThreeBits.WCFService
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "UserSecurityService" en el código, en svc y en el archivo de configuración a la vez.
    // NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione UserSecurityService.svc o UserSecurityService.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class UserSecurityService : IUserSecurityService
    {
        private SecurityBR Seguridad = new SecurityBR();
        private string ServiceName = MethodBase.GetCurrentMethod().DeclaringType.Name;

        /// <summary>
        /// Inserta al usuario.
        /// </summary>
        /// <param name="Reglas">Regalas para la obtencion de datos</param>
        /// <param name="Usuario">Datos del usuario</param>
        /// <param name="Domicilios">Datos de los domicilio</param>
        /// <param name="Contactos">Datos de los contactos</param>
        /// <param name="sApp">Id de la aplicacion</param>
        /// <param name="sPasswordApp">Password de la aplicacion</param>
        /// <returns></returns>
        public UsuarioDC addUsuario(ReglasBE Reglas, UsuariosBE Usuario, List<DomicilioBE> Domicilios, List<ContactoBE> Contactos, List<RolesXUsuarioBE> RolesXUsuario, Int64 App, string PasswordApp)
        {
            UsuarioDC Respuesta = new UsuarioDC();
            Respuesta.ResGral.FLAG = true;
            try
            {
                ////Checamos si tiene acceso al servicio y al metodo, en caso contrario mandamos un error controlado
                Seguridad.checkPermisoMethServ(App, PasswordApp, MethodBase.GetCurrentMethod().Name, ServiceName);
                UsersBR UsuariosBR = new UsersBR();
                Respuesta.DatosUsuario.Usuario = UsuariosBR.addUsuario(Reglas, Usuario, Domicilios, Contactos, RolesXUsuario, App);
            }
            catch (Exception ex)
            {
                Respuesta.ResGral.FLAG = false;
                Respuesta.ResGral.TRACE = ex.StackTrace;
                Respuesta.ResGral.ERRORMESSAGE = ex.Message;
            }
            return Respuesta;
        }

        public UsuarioDC addUsuarioXAplicacion(ReglasBE Reglas, List<UsuarioXAppBE> lstUsuarioXApp, Int64 App, string PasswordApp)
        {
            UsuarioDC Respuesta = new UsuarioDC();
            Respuesta.ResGral.FLAG = true;
            try
            {
                ////Checamos si tiene acceso al servicio y al metodo, en caso contrario mandamos un error controlado
                Seguridad.checkPermisoMethServ(App, PasswordApp, MethodBase.GetCurrentMethod().Name, ServiceName);
                UsersBR UsuariosBR = new UsersBR();
                Respuesta.ResGral.FLAG = UsuariosBR.addUsuarioXAplicacion(Reglas, lstUsuarioXApp, App);
            }
            catch (Exception ex)
            {
                Respuesta.ResGral.FLAG = false;
                Respuesta.ResGral.TRACE = ex.StackTrace;
                Respuesta.ResGral.ERRORMESSAGE = ex.Message;
            }
            return Respuesta;
        }

        /// <summary>
        /// Activa o desartiva al usuario, segun sea el caso.
        /// </summary>
        /// <param name="Reglas">Enviamos las reglas para la activacion o desactivacion del usuario</param>
        /// <param name="IdUsuario">El Id del usuario</param>
        /// <param name="App">Id de la aplicacion</param>
        /// <param name="PasswordApp">Password de la aplicacion</param>
        /// <returns></returns>
        public UsuarioDC actDeactivateUsuario(ReglasBE Reglas, Int64 App, string PasswordApp)
        {
            UsuarioDC Respuesta = new UsuarioDC();
            Respuesta.ResGral.FLAG = true;
            try
            {
                ////Checamos si tiene acceso al servicio y al metodo, en caso contrario mandamos un error controlado
                Seguridad.checkPermisoMethServ(App, PasswordApp, MethodBase.GetCurrentMethod().Name, ServiceName);
                UsersBR UsuariosBR = new UsersBR();
                Respuesta.ResGral.FLAG = UsuariosBR.actDeactivateUsuario(Reglas, App);
            }
            catch (Exception ex)
            {
                Respuesta.ResGral.FLAG = false;
                Respuesta.ResGral.TRACE = ex.StackTrace;
                Respuesta.ResGral.ERRORMESSAGE = ex.Message;
            }
            return Respuesta;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Reglas"></param>
        /// <param name="Usuario"></param>
        /// <param name="Domicilios"></param>
        /// <param name="Contactos"></param>
        /// <param name="App"></param>
        /// <param name="PasswordApp"></param>
        /// <returns></returns>
        public UsuarioDC updateUsuario(ReglasBE Reglas, UsuariosBE Usuario, List<DomicilioBE> Domicilios, List<ContactoBE> Contactos, List<RolesXUsuarioBE> RolesXUsuario, Int64 App, string PasswordApp)
        {
            UsuarioDC Respuesta = new UsuarioDC();
            Respuesta.ResGral.FLAG = true;
            try
            {
                ////Checamos si tiene acceso al servicio y al metodo, en caso contrario mandamos un error controlado
                Seguridad.checkPermisoMethServ(App, PasswordApp, MethodBase.GetCurrentMethod().Name, ServiceName);
                UsersBR UsuariosBR = new UsersBR();
                Respuesta.ResGral.FLAG = UsuariosBR.updateUsuario(Reglas, Usuario, Domicilios, Contactos, RolesXUsuario, App);
            }
            catch (Exception ex)
            {
                Respuesta.ResGral.FLAG = false;
                Respuesta.ResGral.TRACE = ex.StackTrace;
                Respuesta.ResGral.ERRORMESSAGE = ex.Message;
            }
            return Respuesta;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Reglas"></param>
        /// <param name="RolesXUsuario"></param>
        /// <param name="App"></param>
        /// <param name="PasswordApp"></param>
        /// <returns></returns>
        public UsuarioDC addRolesXUsuario(ReglasBE Reglas, List<RolesXUsuarioBE> RolesXUsuario, Int64 App, string PasswordApp)
        {
            UsuarioDC Respuesta = new UsuarioDC();
            Respuesta.ResGral.FLAG = true;
            try
            {
                ////Checamos si tiene acceso al servicio y al metodo, en caso contrario mandamos un error controlado
                Seguridad.checkPermisoMethServ(App, PasswordApp, MethodBase.GetCurrentMethod().Name, ServiceName);
                UsersBR UsuariosBR = new UsersBR();
                Respuesta.ResGral.FLAG = UsuariosBR.addRolesXUsuario(Reglas, RolesXUsuario, App);
            }
            catch (Exception ex)
            {
                Respuesta.ResGral.FLAG = false;
                Respuesta.ResGral.TRACE = ex.StackTrace;
                Respuesta.ResGral.ERRORMESSAGE = ex.Message;
            }
            return Respuesta;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Reglas"></param>
        /// <param name="RolesXUsuario"></param>
        /// <param name="App"></param>
        /// <param name="PasswordApp"></param>
        /// <returns></returns>
        public UsuarioDC updateRol(ReglasBE Reglas, RolesXUsuarioBE RolXUsuario, Int64 App, string PasswordApp)
        {
            UsuarioDC Respuesta = new UsuarioDC();
            Respuesta.ResGral.FLAG = true;
            try
            {
                ////Checamos si tiene acceso al servicio y al metodo, en caso contrario mandamos un error controlado
                Seguridad.checkPermisoMethServ(App, PasswordApp, MethodBase.GetCurrentMethod().Name, ServiceName);
                UsersBR UsuariosBR = new UsersBR();
                Respuesta.ResGral.FLAG = UsuariosBR.updateRol(Reglas, RolXUsuario, App);
            }
            catch (Exception ex)
            {
                Respuesta.ResGral.FLAG = false;
                Respuesta.ResGral.TRACE = ex.StackTrace;
                Respuesta.ResGral.ERRORMESSAGE = ex.Message;
            }
            return Respuesta;
        }

        /// <summary>
        /// Recupera la informacion del usuario por IdUsuarioApp o Idusuario general.
        /// </summary>
        /// <param name="Reglas">Regalas para la obtencion de datos</param>
        /// <param name="Usuario"> 1.-Busqueda por el identity. 2.- Por el usuario de la aplicacion. 3.- Por el usuario</param>
        /// <param name="Domicilios">Datos de los domicilio</param>
        /// <param name="Contactos">Datos de los contactos</param>
        /// <param name="sApp">Id de la aplicacion</param>
        /// <param name="sPasswordApp">Password de la aplicacion</param>
        /// <returns></returns>
        public UsuarioDC getUsuarioFull(ReglasBE Reglas, Int64 App, string PasswordApp)
        {
            UsuarioDC Respuesta = new UsuarioDC();
            Respuesta.ResGral.FLAG = true;
            try
            {
                ////Checamos si tiene acceso al servicio y al metodo, en caso contrario mandamos un error controlado
                Seguridad.checkPermisoMethServ(App, PasswordApp, MethodBase.GetCurrentMethod().Name, ServiceName);
                UsersBR UsuariosBR = new UsersBR();
                Respuesta.DatosUsuario = UsuariosBR.getUsuarioFull(Reglas, App);
            }
            catch (Exception ex)
            {
                Respuesta.ResGral.FLAG = false;
                Respuesta.ResGral.TRACE = ex.StackTrace;
                Respuesta.ResGral.ERRORMESSAGE = ex.Message;
            }
            return Respuesta;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item">Datos para realizar la consulta de de los usuarios</param>
        /// <param name="App">Id de la aplicación</param>
        /// <param name="PasswordApp">Clave de la aplicación</param>
        /// <returns></returns>
        public UsuarioDC GetUsuarios(UsuariosBE item, Int64 App, string PasswordApp)
        {
            UsuarioDC Respuesta = new UsuarioDC();
            Respuesta.ResGral.FLAG = true;
            try
            {
                ////Checamos si tiene acceso al servicio y al metodo, en caso contrario mandamos un error controlado
                Seguridad.checkPermisoMethServ(App, PasswordApp, MethodBase.GetCurrentMethod().Name, ServiceName);
                UsersBR UsuariosBR = new UsersBR();
                Respuesta.DatosUsuario.Usuarios = UsuariosBR.GetUsuarios(item, App);
            }
            catch (Exception ex)
            {
                Respuesta.ResGral.FLAG = false;
                Respuesta.ResGral.TRACE = ex.StackTrace;
                Respuesta.ResGral.ERRORMESSAGE = ex.Message;
            }
            return Respuesta;
        }

        public UsuarioDC GetRolesVsUser(ReglasBE Reglas, Int64 App, string PasswordApp)
        {
            UsuarioDC Respuesta = new UsuarioDC();
            Respuesta.ResGral.FLAG = true;
            try
            {
                ////Checamos si tiene acceso al servicio y al metodo, en caso contrario mandamos un error controlado
                Seguridad.checkPermisoMethServ(App, PasswordApp, MethodBase.GetCurrentMethod().Name, ServiceName);
                UsersBR UsuariosBR = new UsersBR();
                Respuesta.DatosUsuario.RolesVSUsuario = UsuariosBR.GetRolesVsUser(Reglas, App);
            }
            catch (Exception ex)
            {
                Respuesta.ResGral.FLAG = false;
                Respuesta.ResGral.TRACE = ex.StackTrace;
                Respuesta.ResGral.ERRORMESSAGE = ex.Message;
            }
            return Respuesta;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item">Datos para realizar la consulta de de los usuarios</param>
        /// <param name="App">Id de la aplicación</param>
        /// <param name="PasswordApp">Clave de la aplicación</param>
        /// <returns></returns>
        public UsuarioDC GetUsuario(UsuariosBE item, Int64 App, string PasswordApp)
        {
            UsuarioDC Respuesta = new UsuarioDC();
            Respuesta.ResGral.FLAG = true;
            try
            {
                ////Checamos si tiene acceso al servicio y al metodo, en caso contrario mandamos un error controlado
                Seguridad.checkPermisoMethServ(App, PasswordApp, MethodBase.GetCurrentMethod().Name, ServiceName);
                UsersBR UsuariosBR = new UsersBR();
                Respuesta.DatosUsuario.Usuarios = UsuariosBR.GetUsuario(item, App);
            }
            catch (Exception ex)
            {
                Respuesta.ResGral.FLAG = false;
                Respuesta.ResGral.TRACE = ex.StackTrace;
                Respuesta.ResGral.ERRORMESSAGE = ex.Message;
            }
            return Respuesta;
        }

        public UsuarioDC getRolesXApp(ReglasBE Reglas, Int64 App, string PasswordApp)
        {
            UsuarioDC Respuesta = new UsuarioDC();
            Respuesta.ResGral.FLAG = true;
            try
            {
                ////Checamos si tiene acceso al servicio y al metodo, en caso contrario mandamos un error controlado
                Seguridad.checkPermisoMethServ(App, PasswordApp, MethodBase.GetCurrentMethod().Name, ServiceName);
                UsersBR UsuariosBR = new UsersBR();
                Respuesta.ListaRolesXApp = UsuariosBR.getRolesXApp(Reglas, App);
            }
            catch (Exception ex)
            {
                Respuesta.ResGral.FLAG = false;
                Respuesta.ResGral.TRACE = ex.StackTrace;
                Respuesta.ResGral.ERRORMESSAGE = ex.Message;
            }
            return Respuesta;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Reglas"></param>
        /// <param name="App"></param>
        /// <param name="PasswordApp"></param>
        /// <returns></returns>
        public UsuarioDC checkUsrXApp(ReglasBE Reglas, Int64 App, string PasswordApp)
        {
            UsuarioDC Respuesta = new UsuarioDC();
            Respuesta.ResGral.FLAG = true;
            try
            {
                ////Checamos si tiene acceso al servicio y al metodo, en caso contrario mandamos un error controlado
                Seguridad.checkPermisoMethServ(App, PasswordApp, MethodBase.GetCurrentMethod().Name, ServiceName);
                UsersBR UsuariosBR = new UsersBR();
                Respuesta.ResGral.FLAG = UsuariosBR.checkUsrXApp(Reglas, App);
            }
            catch (Exception ex)
            {
                Respuesta.ResGral.FLAG = false;
                Respuesta.ResGral.TRACE = ex.StackTrace;
                Respuesta.ResGral.ERRORMESSAGE = ex.Message;
            }
            return Respuesta;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Reglas"></param>
        /// <param name="App"></param>
        /// <param name="PasswordApp"></param>
        /// <returns></returns>
        public UsuarioDC getAppXUsuario(ReglasBE Reglas, Int64 App, string PasswordApp)
        {
            UsuarioDC Respuesta = new UsuarioDC();
            Respuesta.ResGral.FLAG = true;
            try
            {
                ////Checamos si tiene acceso al servicio y al metodo, en caso contrario mandamos un error controlado
                Seguridad.checkPermisoMethServ(App, PasswordApp, MethodBase.GetCurrentMethod().Name, ServiceName);
                UsersBR UsuariosBR = new UsersBR();
                Respuesta.lstUsuarioXApp = UsuariosBR.getAppXUsuario(Reglas, App);
            }
            catch (Exception ex)
            {
                Respuesta.ResGral.FLAG = false;
                Respuesta.ResGral.TRACE = ex.StackTrace;
                Respuesta.ResGral.ERRORMESSAGE = ex.Message;
            }
            return Respuesta;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Reglas"></param>
        /// <param name="App"></param>
        /// <param name="PasswordApp"></param>
        /// <returns></returns>
        public UsuarioDC getEstacionesXApp(ReglasBE Reglas, Int64 App, string PasswordApp)
        {
            UsuarioDC Respuesta = new UsuarioDC();
            Respuesta.ResGral.FLAG = true;
            try
            {
                ////Checamos si tiene acceso al servicio y al metodo, en caso contrario mandamos un error controlado
                Seguridad.checkPermisoMethServ(App, PasswordApp, MethodBase.GetCurrentMethod().Name, ServiceName);
                UsersBR UsuariosBR = new UsersBR();
                Respuesta.ListaEstaciones = UsuariosBR.getEstacionesXApp(Reglas, App);
            }
            catch (Exception ex)
            {
                Respuesta.ResGral.FLAG = false;
                Respuesta.ResGral.TRACE = ex.StackTrace;
                Respuesta.ResGral.ERRORMESSAGE = ex.Message;
            }
            return Respuesta;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Reglas"></param>
        /// <param name="App"></param>
        /// <param name="PasswordApp"></param>
        /// <returns></returns>
        public UsuarioDC getRelTipoUsuario(ReglasBE Reglas, Int64 App, string PasswordApp)
        {
            UsuarioDC Respuesta = new UsuarioDC();
            Respuesta.ResGral.FLAG = true;
            try
            {
                ////Checamos si tiene acceso al servicio y al metodo, en caso contrario mandamos un error controlado
                Seguridad.checkPermisoMethServ(App, PasswordApp, MethodBase.GetCurrentMethod().Name, ServiceName);
                UsersBR UsuariosBR = new UsersBR();
                Respuesta.ListaRelTipoUsuario = UsuariosBR.getRelTipoUsuario(Reglas, App);
            }
            catch (Exception ex)
            {
                Respuesta.ResGral.FLAG = false;
                Respuesta.ResGral.TRACE = ex.StackTrace;
                Respuesta.ResGral.ERRORMESSAGE = ex.Message;
            }
            return Respuesta;
        }

        /// <summary>
        /// Metodo que explota los catalogos, solo hay que enviarle el ID del catalogo que se requiera.
        /// </summary>
        /// <param name="dIdCatGeneral">Recibe el Id del catalogo que requiere_Ver lista de catalogos</param>
        /// <param name="dIdSubCatalogo">Recibe la condicion de algun Id secundario</param>
        /// <param name="sApp"></param>
        /// <param name="sPasswordApp"></param>
        /// <returns></returns>
        public UsuarioDC getCatSelection(int IdCatGeneral, int IdSubCatalogo, Int64 App, string PasswordApp)
        {
            UsuarioDC Respuesta = new UsuarioDC();
            Respuesta.ResGral.FLAG = true;
            try
            {
                ////Checamos si tiene acceso al servicio y al metodo, en caso contrario mandamos un error controlado
                Seguridad.checkPermisoMethServ(App, PasswordApp, MethodBase.GetCurrentMethod().Name, ServiceName);
                UsersBR UsuariosBR = new UsersBR();
                Respuesta.ListaCatalogos = UsuariosBR.getCatSelection(IdCatGeneral, IdSubCatalogo, App);
            }
            catch (Exception ex)
            {
                Respuesta.ResGral.FLAG = false;
                Respuesta.ResGral.TRACE = ex.StackTrace;
                Respuesta.ResGral.ERRORMESSAGE = ex.Message;
            }
            return Respuesta;
        }
    }
}
