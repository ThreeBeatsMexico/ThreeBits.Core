using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ThreeBits.Security.Portal.COMMONWCF;
using ThreeBits.Security.Portal.Helpers;
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
            //List<CatalogosBE> lstEdoCivil = new List<CatalogosBE>();
            //lstEdoCivil = hHelp.SetDdlCatalogos("5");

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





    }
}