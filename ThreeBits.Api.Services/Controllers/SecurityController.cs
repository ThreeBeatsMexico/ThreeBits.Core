using LatinoSeguros.Bussines.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Web.Http;
using ThreeBits.Business.Security;
using ThreeBits.Business.User;
using ThreeBits.Entities.Common;
using ThreeBits.Entities.Security;

namespace ThreeBits.Api.Services.Controllers
{
    [RoutePrefix("v1/api")]
    public class SecurityController : ApiController
    {

        private string ServiceNameClass = "SecurityService";

        /// <summary>
        /// Encripta o desencripta las cadenas enviadas.
        /// </summary>
        /// <param name="sValor">Recibe el valor a encriptar o desencriptar</param>
        /// <param name="Tipo">1.- Encripta; 2.- Desencripta</param>
        /// <param name="PasswordApp">Recibe el password de la aplicacion</param>
        /// <returns>Regresa el valor encriptado o desencriptado segun sea el caso en el elemento Encriptacion</returns>
        [JwtAuthentication]
        [Route("encryptDecryptChain")]
        [AcceptVerbs("POST")]
        public ProcessResult encryptDecryptChain(string Valor, int Tipo, string Llave, Int64 App, string PasswordApp)
        {
            ProcessResult Respuesta = new ProcessResult();
            SecurityResponse oSecurityRes = new SecurityResponse();
            SecurityBR oSecBr = new SecurityBR();

            Respuesta.flag = true;
            try
            {
                Respuesta.data = oSecurityRes.Encriptacion = oSecBr.encryptDecryptChain(Tipo, Valor, Llave, App);
            }
            catch (Exception ex)
            {
                Respuesta.flag = false;
                Respuesta.errorMessage = ex.Message;
            }
            return Respuesta;
        }

        /// <summary>
        /// Encripta o desencripta las cadenas enviadas.
        /// </summary>
        /// <param name="sValor">Recibe el valor a encriptar o desencriptar</param>
        /// <param name="Tipo">1.- Encripta; 2.- Desencripta</param>
        /// <param name="PasswordApp">Recibe el password de la aplicacion</param>
        /// <returns>Regresa el valor encriptado o desencriptado segun sea el caso en el elemento Encriptacion</returns>
        [JwtAuthentication]
        [Route("encryptDecrypt")]
        [AcceptVerbs("POST")]
        public ProcessResult encryptDesEncrypt(EncryptionBE request)
        {
            ProcessResult Respuesta = new ProcessResult();
            SecurityResponse oSecurityRes = new SecurityResponse();
            SecurityBR oSecBr = new SecurityBR();
            AplicacionBE oApp = new AplicacionBE();

            Respuesta.flag = true;
            try
            {
                var re = Request;
                var headers = re.Headers;
                var xAppId = re.Headers.GetValues("XAPPID").First();
                oApp = oSecBr.getAppInfo(xAppId.ToString());

                if (xAppId == null || xAppId == "")
                {
                    Respuesta.flag = false;
                    Respuesta.errorMessage = "Missing XAPPID Header";
                }
                else
                {
                     Respuesta.data = oSecurityRes.Encriptacion = oSecBr.encryptDesEncrypt(request.TIPO, request.VALORIN, oApp.IDAPLICACION);
                }
            }
            catch (Exception ex)
            {
                Respuesta.flag = false;

                Respuesta.errorMessage = ex.Message;
            }
            return Respuesta;
        }



        




        /// <summary>
        /// Obtenemos de BD de los objetos por App, Rol y pagina.
        /// </summary>
        /// <param name="Rol">Recibe el rol del usuario</param>
        /// <param name="Pagina">Recibe la pagina</param>
        /// <param name="App">Recibe el id de la aplicacion</param>
        /// <param name="PasswordApp">Recibe el password de la aplicacion</param>
        /// <returns>Regresa la informacion en la entidad PermisoXObjetos</returns>
        public ProcessResult getObjetosXAppRolPage(long Rol, string Pagina, Int64 App, string PasswordApp)
        {
            ProcessResult Respuesta = new ProcessResult();
            SecurityResponse oSecurityRes = new SecurityResponse();
            SecurityBR oSecBr = new SecurityBR();
            Respuesta.flag = true;
            try
            {
                //Checamos si tiene acceso al servicio y al metodo, en caso contrario mandamos un error controlado
                Respuesta.data = oSecurityRes.PermisoXObjetos = oSecBr.getObjetosXAppRolPage(Rol, Pagina, App);
            }
            catch (Exception ex)
            {
                Respuesta.flag = false;

                Respuesta.errorMessage = ex.Message;
            }
            return Respuesta;
        }

        /// <summary>
        /// Obtenemos de BD los elementos de un objeto.
        /// </summary>
        /// <param name="IdPermisosXObj">Recibe el Id del Objeto</param>
        /// <param name="App">Recibe el id de la aplicacion</param>
        /// <param name="PasswordApp">Recibe el password de la aplicacion</param>
        /// <returns>Regresa la informacion en la entidad ElementosObjetos</returns>
        public ProcessResult getElementsObjectsXIdObj(long IdPermisosXObj, Int64 App, string PasswordApp)
        {
            ProcessResult Respuesta = new ProcessResult();
            SecurityResponse oSecurityRes = new SecurityResponse();
            SecurityBR oSecBr = new SecurityBR();

            Respuesta.flag = true;
            try
            {
                Respuesta.data = oSecurityRes.ElementosObjetos = oSecBr.getElementsObjectsXIdObj(IdPermisosXObj, App);
            }
            catch (Exception ex)
            {
                Respuesta.flag = false;

                Respuesta.errorMessage = ex.Message;
            }
            return Respuesta;
        }

        /// <summary>
        /// Obtenemos la lista de menus por perfil que le corresponden
        /// </summary>
        /// <param name="Perfil">Recibe el perfil del usuario</param>
        /// <param name="sApp">Recibe el nombre de la aplicacion</param>
        /// <param name="sPasswordApp">Recibe el password de la aplicacion</param>
        /// <returns>Regresa la informacion en la entidad PermisosXMenu</returns>
        [JwtAuthentication]
        [Route("getMenu")]
        [AcceptVerbs("GET")]
        public ProcessResult getMenuXAppRol(long Rol)
        {
            ProcessResult Respuesta = new ProcessResult();
            SecurityResponse oSecurityRes = new SecurityResponse();
            SecurityBR oSecBr = new SecurityBR();
            AplicacionBE oApp = new AplicacionBE();
            Respuesta.flag = true;
            try
            {
                var re = Request;
                var headers = re.Headers;
                var xAppId = re.Headers.GetValues("XAPPID").First();
                oApp = oSecBr.getAppInfo(xAppId.ToString());

                if (xAppId == null || xAppId == "")
                {
                    Respuesta.flag = false;
                    Respuesta.errorMessage = "Missing XAPPID Header";
                }
                else
                {
                     Respuesta.data = oSecurityRes.PermisosXMenu = oSecBr.getMenuXAppRol(Rol, oApp.IDAPLICACION);
                }
            }
            catch (Exception ex)
            {
                Respuesta.flag = false;

                Respuesta.errorMessage = ex.Message;
            }
            return Respuesta;
        }

        /// <summary>
        /// Obtiene la lista de submenus por menu y perfil que le corresponden
        /// </summary>
        /// <param name="IdPermisoMenu">Recibe el id del menu</param>
        /// <param name="Idperfil">Recibe el perfil del usuario</param>
        /// <param name="sApp">Recibe el nombre de la aplicacion</param>
        /// <param name="sPasswordApp">Recibe el password de la aplicacion</param>
        /// <returns>Regresa la informacion en la entidad PermisosXSubmenu</returns>   
        [JwtAuthentication]
        [Route("getSubMenu")]
        [AcceptVerbs("GET")]
        public ProcessResult getSubMenuXIdMenu(long IdPermisoMenu)
        {
            ProcessResult Respuesta = new ProcessResult();
            SecurityResponse oSecurityRes = new SecurityResponse();
            SecurityBR oSecBr = new SecurityBR();
            AplicacionBE oApp = new AplicacionBE();
            Respuesta.flag = true;
            try
            {
                var re = Request;
                var headers = re.Headers;
                var xAppId = re.Headers.GetValues("XAPPID").First();
                oApp = oSecBr.getAppInfo(xAppId.ToString());

                if (xAppId == null || xAppId == "")
                {
                    Respuesta.flag = false;
                    Respuesta.errorMessage = "Missing XAPPID Header";
                }
                else
                {
                    Respuesta.data = oSecurityRes.PermisosXSubmenu = oSecBr.getSubMenuXIdMenu(IdPermisoMenu, oApp.IDAPLICACION);
                }
            }
            catch (Exception ex)
            {
                Respuesta.flag = false;

                Respuesta.errorMessage = ex.Message;
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
        /// 
        [JwtAuthentication]
        [Route("getUsuarioFull")]
        [AcceptVerbs("POST")]
        public ProcessResult getUsuarioFull(ReglasBE Reglas)
        {
            ProcessResult Respuesta = new ProcessResult();
            SecurityResponse oSecurityRes = new SecurityResponse();
            SecurityBR oSecBr = new SecurityBR();
            AplicacionBE oApp = new AplicacionBE();
            Respuesta.flag = true;
            try
            {
                var re = Request;
                var headers = re.Headers;
                var xAppId = re.Headers.GetValues("XAPPID").First();
                oApp = oSecBr.getAppInfo(xAppId.ToString());

                Reglas.IDAPP = oApp.IDAPLICACION;

                if (xAppId == null || xAppId == "")
                {
                    Respuesta.flag = false;
                    Respuesta.errorMessage = "Missing XAPPID Header";
                }
                else
                {
                    UsersBR UsuariosBR = new UsersBR();
                    Respuesta.data = UsuariosBR.getUsuarioFull(Reglas, oApp.IDAPLICACION);
                }
            }
            catch (Exception ex)
            {
                Respuesta.flag = false;

                Respuesta.errorMessage = ex.Message;
            }
            return Respuesta;
        }





        /// <summary>
        /// 
        /// </summary>
        /// <param name="Rol"></param>
        /// <param name="App"></param>
        /// <param name="PasswordApp"></param>
        /// <returns></returns>
        public ProcessResult getMenuXAppRolAdmin(long Rol, Int64 App, string PasswordApp)
        {
            ProcessResult Respuesta = new ProcessResult();
            SecurityResponse oSecurityRes = new SecurityResponse();

            Respuesta.flag = true;
            try
            {
                //Checamos si tiene acceso al servicio y al metodo, en caso contrario mandamos un error controlado
                SecurityBR Seguridad = new SecurityBR();
                Seguridad.checkPermisoMethServ(App, PasswordApp, MethodBase.GetCurrentMethod().Name, ServiceNameClass);
                Respuesta.data = oSecurityRes.PermisosXMenu = Seguridad.getMenuXAppRolAdmin(Rol, App);
            }
            catch (Exception ex)
            {
                Respuesta.flag = false;

                Respuesta.errorMessage = ex.Message;
            }
            return Respuesta;
        }

        /// <summary>
        /// Obtiene la lista de submenus por menu y perfil que le corresponden
        /// </summary>
        /// <param name="IdPermisoMenu">Recibe el id del menu</param>
        /// <param name="Idperfil">Recibe el perfil del usuario</param>
        /// <param name="sApp">Recibe el nombre de la aplicacion</param>
        /// <param name="sPasswordApp">Recibe el password de la aplicacion</param>
        /// <returns>Regresa la informacion en la entidad PermisosXSubmenu</returns>   
        public ProcessResult getSubMenuXIdMenuAdmin(long IdPermisoMenu, Int64 App, string PasswordApp)
        {
            ProcessResult Respuesta = new ProcessResult();
            SecurityResponse oSecurityRes = new SecurityResponse();

            Respuesta.flag = true;
            try
            {
                //Checamos si tiene acceso al servicio y al metodo, en caso contrario mandamos un error controlado
                SecurityBR Seguridad = new SecurityBR();
                Seguridad.checkPermisoMethServ(App, PasswordApp, MethodBase.GetCurrentMethod().Name, ServiceNameClass);
                Respuesta.data = oSecurityRes.PermisosXSubmenu = Seguridad.getSubMenuXIdMenuAdmin(IdPermisoMenu, App);
            }
            catch (Exception ex)
            {
                Respuesta.flag = false;

                Respuesta.errorMessage = ex.Message;
            }
            return Respuesta;
        }



        /// <summary>
        /// Obtiene la lista de Aplicaciones de toda la seguridad Latino
        /// </summary>
        /// <param name="sApp">Recibe el nombre de la aplicacion</param>
        /// <param name="sPasswordApp">Recibe el password de la aplicacion</param>
        /// <returns>Regresa la informacion en la entidad AplicacionBE</returns>   
        public ProcessResult getAplicaciones(string idAplicacion, string txtBusqueda, Int64 App, string PasswordApp)
        {
            ProcessResult Respuesta = new ProcessResult();
            SecurityResponse oSecurityRes = new SecurityResponse();

            Respuesta.flag = true;
            try
            {
                //Checamos si tiene acceso al servicio y al metodo, en caso contrario mandamos un error controlado
                SecurityBR Seguridad = new SecurityBR();
                Seguridad.checkPermisoMethServ(App, PasswordApp, MethodBase.GetCurrentMethod().Name, ServiceNameClass);
                Respuesta.data = oSecurityRes.Aplicaciones = Seguridad.getAplicaciones(idAplicacion, txtBusqueda, App);
            }
            catch (Exception ex)
            {
                Respuesta.flag = false;

                Respuesta.errorMessage = ex.Message;
            }
            return Respuesta;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Aplicacion"></param>
        /// <param name="App"></param>
        /// <param name="PasswordApp"></param>
        /// <returns></returns>
        public ProcessResult addAplicacion(AplicacionBE Aplicacion, Int64 App, string PasswordApp)
        {
            ProcessResult Respuesta = new ProcessResult();
            SecurityResponse oSecurityRes = new SecurityResponse();

            Respuesta.flag = true;
            try
            {
                ////Checamos si tiene acceso al servicio y al metodo, en caso contrario mandamos un error controlado
                SecurityBR Seguridad = new SecurityBR();
                Seguridad.checkPermisoMethServ(App, PasswordApp, MethodBase.GetCurrentMethod().Name, ServiceNameClass);
                Respuesta.flag = Seguridad.addAplicacion(Aplicacion, App);
            }
            catch (Exception ex)
            {
                Respuesta.flag = false;

                Respuesta.errorMessage = ex.Message;
            }
            return Respuesta;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Aplicacion"></param>
        /// <param name="App"></param>
        /// <param name="PasswordApp"></param>
        /// <returns></returns>
        public ProcessResult updAplicacion(AplicacionBE Aplicacion, Int64 App, string PasswordApp)
        {
            ProcessResult Respuesta = new ProcessResult();
            SecurityResponse oSecurityRes = new SecurityResponse();

            Respuesta.flag = true;
            try
            {
                ////Checamos si tiene acceso al servicio y al metodo, en caso contrario mandamos un error controlado
                SecurityBR Seguridad = new SecurityBR();
                Respuesta.flag = Seguridad.updAplicacion(Aplicacion, App);
            }
            catch (Exception ex)
            {
                Respuesta.flag = false;

                Respuesta.errorMessage = ex.Message;
            }
            return Respuesta;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="idMenu"></param>
        /// <param name="App"></param>
        /// <param name="PasswordApp"></param>
        /// <returns></returns>
        public ProcessResult delMenu(Int64 idMenu, Int64 App, string PasswordApp)
        {
            ProcessResult Respuesta = new ProcessResult();
            SecurityResponse oSecurityRes = new SecurityResponse();

            Respuesta.flag = true;
            try
            {
                ////Checamos si tiene acceso al servicio y al metodo, en caso contrario mandamos un error controlado
                SecurityBR Seguridad = new SecurityBR();
                Respuesta.flag = Seguridad.delMenu(idMenu, App);
            }
            catch (Exception ex)
            {
                Respuesta.flag = false;

                Respuesta.errorMessage = ex.Message;
            }
            return Respuesta;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="idMenu"></param>
        /// <param name="App"></param>
        /// <param name="PasswordApp"></param>
        /// <returns></returns>
        public ProcessResult delSubMenu(Int64 idMenu, Int64 App, string PasswordApp)
        {
            ProcessResult Respuesta = new ProcessResult();
            SecurityResponse oSecurityRes = new SecurityResponse();

            Respuesta.flag = true;
            try
            {
                ////Checamos si tiene acceso al servicio y al metodo, en caso contrario mandamos un error controlado
                SecurityBR Seguridad = new SecurityBR();
                Respuesta.flag = Seguridad.delSubMenu(idMenu, App);
            }
            catch (Exception ex)
            {
                Respuesta.flag = false;

                Respuesta.errorMessage = ex.Message;
            }
            return Respuesta;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="MenuItem"></param>
        /// <param name="App"></param>
        /// <param name="PasswordApp"></param>
        /// <returns></returns>
        public ProcessResult updMenuxAppRol(PermisosXMenuBE MenuItem, Int64 App, string PasswordApp)
        {
            ProcessResult Respuesta = new ProcessResult();
            SecurityResponse oSecurityRes = new SecurityResponse();

            Respuesta.flag = true;
            try
            {
                ////Checamos si tiene acceso al servicio y al metodo, en caso contrario mandamos un error controlado
                SecurityBR Seguridad = new SecurityBR();
                Seguridad.checkPermisoMethServ(App, PasswordApp, MethodBase.GetCurrentMethod().Name, ServiceNameClass);
                Respuesta.flag = Seguridad.updMenuxAppRol(MenuItem.IDPERMISOSMENU, MenuItem.NOMBREMENU, MenuItem.IMAGEN, MenuItem.TIPOOBJETO, MenuItem.URL, MenuItem.TOOLTIP, MenuItem.ORDENMENU, MenuItem.ACTIVO, Convert.ToString(App));
            }
            catch (Exception ex)
            {
                Respuesta.flag = false;
                Respuesta.errorMessage = ex.Message;
            }
            return Respuesta;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="SubMenuItem"></param>
        /// <param name="App"></param>
        /// <param name="PasswordApp"></param>
        /// <returns></returns>
        public ProcessResult updSubMenuxAppRol(PermisoXSubmenuBE SubMenuItem, Int64 App, string PasswordApp)
        {
            ProcessResult Respuesta = new ProcessResult();
            Respuesta.flag = true;
            try
            {
                ////Checamos si tiene acceso al servicio y al metodo, en caso contrario mandamos un error controlado
                SecurityBR Seguridad = new SecurityBR();
                Seguridad.checkPermisoMethServ(App, PasswordApp, MethodBase.GetCurrentMethod().Name, ServiceNameClass);
                Respuesta.flag = Seguridad.updSubMenuxAppRol(SubMenuItem.IDPERMISOSMENU, SubMenuItem.IDPERMISOSXSUBMENU, SubMenuItem.NOMBRESUBMENU, SubMenuItem.IMAGEN, SubMenuItem.TIPOOBJETO, SubMenuItem.URL, SubMenuItem.TOOLTIP, SubMenuItem.ORDENSUBMENU, SubMenuItem.ACTIVO, Convert.ToString(App));
            }
            catch (Exception ex)
            {
                Respuesta.flag = false;

                Respuesta.errorMessage = ex.Message;
            }
            return Respuesta;
        }

        /// <summary>
        /// Obtenemos la lista de menus por perfil que le corresponden
        /// </summary>
        /// <param name="Perfil">Recibe el perfil del usuario</param>
        /// <param name="sApp">Recibe el nombre de la aplicacion</param>
        /// <param name="sPasswordApp">Recibe el password de la aplicacion</param>
        /// <returns>Regresa la informacion en la entidad PermisosXMenu</returns>
        public ProcessResult getMenuxRol(long Rol, Int64 AppxRol, Int64 App, string PasswordApp)
        {
            ProcessResult Respuesta = new ProcessResult();
            SecurityResponse oSecurityRes = new SecurityResponse();
            Respuesta.flag = true;
            try
            {
                //Checamos si tiene acceso al servicio y al metodo, en caso contrario mandamos un error controlado
                SecurityBR Seguridad = new SecurityBR();
                Seguridad.checkPermisoMethServ(App, PasswordApp, MethodBase.GetCurrentMethod().Name, ServiceNameClass);
                Respuesta.data = oSecurityRes.PermisosXMenu = Seguridad.getMenuXAppRol(Rol, AppxRol);
            }
            catch (Exception ex)
            {
                Respuesta.flag = false;

                Respuesta.errorMessage = ex.Message;
            }
            return Respuesta;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Rol"></param>
        /// <param name="AppxRol"></param>
        /// <param name="App"></param>
        /// <param name="PasswordApp"></param>
        /// <returns></returns>
        public ProcessResult getMenuxRolAdmin(long Rol, Int64 AppxRol, Int64 App, string PasswordApp)
        {
            ProcessResult Respuesta = new ProcessResult();
            SecurityResponse oSecurityRes = new SecurityResponse();
            Respuesta.flag = true;
            try
            {
                //Checamos si tiene acceso al servicio y al metodo, en caso contrario mandamos un error controlado
                SecurityBR Seguridad = new SecurityBR();
                Seguridad.checkPermisoMethServ(App, PasswordApp, MethodBase.GetCurrentMethod().Name, ServiceNameClass);
                Respuesta.data = oSecurityRes.PermisosXMenu = Seguridad.getMenuXAppRolAdmin(Rol, AppxRol);
            }
            catch (Exception ex)
            {
                Respuesta.flag = false;

                Respuesta.errorMessage = ex.Message;
            }
            return Respuesta;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Rol"></param>
        /// <param name="AppRol"></param>
        /// <param name="App"></param>
        /// <param name="PasswordApp"></param>
        /// <returns></returns>
        public ProcessResult addRolxApp(string Rol, Int64 AppRol, Int64 App, string PasswordApp)
        {
            ProcessResult Respuesta = new ProcessResult();
            Respuesta.flag = true;
            try
            {
                ////Checamos si tiene acceso al servicio y al metodo, en caso contrario mandamos un error controlado
                SecurityBR Seguridad = new SecurityBR();
                Seguridad.checkPermisoMethServ(App, PasswordApp, MethodBase.GetCurrentMethod().Name, ServiceNameClass);
                Respuesta.flag = Seguridad.addRolxApp(Rol, AppRol);
            }
            catch (Exception ex)
            {
                Respuesta.flag = false;

                Respuesta.errorMessage = ex.Message;
            }
            return Respuesta;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Metodos"></param>
        /// <param name="App"></param>
        /// <param name="PasswordApp"></param>
        /// <returns></returns>
        public ProcessResult addMetodo(List<WCFMetodosBE> Metodos, Int64 App, string PasswordApp)
        {
            ProcessResult Respuesta = new ProcessResult();
            Respuesta.flag = true;
            try
            {
                ////Checamos si tiene acceso al servicio y al metodo, en caso contrario mandamos un error controlado
                SecurityBR Seguridad = new SecurityBR();
                Seguridad.checkPermisoMethServ(App, PasswordApp, MethodBase.GetCurrentMethod().Name, ServiceNameClass);
                Respuesta.flag = Seguridad.addMetodo(Metodos, App);
            }
            catch (Exception ex)
            {
                Respuesta.flag = false;

                Respuesta.errorMessage = ex.Message;
            }
            return Respuesta;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Servicio"></param>
        /// <param name="IdApp"></param>
        /// <param name="Recurrente"></param>
        /// <param name="App"></param>
        /// <param name="PasswordApp"></param>
        /// <returns></returns>
        public ProcessResult addServicio(string Servicio, Int64 IdApp, bool Recurrente, Int64 App, string PasswordApp)
        {
            ProcessResult Respuesta = new ProcessResult();
            Respuesta.flag = true;
            try
            {
                ////Checamos si tiene acceso al servicio y al metodo, en caso contrario mandamos un error controlado
                SecurityBR Seguridad = new SecurityBR();
                Seguridad.checkPermisoMethServ(App, PasswordApp, MethodBase.GetCurrentMethod().Name, ServiceNameClass);
                Respuesta.flag = Seguridad.addServicio(Servicio, IdApp, Recurrente);
            }
            catch (Exception ex)
            {
                Respuesta.flag = false;

                Respuesta.errorMessage = ex.Message;
            }
            return Respuesta;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Rol"></param>
        /// <param name="IdApp"></param>
        /// <param name="Menu"></param>
        /// <param name="Img"></param>
        /// <param name="Obj"></param>
        /// <param name="Url"></param>
        /// <param name="Tool"></param>
        /// <param name="Orden"></param>
        /// <param name="App"></param>
        /// <param name="PasswordApp"></param>
        /// <returns></returns>
        public ProcessResult addMenuxAppRol(Int64 Rol, Int64 IdApp, string Menu, string Img, string Obj, string Url, string Tool, Int64 Orden, Int64 App, string PasswordApp)
        {
            ProcessResult Respuesta = new ProcessResult();
            Respuesta.flag = true;
            try
            {
                ////Checamos si tiene acceso al servicio y al metodo, en caso contrario mandamos un error controlado
                SecurityBR Seguridad = new SecurityBR();
                Respuesta.flag = Seguridad.addMenuxAppRol(Rol, IdApp, Menu, Img, Obj, Url, Tool, Orden);
            }
            catch (Exception ex)
            {
                Respuesta.flag = false;

                Respuesta.errorMessage = ex.Message;
            }
            return Respuesta;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdSubMenu"></param>
        /// <param name="SubMenu"></param>
        /// <param name="Img"></param>
        /// <param name="Obj"></param>
        /// <param name="Url"></param>
        /// <param name="Tool"></param>
        /// <param name="Orden"></param>
        /// <param name="App"></param>
        /// <param name="PasswordApp"></param>
        /// <returns></returns>
        public ProcessResult addSubMenuxAppRol(Int64 IdSubMenu, string SubMenu, string Img, string Obj, string Url, string Tool, Int64 Orden, Int64 App, string PasswordApp)
        {
            ProcessResult Respuesta = new ProcessResult();
            Respuesta.flag = true;
            try
            {
                ////Checamos si tiene acceso al servicio y al metodo, en caso contrario mandamos un error controlado
                SecurityBR Seguridad = new SecurityBR();
                Respuesta.flag = Seguridad.addSubMenuxAppRol(IdSubMenu, SubMenu, Img, Obj, Url, Tool, Orden);
            }
            catch (Exception ex)
            {
                Respuesta.flag = false;

                Respuesta.errorMessage = ex.Message;
            }
            return Respuesta;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdRol"></param>
        /// <param name="Pagina"></param>
        /// <param name="Obj"></param>
        /// <param name="TipoObj"></param>
        /// <param name="Tool"></param>
        /// <param name="App"></param>
        /// <param name="PasswordApp"></param>
        /// <returns></returns>
        public ProcessResult addPermisosxObjeto(Int64 IdRol, string Pagina, string Obj, string TipoObj, string Tool, Int64 App, string PasswordApp)
        {
            ProcessResult Respuesta = new ProcessResult();
            Respuesta.flag = true;
            try
            {
                ////Checamos si tiene acceso al servicio y al metodo, en caso contrario mandamos un error controlado
                SecurityBR Seguridad = new SecurityBR();
                Seguridad.checkPermisoMethServ(App, PasswordApp, MethodBase.GetCurrentMethod().Name, ServiceNameClass);
                Respuesta.flag = Seguridad.addPermisosxObjeto(IdRol, Pagina, Obj, TipoObj, Tool);
            }
            catch (Exception ex)
            {
                Respuesta.flag = false;

                Respuesta.errorMessage = ex.Message;
            }
            return Respuesta;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdPermiosObj"></param>
        /// <param name="Elemento"></param>
        /// <param name="Tool"></param>
        /// <param name="App"></param>
        /// <param name="PasswordApp"></param>
        /// <returns></returns>
        public ProcessResult addPermisosxElementoObjeto(Int64 IdPermiosObj, string Elemento, string Tool, Int64 App, string PasswordApp)
        {
            ProcessResult Respuesta = new ProcessResult();
            Respuesta.flag = true;
            try
            {
                ////Checamos si tiene acceso al servicio y al metodo, en caso contrario mandamos un error controlado
                SecurityBR Seguridad = new SecurityBR();
                Seguridad.checkPermisoMethServ(App, PasswordApp, MethodBase.GetCurrentMethod().Name, ServiceNameClass);
                Respuesta.flag = Seguridad.addPermisosxElementoObjeto(IdPermiosObj, Elemento, Tool);
            }
            catch (Exception ex)
            {
                Respuesta.flag = false;

                Respuesta.errorMessage = ex.Message;
            }
            return Respuesta;
        }

       

        

       

    }
}