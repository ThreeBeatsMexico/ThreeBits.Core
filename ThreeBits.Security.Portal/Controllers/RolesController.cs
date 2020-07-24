using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using ThreeBits.Security.Portal.COMMONWCF;
using ThreeBits.Security.Portal.Helpers;
using ThreeBits.Security.Portal.Models;
using ThreeBits.Security.Portal.Properties;

namespace ThreeBits.Security.Portal.Controllers
{
    public class RolesController : Controller
    {
        // GET: Roles
        public ActionResult Index()
        {
            HelperTools hHelp = new HelperTools();
            List<CatalogosBE> lstAplicaciones = new List<CatalogosBE>();
            lstAplicaciones = hHelp.SetDdlCatalogos("13");
            ViewBag.IdAplicacion = new SelectList(lstAplicaciones, "ID", "DESCRIPCION", "Selecciona una Aplicacion");
            return View();
        }

        public ActionResult ConsultaRoles(int IdAplicacion)
        {
            HelperTools hHelp = new HelperTools();
            List<CatalogosBE> lstRoles = new List<CatalogosBE>();
            lstRoles = hHelp.SetDdlCatalogos("14", IdAplicacion.ToString());
            return Json(lstRoles);
        }

        public ActionResult ConsultaMenu(string IdAplicacion, string IdRol)
        {
            SECURITYWCF.SecurityServiceClient security = new SECURITYWCF.SecurityServiceClient();
            SECURITYWCF.SecutityDC resSecurity = new SECURITYWCF.SecutityDC();

            resSecurity = security.getMenuxRolAdmin(long.Parse(IdRol), long.Parse(IdAplicacion), long.Parse(ResourceApp.IdApp), ResourceApp.Password);
            List<SECURITYWCF.PermisosXMenuBE> oMenuLista = new List<SECURITYWCF.PermisosXMenuBE>();
            oMenuLista = resSecurity.PermisosXMenu.ToList();
           

            return Json(oMenuLista);
        }

        public ActionResult MenuSubmenus(string IDMENU)
        {
            SECURITYWCF.SecurityServiceClient security = new SECURITYWCF.SecurityServiceClient();
            SECURITYWCF.SecutityDC resSecurity = new SECURITYWCF.SecutityDC();

            resSecurity = security.getSubMenuXIdMenu(long.Parse(IDMENU), long.Parse(ResourceApp.IdApp), ResourceApp.Password);
            List<SECURITYWCF.PermisoXSubmenuBE> oSubMenuLista = new List<SECURITYWCF.PermisoXSubmenuBE>();
            oSubMenuLista = resSecurity.PermisosXSubmenu.ToList();
            return View(oSubMenuLista);
        }

        public ActionResult MenuView(string IdAplicacion, string IdRol)
        {
            SECURITYWCF.SecurityServiceClient security = new SECURITYWCF.SecurityServiceClient();
            SECURITYWCF.SecutityDC resSecurity = new SECURITYWCF.SecutityDC();

            resSecurity = security.getMenuxRolAdmin(long.Parse(IdRol), long.Parse(IdAplicacion), long.Parse(ResourceApp.IdApp), ResourceApp.Password);
            List<SECURITYWCF.PermisosXMenuBE> oMenuLista = new List<SECURITYWCF.PermisosXMenuBE>();
            oMenuLista = resSecurity.PermisosXMenu.ToList();
            

            return PartialView("MenuView",oMenuLista);
        }

        [HttpPost]
        public ActionResult AddMenu(string IdRol,string IdApp, string Menu, string Img, string Obj,string Url, string Tool, string Orden)
        {
            USERSECURITYWCF.UsuariosBE itemSecurity = new USERSECURITYWCF.UsuariosBE();
            itemSecurity = (USERSECURITYWCF.UsuariosBE)Session["USER_SESSION"];

            StringBuilder strMensaje = new StringBuilder();
            int id = 0;
            bool success = false;
            SECURITYWCF.SecurityServiceClient seguridad = new SECURITYWCF.SecurityServiceClient();
            SECURITYWCF.SecutityDC resSeguridad = new SECURITYWCF.SecutityDC();
          
            resSeguridad = seguridad.addMenuxAppRol(long.Parse(IdRol), long.Parse(IdApp),Menu,Img,Obj,Url,Tool, long.Parse(Orden), long.Parse(ResourceApp.IdApp), ResourceApp.Password);

            if (resSeguridad.ResGral.FLAG)
            {
                success = true;
                id = 1;
                strMensaje.Append("Se agrego el Menu.");
            }
            else
            {
                success = false;
                strMensaje.Append("Ups!, Algo salio mal.");
                strMensaje.Append("<br/>");
                strMensaje.Append("Error: ");
                strMensaje.Append(resSeguridad.ResGral.ERRORMESSAGE);

            }





            return Json(new Response { IsSuccess = success, Message = strMensaje.ToString(), Id = id, RedirectTo="Roles" }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult AddRol(string IdApp, string Rol)
        {
            USERSECURITYWCF.UsuariosBE itemSecurity = new USERSECURITYWCF.UsuariosBE();
            itemSecurity = (USERSECURITYWCF.UsuariosBE)Session["USER_SESSION"];

            StringBuilder strMensaje = new StringBuilder();
            int id = 0;
            bool success = false;
            SECURITYWCF.SecurityServiceClient seguridad = new SECURITYWCF.SecurityServiceClient();
            SECURITYWCF.SecutityDC resSeguridad = new SECURITYWCF.SecutityDC();

            resSeguridad = seguridad.addRolxApp(Rol,long.Parse(IdApp), long.Parse(ResourceApp.IdApp), ResourceApp.Password);

            if (resSeguridad.ResGral.FLAG)
            {
                success = true;
                id = 1;
                strMensaje.Append("Se agrego el Rol.");
            }
            else
            {
                success = false;
                strMensaje.Append("Ups!, Algo salio mal.");
                strMensaje.Append("<br/>");
                strMensaje.Append("Error: ");
                strMensaje.Append(resSeguridad.ResGral.ERRORMESSAGE);

            }
            return Json(new Response { IsSuccess = success, Message = strMensaje.ToString(), Id = id, RedirectTo = "Roles" }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Create(string IdAplicacion)
        {
            ViewBag.IdAplicacion = IdAplicacion;
            return View();
        }

        public ActionResult CreateMenu(string IdAplicacion, string IdRol)
        {
            ViewBag.IdAplicacion = IdAplicacion;
            ViewBag.IdRol = IdRol;
            return View();
        }

        public ActionResult CreateSubMenu(string IdAplicacion, string IdMenu)
        {
            ViewBag.IdAplicacion = IdAplicacion;
            ViewBag.IdMenu = IdMenu;
            return View();
        }

        public ActionResult EditMenu(string IdAplicacion, string IdRol, string IdMenu)
        {
            SECURITYWCF.SecurityServiceClient security = new SECURITYWCF.SecurityServiceClient();
            SECURITYWCF.SecutityDC resSecurity = new SECURITYWCF.SecutityDC();

            resSecurity = security.getMenuxRol(long.Parse(IdRol), long.Parse(IdAplicacion), long.Parse(ResourceApp.IdApp), ResourceApp.Password);
            SECURITYWCF.PermisosXMenuBE oMenu = new SECURITYWCF.PermisosXMenuBE();
            oMenu = resSecurity.PermisosXMenu.Where(x => x.IDPERMISOSMENU == long.Parse(IdMenu)).FirstOrDefault();
            return View(oMenu);
        }

        public ActionResult DelMenu(string IdAplicacion, string IdRol, string IdMenu)
        {
            SECURITYWCF.SecurityServiceClient security = new SECURITYWCF.SecurityServiceClient();
            SECURITYWCF.SecutityDC resSecurity = new SECURITYWCF.SecutityDC();

            resSecurity = security.getMenuxRol(long.Parse(IdRol), long.Parse(IdAplicacion), long.Parse(ResourceApp.IdApp), ResourceApp.Password);
            SECURITYWCF.PermisosXMenuBE oMenu = new SECURITYWCF.PermisosXMenuBE();

            oMenu = resSecurity.PermisosXMenu.Where(x => x.IDPERMISOSMENU == long.Parse(IdMenu)).FirstOrDefault();
            return View(oMenu);
        }

        public ActionResult EditSubMenu(string IdAplicacion, string IdSubMenu, string IdMenu)
        {
            SECURITYWCF.SecurityServiceClient security = new SECURITYWCF.SecurityServiceClient();
            SECURITYWCF.SecutityDC resSecurity = new SECURITYWCF.SecutityDC();

            resSecurity = security.getSubMenuXIdMenu(long.Parse(IdMenu), long.Parse(ResourceApp.IdApp), ResourceApp.Password);
            SECURITYWCF.PermisoXSubmenuBE oSubMenu = new SECURITYWCF.PermisoXSubmenuBE();
            oSubMenu = resSecurity.PermisosXSubmenu.Where(x => x.IDPERMISOSXSUBMENU == long.Parse(IdSubMenu)).FirstOrDefault();
            return View(oSubMenu);
        }

        public ActionResult DelSubMenu(string IdAplicacion, string IdSubMenu, string IdMenu)
        {
            SECURITYWCF.SecurityServiceClient security = new SECURITYWCF.SecurityServiceClient();
            SECURITYWCF.SecutityDC resSecurity = new SECURITYWCF.SecutityDC();

            resSecurity = security.getSubMenuXIdMenu(long.Parse(IdMenu), long.Parse(ResourceApp.IdApp), ResourceApp.Password);
            SECURITYWCF.PermisoXSubmenuBE oSubMenu = new SECURITYWCF.PermisoXSubmenuBE();
            oSubMenu = resSecurity.PermisosXSubmenu.Where(x => x.IDPERMISOSXSUBMENU == long.Parse(IdSubMenu)).FirstOrDefault();
            return View(oSubMenu);
        }

        [HttpPost]
        public ActionResult DelSubMenu(SECURITYWCF.PermisoXSubmenuBE eSubMenu)
        {
            USERSECURITYWCF.UsuariosBE itemSecurity = new USERSECURITYWCF.UsuariosBE();
            itemSecurity = (USERSECURITYWCF.UsuariosBE)Session["USER_SESSION"];

            StringBuilder strMensaje = new StringBuilder();
            int id = 0;
            bool success = false;
            SECURITYWCF.SecurityServiceClient seguridad = new SECURITYWCF.SecurityServiceClient();
            SECURITYWCF.SecutityDC resSeguridad = new SECURITYWCF.SecutityDC();

            resSeguridad = seguridad.delSubMenu(eSubMenu.IDPERMISOSXSUBMENU, long.Parse(ResourceApp.IdApp), ResourceApp.Password);

            if (resSeguridad.ResGral.FLAG)
            {
                success = true;
                id = 1;
                strMensaje.Append("Se elimino el Sub Menu.");
            }
            else
            {
                success = false;
                strMensaje.Append("Ups!, Algo salio mal.");
                strMensaje.Append("<br/>");
                strMensaje.Append("Error: ");
                strMensaje.Append(resSeguridad.ResGral.ERRORMESSAGE);

            }





            return Json(new Response { IsSuccess = success, Message = strMensaje.ToString(), Id = id, RedirectTo = "Roles" }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public ActionResult EditMenu(SECURITYWCF.PermisosXMenuBE eMenu)
        {
            USERSECURITYWCF.UsuariosBE itemSecurity = new USERSECURITYWCF.UsuariosBE();
            itemSecurity = (USERSECURITYWCF.UsuariosBE)Session["USER_SESSION"];

            StringBuilder strMensaje = new StringBuilder();
            int id = 0;
            bool success = false;
            SECURITYWCF.SecurityServiceClient seguridad = new SECURITYWCF.SecurityServiceClient();
            SECURITYWCF.SecutityDC resSeguridad = new SECURITYWCF.SecutityDC();

            resSeguridad = seguridad.updMenuxAppRol(eMenu, long.Parse(ResourceApp.IdApp), ResourceApp.Password);

            if (resSeguridad.ResGral.FLAG)
            {
                success = true;
                id = 1;
                strMensaje.Append("Se agrego el Menu.");
            }
            else
            {
                success = false;
                strMensaje.Append("Ups!, Algo salio mal.");
                strMensaje.Append("<br/>");
                strMensaje.Append("Error: ");
                strMensaje.Append(resSeguridad.ResGral.ERRORMESSAGE);

            }





            return Json(new Response { IsSuccess = success, Message = strMensaje.ToString(), Id = id, RedirectTo = "Roles" }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult DelMenu(SECURITYWCF.PermisosXMenuBE eMenu)
        {
            USERSECURITYWCF.UsuariosBE itemSecurity = new USERSECURITYWCF.UsuariosBE();
            itemSecurity = (USERSECURITYWCF.UsuariosBE)Session["USER_SESSION"];

            StringBuilder strMensaje = new StringBuilder();
            int id = 0;
            bool success = false;
            SECURITYWCF.SecurityServiceClient seguridad = new SECURITYWCF.SecurityServiceClient();
            SECURITYWCF.SecutityDC resSeguridad = new SECURITYWCF.SecutityDC();

            resSeguridad = seguridad.delMenu(eMenu.IDPERMISOSMENU, long.Parse(ResourceApp.IdApp), ResourceApp.Password);

            if (resSeguridad.ResGral.FLAG)
            {
                success = true;
                id = 1;
                strMensaje.Append("Se elimino el Menu.");
            }
            else
            {
                success = false;
                strMensaje.Append("Ups!, Algo salio mal.");
                strMensaje.Append("<br/>");
                strMensaje.Append("Error: ");
                strMensaje.Append(resSeguridad.ResGral.ERRORMESSAGE);

            }





            return Json(new Response { IsSuccess = success, Message = strMensaje.ToString(), Id = id, RedirectTo = "Roles" }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult EditSubMenu(SECURITYWCF.PermisoXSubmenuBE eSubMenu)
        {
            USERSECURITYWCF.UsuariosBE itemSecurity = new USERSECURITYWCF.UsuariosBE();
            itemSecurity = (USERSECURITYWCF.UsuariosBE)Session["USER_SESSION"];

            StringBuilder strMensaje = new StringBuilder();
            int id = 0;
            bool success = false;
            SECURITYWCF.SecurityServiceClient seguridad = new SECURITYWCF.SecurityServiceClient();
            SECURITYWCF.SecutityDC resSeguridad = new SECURITYWCF.SecutityDC();

            resSeguridad = seguridad.updSubMenuxAppRol(eSubMenu, long.Parse(ResourceApp.IdApp), ResourceApp.Password);

            if (resSeguridad.ResGral.FLAG)
            {
                success = true;
                id = 1;
                strMensaje.Append("Se actualizo el Sub Menu.");
            }
            else
            {
                success = false;
                strMensaje.Append("Ups!, Algo salio mal.");
                strMensaje.Append("<br/>");
                strMensaje.Append("Error: ");
                strMensaje.Append(resSeguridad.ResGral.ERRORMESSAGE);

            }





            return Json(new Response { IsSuccess = success, Message = strMensaje.ToString(), Id = id, RedirectTo = "Roles" }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult AddSubMenu(string IdMenu, string SubMenu, string Img, string Obj, string Url, string Tool, string Orden)
        {
            USERSECURITYWCF.UsuariosBE itemSecurity = new USERSECURITYWCF.UsuariosBE();
            itemSecurity = (USERSECURITYWCF.UsuariosBE)Session["USER_SESSION"];

            StringBuilder strMensaje = new StringBuilder();
            int id = 0;
            bool success = false;
            SECURITYWCF.SecurityServiceClient seguridad = new SECURITYWCF.SecurityServiceClient();
            SECURITYWCF.SecutityDC resSeguridad = new SECURITYWCF.SecutityDC();

            resSeguridad = seguridad.addSubMenuxAppRol(long.Parse(IdMenu), SubMenu, Img, Obj, Url, Tool, long.Parse(Orden), long.Parse(ResourceApp.IdApp), ResourceApp.Password);

            if (resSeguridad.ResGral.FLAG)
            {
                success = true;
                id = 1;
                strMensaje.Append("Se agrego el Sub Menu.");
            }
            else
            {
                success = false;
                strMensaje.Append("Ups!, Algo salio mal.");
                strMensaje.Append("<br/>");
                strMensaje.Append("Error: ");
                strMensaje.Append(resSeguridad.ResGral.ERRORMESSAGE);
            }
            return Json(new Response { IsSuccess = success, Message = strMensaje.ToString(), Id = id, RedirectTo = "Roles" }, JsonRequestBehavior.AllowGet);
        }
    }
}