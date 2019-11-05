using PVLSECVB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThreeBits.Data.Security;
using ThreeBits.Entities.Security;

namespace ThreeBits.Business.Security
{
    public class SecurityBR
    {
        public void checkPermisoMethServ(Int64 App, string sPasswordApp, string sMethodName, string sServiceName)
        {
            try
            {
                SecurityDA SeguridadDA = new SecurityDA();
                AplicacionBE PermisosApp = new AplicacionBE();
                EncryptDecryptSecVB CrypDecrypt = new EncryptDecryptSecVB();

                bool Permiso = false;
                PermisosApp = SeguridadDA.checkPermisoXAplicacion(App, sPasswordApp);
                if (CrypDecrypt.DecryptString(PermisosApp.PASSWORD, "") == CrypDecrypt.DecryptString(sPasswordApp, "")) Permiso = true;
                ////Si sale negativo mandamos una excepcion controlada
                if (!Permiso) throw new SeguridadException("La aplicación no tiene acceso al servicio: " + sServiceName);

                Permiso = false;
                List<WCFMetodosBE> MetodosXApp = new List<WCFMetodosBE>();
                MetodosXApp = SeguridadDA.checkMetodoXApp(App, sServiceName, sMethodName);
                if (MetodosXApp.Count == 0) throw new SeguridadException("La aplicación no tiene acceso al método: " + sMethodName);
            }
            catch (SeguridadException exCu)
            {
                throw new SeguridadException(exCu.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<WCFMetodosBE> checkMetodoXApp(Int64 App, string sServiceName, string sMethodName)
        {
            SecurityDA oSecurityDA = new SecurityDA();
            List<WCFMetodosBE> lstMetodos = new List<WCFMetodosBE>();
            try
            {
                lstMetodos = oSecurityDA.checkMetodoXApp(App, sServiceName, sMethodName);
            }
            catch (SeguridadException exCu)
            {
                throw new SeguridadException(exCu.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return lstMetodos;
        }

        public List<PermisosXObjetosBE> getObjetosXAppRolPage(long Rol, string Pagina, Int64 App)
        {
            try
            {
                List<PermisosXObjetosBE> PermisoXObjetos = new List<PermisosXObjetosBE>();
                SecurityDA Seguridad = new SecurityDA();
                PermisoXObjetos = Seguridad.getObjetosXAppRolPage(Rol, Pagina, App);
                return PermisoXObjetos;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<PermisoXElementosObjBE> getElementsObjectsXIdObj(long IdPermisosXObj, Int64 App)
        {
            try
            {
                List<PermisoXElementosObjBE> PermisoXElementosObj = new List<PermisoXElementosObjBE>();
                SecurityDA Seguridad = new SecurityDA();
                PermisoXElementosObj = Seguridad.getElementsObjectsXIdObj(IdPermisosXObj, App);
                return PermisoXElementosObj;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<PermisosXMenuBE> getMenuXAppRol(long Rol, Int64 App)
        {
            try
            {
                List<PermisosXMenuBE> PermisosXMenu = new List<PermisosXMenuBE>();
                SecurityDA Seguridad = new SecurityDA();
                PermisosXMenu = Seguridad.getMenuXAppRol(Rol, App);
                return PermisosXMenu;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<PermisoXSubmenuBE> getSubMenuXIdMenu(long IdPermisoMenu, Int64 App)
        {
            try
            {
                List<PermisoXSubmenuBE> PermisosXSubmenu = new List<PermisoXSubmenuBE>();
                SecurityDA Seguridad = new SecurityDA();
                PermisosXSubmenu = Seguridad.getSubMenuXIdMenu(IdPermisoMenu, App);
                return PermisosXSubmenu;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<PermisosXMenuBE> getMenuXAppRolAdmin(long Rol, Int64 App)
        {
            try
            {
                List<PermisosXMenuBE> PermisosXMenu = new List<PermisosXMenuBE>();
                SecurityDA Seguridad = new SecurityDA();
                PermisosXMenu = Seguridad.getMenuXAppRolAdmin(Rol, App);
                return PermisosXMenu;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<PermisoXSubmenuBE> getSubMenuXIdMenuAdmin(long IdPermisoMenu, Int64 App)
        {
            try
            {
                List<PermisoXSubmenuBE> PermisosXSubmenu = new List<PermisoXSubmenuBE>();
                SecurityDA Seguridad = new SecurityDA();
                PermisosXSubmenu = Seguridad.getSubMenuXIdMenuAdmin(IdPermisoMenu, App);
                return PermisosXSubmenu;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public EncryptionBE encryptDesEncrypt(int Tipo, string Valor, Int64 App)
        {
            try
            {
                EncryptDecryptSecVB CrypDecrypt = new EncryptDecryptSecVB();
                EncryptionBE Encriptacion = new EncryptionBE();
                if (Tipo == 1) ////Encrypta
                {
                    Encriptacion.VALORIN = Valor;
                    Encriptacion.VALOROUT = CrypDecrypt.EncryptString(Valor, "");
                }
                else ///Desencrypta
                {
                    Encriptacion.VALORIN = Valor;
                    Encriptacion.VALOROUT = CrypDecrypt.DecryptString(Valor, "");
                }
                return Encriptacion;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<AplicacionBE> getAplicaciones(string idAplicacion, string txtbusqueda, Int64 App)
        {
            try
            {
                List<AplicacionBE> Aplicaciones = new List<AplicacionBE>();
                SecurityDA Seguridad = new SecurityDA();
                Aplicaciones = Seguridad.getAplicaciones(idAplicacion, txtbusqueda, App);
                return Aplicaciones;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public bool addAplicacion(AplicacionBE Aplicacion, Int64 App)
        {
            try
            {
                SecurityDA Seguridad = new SecurityDA();

                bool bExistUsr = checkApp(Aplicacion.DESCRIPCION.ToString().ToUpper());
                if (bExistUsr) throw new Exception("La Aplicacion ya existe.");
                return Seguridad.addAplicacion(Aplicacion, App);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool updAplicacion(AplicacionBE Aplicacion, Int64 App)
        {
            try
            {
                SecurityDA Seguridad = new SecurityDA();
                //bool bExistUsr = checkApp(Aplicacion.DESCRIPCION.ToString().ToUpper());
                //if (!bExistUsr) throw new Exception("La Aplicacion ya existe.");
                return Seguridad.updAplicacion(Aplicacion, App);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool updMenuxAppRol(Int64 idMenu, string Menu, string Img, string TpoObj, string Url, string Tool, Int64 Orden, bool Activo, string idApp)
        {
            try
            {
                SecurityDA Seguridad = new SecurityDA();
                return Seguridad.updMenuxAppRol(idMenu, Menu, Img, TpoObj, Url, Tool, Orden, Activo, idApp);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool updSubMenuxAppRol(Int64 idPermisoMenu, Int64 IdPermisoSubmenu, string SubMenu, string Img, string TpoObj, string Url, string Tool, Int64 Orden, bool Activo, string App)
        {
            try
            {
                SecurityDA Seguridad = new SecurityDA();
                return Seguridad.updSubMenuxAppRol(idPermisoMenu, IdPermisoSubmenu, SubMenu, Img, TpoObj, Url, Tool, Orden, Activo, App);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }



        public bool addRolxApp(string Rol, Int64 App)
        {
            try
            {
                SecurityDA Seguridad = new SecurityDA();

                bool bExistUsr = checkRol(Rol.ToString().ToUpper(), App);
                if (bExistUsr) throw new Exception("El Rol ya existe.");
                return Seguridad.addRolxApp(Rol.ToString().ToUpper(), App);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool addMetodo(List<WCFMetodosBE> lstMetodos, Int64 IdApp)
        {
            try
            {
                SecurityDA Seguridad = new SecurityDA();

                //[HVG][20012016][estas validaciones se estan realizando desde el front]
                //bool bExistUsr = checkMetodo(Metodo.ToString().ToUpper(), IdApp,Servicio);
                //if (bExistUsr) throw new Exception("El MEtodo ya existe.");
                return Seguridad.addMetodo(lstMetodos, IdApp);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool addServicio(string Servicio, Int64 IdApp, bool Recurrente)
        {
            try
            {
                SecurityDA Seguridad = new SecurityDA();

                bool bExistUsr = checkServicio(Servicio.ToString().ToUpper(), IdApp);
                if (bExistUsr) throw new Exception("El Servicio ya existe.");
                return Seguridad.addServicio(Servicio);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool addMenuxAppRol(Int64 Rol, Int64 IdApp, string Menu, string Img, string Obj, string Url, string Tool, Int64 Orden)
        {
            try
            {
                SecurityDA Seguridad = new SecurityDA();

                bool bExistUsr = checkMenu(Menu, Rol, IdApp);
                if (bExistUsr) throw new Exception("El Servicio ya existe.");
                return Seguridad.addMenuxAppRol(Rol, IdApp, Menu, Img, Obj, Url, Tool, Orden);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool addSubMenuxAppRol(Int64 IdSubMenu, string SubMenu, string Img, string Obj, string Url, string Tool, Int64 Orden)
        {
            try
            {
                SecurityDA Seguridad = new SecurityDA();

                bool bExistUsr = checkSubMenu(SubMenu, IdSubMenu);
                if (bExistUsr) throw new Exception("El Servicio ya existe.");
                return Seguridad.addSubMenuxAppRol(IdSubMenu, SubMenu, Img, Obj, Url, Tool, Orden);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool addPermisosxObjeto(Int64 IdRol, string Pagina, string Obj, string TipoObj, string Tool)
        {
            try
            {
                SecurityDA Seguridad = new SecurityDA();


                return Seguridad.addPermisosxObjeto(IdRol, Pagina, Obj, TipoObj, Tool);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool addPermisosxElementoObjeto(Int64 IdPermiosObj, string Elemento, string Tool)
        {
            try
            {
                SecurityDA Seguridad = new SecurityDA();

                // bool bExistUsr = checkSubMenu(SubMenu, IdSubMenu);
                //if (bExistUsr) throw new Exception("El Servicio ya existe.");
                return Seguridad.addPermisosxElementoObjeto(IdPermiosObj, Elemento, Tool);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool delMenu(Int64 idMenu, Int64 App)
        {
            try
            {
                SecurityDA Seguridad = new SecurityDA();
                return Seguridad.delMenu(idMenu, App);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool delSubMenu(Int64 idSubMenu, Int64 App)
        {
            try
            {
                SecurityDA Seguridad = new SecurityDA();
                return Seguridad.delMenu(idSubMenu, App);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool checkApp(string App)
        {
            try
            {
                SecurityDA seguridad = new SecurityDA();
                return seguridad.checkApp(App);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool checkRol(string Rol, Int64 App)
        {
            try
            {
                SecurityDA seguridad = new SecurityDA();
                return seguridad.checkRol(Rol, App);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool checkMetodo(string Metodo, Int64 IdApp, Int64 Servicio)
        {
            try
            {
                SecurityDA seguridad = new SecurityDA();
                return seguridad.checkMetodo(Metodo, IdApp, Servicio);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool checkServicio(string Servicio, Int64 IdApp)
        {
            try
            {
                SecurityDA seguridad = new SecurityDA();
                return seguridad.checkServicio(Servicio, IdApp);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool checkMenu(string Menu, Int64 Rol, Int64 IdApp)
        {
            try
            {
                SecurityDA seguridad = new SecurityDA();
                return seguridad.checkMenuxAppRol(Menu, Rol, IdApp);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool checkSubMenu(string SubMenu, Int64 PermisosMenu)
        {
            try
            {
                SecurityDA seguridad = new SecurityDA();
                return seguridad.checkSubMenuxAppRol(SubMenu, PermisosMenu);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


    }




    public class SeguridadException : Exception
    {
        public SeguridadException()
        {
        }
        public SeguridadException(string message)
            : base(message)
        {
        }
        public SeguridadException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
