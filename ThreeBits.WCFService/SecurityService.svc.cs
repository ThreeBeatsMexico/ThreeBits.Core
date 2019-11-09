using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using ThreeBits.Business.Security;
using ThreeBits.Entities.Security;

namespace ThreeBits.WCFService
{
    // NOTA: puede usar el comando 'Rename' del menú 'Refactorizar' para cambiar el nombre de clase 'SecurityService' en el código, en svc y en el archivo de configuración a la vez.
    // NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione SecurityService.svc o SecurityService.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class SecurityService : ISecurityService
    {
        private string ServiceNameClass = MethodBase.GetCurrentMethod().DeclaringType.Name;

        /// <summary>
        /// Verifica que quien ejecute algun metodo de cierto servicio tenga permiso para poder acceder al WCF
        /// </summary>
        /// <param name='MethodName'>Recibe el metodo que se requiere ejecutar</param>
        /// <param name='ServiceName'>Recibe el servicio</param>
        /// <param name='App'>Recibe el nombre de la aplicacion</param>
        /// <param name='PasswordApp'>Recibe el password de la aplicacion</param>
        /// <returns></returns>
        public SecutityDC checkPermisoXMethServ(string MethodName, string ServiceName, Int64 App, string PasswordApp)
        {
            SecutityDC Respuesta = new SecutityDC();
            Respuesta.ResGral.FLAG = true;
            try
            {
                ////Checamos si tiene acceso al servicio y al metodo, en caso contrario mandamos un error controlado
                SecurityBR Seguridad = new SecurityBR();
                Seguridad.checkPermisoMethServ(App, PasswordApp, MethodName, ServiceName);
            }
            catch (Exception ex)
            {
                Respuesta.ResGral.FLAG = false;
                Respuesta.ResGral.TRACE = ex.StackTrace;
                Respuesta.ResGral.ERRORMESSAGE = ex.Message;
            }
            return Respuesta;
        }

        public SecutityDC checkMetodoXApp(Int64 IdApp, string sServiceName, string sMethodName, Int64 App, string PasswordApp)
        {
            SecutityDC Respuesta = new SecutityDC();
            Respuesta.ResGral.FLAG = false;
            try
            {
                ////Checamos si tiene acceso al servicio y al metodo, en caso contrario mandamos un error controlado

                SecurityBR Seguridad = new SecurityBR();
                Seguridad.checkPermisoMethServ(App, PasswordApp, MethodBase.GetCurrentMethod().Name, ServiceNameClass);
                Respuesta.Metodos = Seguridad.checkMetodoXApp(IdApp, sServiceName, sMethodName);
                Respuesta.ResGral.FLAG = true;
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
        /// Obtenemos de BD de los objetos por App, Rol y pagina.
        /// </summary>
        /// <param name='Rol'>Recibe el rol del usuario</param>
        /// <param name='Pagina'>Recibe la pagina</param>
        /// <param name='App'>Recibe el id de la aplicacion</param>
        /// <param name='PasswordApp'>Recibe el password de la aplicacion</param>
        /// <returns>Regresa la informacion en la entidad PermisoXObjetos</returns>
        public SecutityDC getObjetosXAppRolPage(long Rol, string Pagina, Int64 App, string PasswordApp)
        {
            SecutityDC Respuesta = new SecutityDC();
            Respuesta.ResGral.FLAG = true;
            try
            {
                //Checamos si tiene acceso al servicio y al metodo, en caso contrario mandamos un error controlado
                SecurityBR Seguridad = new SecurityBR();
                Seguridad.checkPermisoMethServ(App, PasswordApp, MethodBase.GetCurrentMethod().Name, ServiceNameClass);
                Respuesta.PermisoXObjetos = Seguridad.getObjetosXAppRolPage(Rol, Pagina, App);
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
        /// Obtenemos de BD los elementos de un objeto.
        /// </summary>
        /// <param name='IdPermisosXObj'>Recibe el Id del Objeto</param>
        /// <param name='App'>Recibe el id de la aplicacion</param>
        /// <param name='PasswordApp'>Recibe el password de la aplicacion</param>
        /// <returns>Regresa la informacion en la entidad ElementosObjetos</returns>
        public SecutityDC getElementsObjectsXIdObj(long IdPermisosXObj, Int64 App, string PasswordApp)
        {
            SecutityDC Respuesta = new SecutityDC();
            Respuesta.ResGral.FLAG = true;
            try
            {
                //Checamos si tiene acceso al servicio y al metodo, en caso contrario mandamos un error controlado
                SecurityBR Seguridad = new SecurityBR();
                Seguridad.checkPermisoMethServ(App, PasswordApp, MethodBase.GetCurrentMethod().Name, ServiceNameClass);
                Respuesta.ElementosObjetos = Seguridad.getElementsObjectsXIdObj(IdPermisosXObj, App);
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
        /// Obtenemos la lista de menus por perfil que le corresponden
        /// </summary>
        /// <param name='Perfil'>Recibe el perfil del usuario</param>
        /// <param name='sApp'>Recibe el nombre de la aplicacion</param>
        /// <param name='sPasswordApp'>Recibe el password de la aplicacion</param>
        /// <returns>Regresa la informacion en la entidad PermisosXMenu</returns>
        public SecutityDC getMenuXAppRol(long Rol, Int64 App, string PasswordApp)
        {
            SecutityDC Respuesta = new SecutityDC();
            Respuesta.ResGral.FLAG = true;
            try
            {
                //Checamos si tiene acceso al servicio y al metodo, en caso contrario mandamos un error controlado
                SecurityBR Seguridad = new SecurityBR();
                Seguridad.checkPermisoMethServ(App, PasswordApp, MethodBase.GetCurrentMethod().Name, ServiceNameClass);
                Respuesta.PermisosXMenu = Seguridad.getMenuXAppRol(Rol, App);
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
        /// Obtiene la lista de submenus por menu y perfil que le corresponden
        /// </summary>
        /// <param name='IdPermisoMenu'>Recibe el id del menu</param>
        /// <param name='Idperfil'>Recibe el perfil del usuario</param>
        /// <param name='sApp'>Recibe el nombre de la aplicacion</param>
        /// <param name='sPasswordApp'>Recibe el password de la aplicacion</param>
        /// <returns>Regresa la informacion en la entidad PermisosXSubmenu</returns>   
        public SecutityDC getSubMenuXIdMenu(long IdPermisoMenu, Int64 App, string PasswordApp)
        {
            SecutityDC Respuesta = new SecutityDC();
            Respuesta.ResGral.FLAG = true;
            try
            {
                //Checamos si tiene acceso al servicio y al metodo, en caso contrario mandamos un error controlado
                SecurityBR Seguridad = new SecurityBR();
                Seguridad.checkPermisoMethServ(App, PasswordApp, MethodBase.GetCurrentMethod().Name, ServiceNameClass);
                Respuesta.PermisosXSubmenu = Seguridad.getSubMenuXIdMenu(IdPermisoMenu, App);
            }
            catch (Exception ex)
            {
                Respuesta.ResGral.FLAG = false;
                Respuesta.ResGral.TRACE = ex.StackTrace;
                Respuesta.ResGral.ERRORMESSAGE = ex.Message;
            }
            return Respuesta;
        }

        public SecutityDC getMenuXAppRolAdmin(long Rol, Int64 App, string PasswordApp)
        {
            SecutityDC Respuesta = new SecutityDC();
            Respuesta.ResGral.FLAG = true;
            try
            {
                //Checamos si tiene acceso al servicio y al metodo, en caso contrario mandamos un error controlado
                SecurityBR Seguridad = new SecurityBR();
                Seguridad.checkPermisoMethServ(App, PasswordApp, MethodBase.GetCurrentMethod().Name, ServiceNameClass);
                Respuesta.PermisosXMenu = Seguridad.getMenuXAppRolAdmin(Rol, App);
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
        /// Obtiene la lista de submenus por menu y perfil que le corresponden
        /// </summary>
        /// <param name='IdPermisoMenu'>Recibe el id del menu</param>
        /// <param name='Idperfil'>Recibe el perfil del usuario</param>
        /// <param name='sApp'>Recibe el nombre de la aplicacion</param>
        /// <param name='sPasswordApp'>Recibe el password de la aplicacion</param>
        /// <returns>Regresa la informacion en la entidad PermisosXSubmenu</returns>   
        public SecutityDC getSubMenuXIdMenuAdmin(long IdPermisoMenu, Int64 App, string PasswordApp)
        {
            SecutityDC Respuesta = new SecutityDC();
            Respuesta.ResGral.FLAG = true;
            try
            {
                //Checamos si tiene acceso al servicio y al metodo, en caso contrario mandamos un error controlado
                SecurityBR Seguridad = new SecurityBR();
                Seguridad.checkPermisoMethServ(App, PasswordApp, MethodBase.GetCurrentMethod().Name, ServiceNameClass);
                Respuesta.PermisosXSubmenu = Seguridad.getSubMenuXIdMenuAdmin(IdPermisoMenu, App);
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
        /// Encripta o desencripta las cadenas enviadas.
        /// </summary>
        /// <param name='sValor'>Recibe el valor a encriptar o desencriptar</param>
        /// <param name='Tipo'>1.- Encripta; 2.- Desencripta</param>
        /// <param name='PasswordApp'>Recibe el password de la aplicacion</param>
        /// <returns>Regresa el valor encriptado o desencriptado segun sea el caso en el elemento Encriptacion</returns>
        public SecutityDC encryptDesEncrypt(string Valor, int Tipo, Int64 App, string PasswordApp)
        {
            SecutityDC Respuesta = new SecutityDC();
            Respuesta.ResGral.FLAG = true;
            try
            {
                //Checamos si tiene acceso al servicio y al metodo, en caso contrario mandamos un error controlado
                SecurityBR Seguridad = new SecurityBR();
                Seguridad.checkPermisoMethServ(App, PasswordApp, MethodBase.GetCurrentMethod().Name, ServiceNameClass);
                Respuesta.Encriptacion = Seguridad.encryptDesEncrypt(Tipo, Valor, App);
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
        /// Obtiene la lista de Aplicaciones de toda la seguridad Latino
        /// </summary>
        /// <param name='sApp'>Recibe el nombre de la aplicacion</param>
        /// <param name='sPasswordApp'>Recibe el password de la aplicacion</param>
        /// <returns>Regresa la informacion en la entidad AplicacionBE</returns>   
        public SecutityDC getAplicaciones(string idAplicacion, string txtBusqueda, Int64 App, string PasswordApp)
        {
            SecutityDC Respuesta = new SecutityDC();
            Respuesta.ResGral.FLAG = true;
            try
            {
                //Checamos si tiene acceso al servicio y al metodo, en caso contrario mandamos un error controlado
                SecurityBR Seguridad = new SecurityBR();
                Seguridad.checkPermisoMethServ(App, PasswordApp, MethodBase.GetCurrentMethod().Name, ServiceNameClass);
                Respuesta.Aplicaciones = Seguridad.getAplicaciones(idAplicacion, txtBusqueda, App);
            }
            catch (Exception ex)
            {
                Respuesta.ResGral.FLAG = false;
                Respuesta.ResGral.TRACE = ex.StackTrace;
                Respuesta.ResGral.ERRORMESSAGE = ex.Message;
            }
            return Respuesta;
        }

        public SecutityDC addAplicacion(AplicacionBE Aplicacion, Int64 App, string PasswordApp)
        {
            SecutityDC Respuesta = new SecutityDC();
            Respuesta.ResGral.FLAG = true;
            try
            {
                ////Checamos si tiene acceso al servicio y al metodo, en caso contrario mandamos un error controlado
                SecurityBR Seguridad = new SecurityBR();
                Seguridad.checkPermisoMethServ(App, PasswordApp, MethodBase.GetCurrentMethod().Name, ServiceNameClass);
                Respuesta.ResGral.FLAG = Seguridad.addAplicacion(Aplicacion, App);
            }
            catch (Exception ex)
            {
                Respuesta.ResGral.FLAG = false;
                Respuesta.ResGral.TRACE = ex.StackTrace;
                Respuesta.ResGral.ERRORMESSAGE = ex.Message;
            }
            return Respuesta;
        }
        public SecutityDC updAplicacion(AplicacionBE Aplicacion, Int64 App, string PasswordApp)
        {
            SecutityDC Respuesta = new SecutityDC();
            Respuesta.ResGral.FLAG = true;
            try
            {
                ////Checamos si tiene acceso al servicio y al metodo, en caso contrario mandamos un error controlado
                SecurityBR Seguridad = new SecurityBR();
                Seguridad.checkPermisoMethServ(App, PasswordApp, MethodBase.GetCurrentMethod().Name, ServiceNameClass);
                Respuesta.ResGral.FLAG = Seguridad.updAplicacion(Aplicacion, App);
            }
            catch (Exception ex)
            {
                Respuesta.ResGral.FLAG = false;
                Respuesta.ResGral.TRACE = ex.StackTrace;
                Respuesta.ResGral.ERRORMESSAGE = ex.Message;
            }
            return Respuesta;
        }

        public SecutityDC delMenu(Int64 idMenu, Int64 App, string PasswordApp)
        {
            SecutityDC Respuesta = new SecutityDC();
            Respuesta.ResGral.FLAG = true;
            try
            {
                ////Checamos si tiene acceso al servicio y al metodo, en caso contrario mandamos un error controlado
                SecurityBR Seguridad = new SecurityBR();
                Seguridad.checkPermisoMethServ(App, PasswordApp, MethodBase.GetCurrentMethod().Name, ServiceNameClass);
                Respuesta.ResGral.FLAG = Seguridad.delMenu(idMenu, App);
            }
            catch (Exception ex)
            {
                Respuesta.ResGral.FLAG = false;
                Respuesta.ResGral.TRACE = ex.StackTrace;
                Respuesta.ResGral.ERRORMESSAGE = ex.Message;
            }
            return Respuesta;
        }
        public SecutityDC delSubMenu(Int64 idMenu, Int64 App, string PasswordApp)
        {
            SecutityDC Respuesta = new SecutityDC();
            Respuesta.ResGral.FLAG = true;
            try
            {
                ////Checamos si tiene acceso al servicio y al metodo, en caso contrario mandamos un error controlado
                SecurityBR Seguridad = new SecurityBR();
                Seguridad.checkPermisoMethServ(App, PasswordApp, MethodBase.GetCurrentMethod().Name, ServiceNameClass);
                Respuesta.ResGral.FLAG = Seguridad.delSubMenu(idMenu, App);
            }
            catch (Exception ex)
            {
                Respuesta.ResGral.FLAG = false;
                Respuesta.ResGral.TRACE = ex.StackTrace;
                Respuesta.ResGral.ERRORMESSAGE = ex.Message;
            }
            return Respuesta;
        }

        public SecutityDC updMenuxAppRol(PermisosXMenuBE MenuItem, Int64 App, string PasswordApp)
        {
            SecutityDC Respuesta = new SecutityDC();
            Respuesta.ResGral.FLAG = true;
            try
            {
                ////Checamos si tiene acceso al servicio y al metodo, en caso contrario mandamos un error controlado
                SecurityBR Seguridad = new SecurityBR();
                Seguridad.checkPermisoMethServ(App, PasswordApp, MethodBase.GetCurrentMethod().Name, ServiceNameClass);
                Respuesta.ResGral.FLAG = Seguridad.updMenuxAppRol(MenuItem.IDPERMISOSMENU, MenuItem.NOMBREMENU, MenuItem.IMAGEN, MenuItem.TIPOOBJETO, MenuItem.URL, MenuItem.TOOLTIP, MenuItem.ORDENMENU, MenuItem.ACTIVO, Convert.ToString(App));
            }
            catch (Exception ex)
            {
                Respuesta.ResGral.FLAG = false;
                Respuesta.ResGral.TRACE = ex.StackTrace;
                Respuesta.ResGral.ERRORMESSAGE = ex.Message;
            }
            return Respuesta;
        }

        public SecutityDC updSubMenuxAppRol(PermisoXSubmenuBE SubMenuItem, Int64 App, string PasswordApp)
        {
            SecutityDC Respuesta = new SecutityDC();
            Respuesta.ResGral.FLAG = true;
            try
            {
                ////Checamos si tiene acceso al servicio y al metodo, en caso contrario mandamos un error controlado
                SecurityBR Seguridad = new SecurityBR();
                Seguridad.checkPermisoMethServ(App, PasswordApp, MethodBase.GetCurrentMethod().Name, ServiceNameClass);
                Respuesta.ResGral.FLAG = Seguridad.updSubMenuxAppRol(SubMenuItem.IDPERMISOSMENU, SubMenuItem.IDPERMISOSXSUBMENU, SubMenuItem.NOMBRESUBMENU, SubMenuItem.IMAGEN, SubMenuItem.TIPOOBJETO, SubMenuItem.URL, SubMenuItem.TOOLTIP, SubMenuItem.ORDENSUBMENU, SubMenuItem.ACTIVO, Convert.ToString(App));
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
        /// Obtenemos la lista de menus por perfil que le corresponden
        /// </summary>
        /// <param name='Perfil'>Recibe el perfil del usuario</param>
        /// <param name='sApp'>Recibe el nombre de la aplicacion</param>
        /// <param name='sPasswordApp'>Recibe el password de la aplicacion</param>
        /// <returns>Regresa la informacion en la entidad PermisosXMenu</returns>
        public SecutityDC getMenuxRol(long Rol, Int64 AppxRol, Int64 App, string PasswordApp)
        {
            SecutityDC Respuesta = new SecutityDC();
            Respuesta.ResGral.FLAG = true;
            try
            {
                //Checamos si tiene acceso al servicio y al metodo, en caso contrario mandamos un error controlado
                SecurityBR Seguridad = new SecurityBR();
                Seguridad.checkPermisoMethServ(App, PasswordApp, MethodBase.GetCurrentMethod().Name, ServiceNameClass);
                Respuesta.PermisosXMenu = Seguridad.getMenuXAppRol(Rol, AppxRol);
            }
            catch (Exception ex)
            {
                Respuesta.ResGral.FLAG = false;
                Respuesta.ResGral.TRACE = ex.StackTrace;
                Respuesta.ResGral.ERRORMESSAGE = ex.Message;
            }
            return Respuesta;
        }

        public SecutityDC getMenuxRolAdmin(long Rol, Int64 AppxRol, Int64 App, string PasswordApp)
        {
            SecutityDC Respuesta = new SecutityDC();
            Respuesta.ResGral.FLAG = true;
            try
            {
                //Checamos si tiene acceso al servicio y al metodo, en caso contrario mandamos un error controlado
                SecurityBR Seguridad = new SecurityBR();
                Seguridad.checkPermisoMethServ(App, PasswordApp, MethodBase.GetCurrentMethod().Name, ServiceNameClass);
                Respuesta.PermisosXMenu = Seguridad.getMenuXAppRolAdmin(Rol, AppxRol);
            }
            catch (Exception ex)
            {
                Respuesta.ResGral.FLAG = false;
                Respuesta.ResGral.TRACE = ex.StackTrace;
                Respuesta.ResGral.ERRORMESSAGE = ex.Message;
            }
            return Respuesta;
        }


        public SecutityDC addRolxApp(string Rol, Int64 AppRol, Int64 App, string PasswordApp)
        {
            SecutityDC Respuesta = new SecutityDC();
            Respuesta.ResGral.FLAG = true;
            try
            {
                ////Checamos si tiene acceso al servicio y al metodo, en caso contrario mandamos un error controlado
                SecurityBR Seguridad = new SecurityBR();
                Seguridad.checkPermisoMethServ(App, PasswordApp, MethodBase.GetCurrentMethod().Name, ServiceNameClass);
                Respuesta.ResGral.FLAG = Seguridad.addRolxApp(Rol, AppRol);
            }
            catch (Exception ex)
            {
                Respuesta.ResGral.FLAG = false;
                Respuesta.ResGral.TRACE = ex.StackTrace;
                Respuesta.ResGral.ERRORMESSAGE = ex.Message;
            }
            return Respuesta;
        }


        public SecutityDC addMetodo(List<WCFMetodosBE> Metodos, Int64 App, string PasswordApp)
        {
            SecutityDC Respuesta = new SecutityDC();
            Respuesta.ResGral.FLAG = true;
            try
            {
                ////Checamos si tiene acceso al servicio y al metodo, en caso contrario mandamos un error controlado
                SecurityBR Seguridad = new SecurityBR();
                Seguridad.checkPermisoMethServ(App, PasswordApp, MethodBase.GetCurrentMethod().Name, ServiceNameClass);
                Respuesta.ResGral.FLAG = Seguridad.addMetodo(Metodos, App);
            }
            catch (Exception ex)
            {
                Respuesta.ResGral.FLAG = false;
                Respuesta.ResGral.TRACE = ex.StackTrace;
                Respuesta.ResGral.ERRORMESSAGE = ex.Message;
            }
            return Respuesta;
        }

        public SecutityDC addServicio(string Servicio, Int64 IdApp, bool Recurrente, Int64 App, string PasswordApp)
        {
            SecutityDC Respuesta = new SecutityDC();
            Respuesta.ResGral.FLAG = true;
            try
            {
                ////Checamos si tiene acceso al servicio y al metodo, en caso contrario mandamos un error controlado
                SecurityBR Seguridad = new SecurityBR();
                Seguridad.checkPermisoMethServ(App, PasswordApp, MethodBase.GetCurrentMethod().Name, ServiceNameClass);
                Respuesta.ResGral.FLAG = Seguridad.addServicio(Servicio, IdApp, Recurrente);
            }
            catch (Exception ex)
            {
                Respuesta.ResGral.FLAG = false;
                Respuesta.ResGral.TRACE = ex.StackTrace;
                Respuesta.ResGral.ERRORMESSAGE = ex.Message;
            }
            return Respuesta;
        }

        public SecutityDC addMenuxAppRol(Int64 Rol, Int64 IdApp, string Menu, string Img, string Obj, string Url, string Tool, Int64 Orden, Int64 App, string PasswordApp)
        {
            SecutityDC Respuesta = new SecutityDC();
            Respuesta.ResGral.FLAG = true;
            try
            {
                ////Checamos si tiene acceso al servicio y al metodo, en caso contrario mandamos un error controlado
                SecurityBR Seguridad = new SecurityBR();
                Seguridad.checkPermisoMethServ(App, PasswordApp, MethodBase.GetCurrentMethod().Name, ServiceNameClass);
                Respuesta.ResGral.FLAG = Seguridad.addMenuxAppRol(Rol, IdApp, Menu, Img, Obj, Url, Tool, Orden);
            }
            catch (Exception ex)
            {
                Respuesta.ResGral.FLAG = false;
                Respuesta.ResGral.TRACE = ex.StackTrace;
                Respuesta.ResGral.ERRORMESSAGE = ex.Message;
            }
            return Respuesta;
        }
        public SecutityDC addSubMenuxAppRol(Int64 IdSubMenu, string SubMenu, string Img, string Obj, string Url, string Tool, Int64 Orden, Int64 App, string PasswordApp)
        {
            SecutityDC Respuesta = new SecutityDC();
            Respuesta.ResGral.FLAG = true;
            try
            {
                ////Checamos si tiene acceso al servicio y al metodo, en caso contrario mandamos un error controlado
                SecurityBR Seguridad = new SecurityBR();
                Seguridad.checkPermisoMethServ(App, PasswordApp, MethodBase.GetCurrentMethod().Name, ServiceNameClass);
                Respuesta.ResGral.FLAG = Seguridad.addSubMenuxAppRol(IdSubMenu, SubMenu, Img, Obj, Url, Tool, Orden);
            }
            catch (Exception ex)
            {
                Respuesta.ResGral.FLAG = false;
                Respuesta.ResGral.TRACE = ex.StackTrace;
                Respuesta.ResGral.ERRORMESSAGE = ex.Message;
            }
            return Respuesta;
        }

        public SecutityDC addPermisosxObjeto(Int64 IdRol, string Pagina, string Obj, string TipoObj, string Tool, Int64 App, string PasswordApp)
        {
            SecutityDC Respuesta = new SecutityDC();
            Respuesta.ResGral.FLAG = true;
            try
            {
                ////Checamos si tiene acceso al servicio y al metodo, en caso contrario mandamos un error controlado
                SecurityBR Seguridad = new SecurityBR();
                Seguridad.checkPermisoMethServ(App, PasswordApp, MethodBase.GetCurrentMethod().Name, ServiceNameClass);
                Respuesta.ResGral.FLAG = Seguridad.addPermisosxObjeto(IdRol, Pagina, Obj, TipoObj, Tool);
            }
            catch (Exception ex)
            {
                Respuesta.ResGral.FLAG = false;
                Respuesta.ResGral.TRACE = ex.StackTrace;
                Respuesta.ResGral.ERRORMESSAGE = ex.Message;
            }
            return Respuesta;
        }

        public SecutityDC addPermisosxElementoObjeto(Int64 IdPermiosObj, string Elemento, string Tool, Int64 App, string PasswordApp)
        {
            SecutityDC Respuesta = new SecutityDC();
            Respuesta.ResGral.FLAG = true;
            try
            {
                ////Checamos si tiene acceso al servicio y al metodo, en caso contrario mandamos un error controlado
                SecurityBR Seguridad = new SecurityBR();
                Seguridad.checkPermisoMethServ(App, PasswordApp, MethodBase.GetCurrentMethod().Name, ServiceNameClass);
                Respuesta.ResGral.FLAG = Seguridad.addPermisosxElementoObjeto(IdPermiosObj, Elemento, Tool);
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
