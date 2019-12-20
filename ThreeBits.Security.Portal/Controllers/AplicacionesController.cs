using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using ThreeBits.Security.Portal.Models;
using ThreeBits.Security.Portal.Properties;
using ThreeBits.Security.Portal.SECURITYWCF;
using ThreeBits.Security.Portal.USERSECURITYWCF;

namespace ThreeBits.Security.Portal.Controllers
{
    public class AplicacionesController : Controller
    {
        // GET: Aplicaciones
        public ActionResult Index()
        {
            if (Session["USER_SESSION"] != null)
            {

                string idApp = string.Empty;
                SecurityServiceClient seguridad = new SecurityServiceClient();
                SecutityDC resSeguridad = new SecutityDC();
                resSeguridad = seguridad.getAplicaciones(idApp, string.Empty, long.Parse(ResourceApp.IdApp), ResourceApp.Password);
                List<SECURITYWCF.AplicacionBE> oAplicacionesLista = new List<SECURITYWCF.AplicacionBE>();
                oAplicacionesLista = resSeguridad.Aplicaciones.ToList();
                ViewBag.lstAplicaciones = oAplicacionesLista;
                ViewBag.NumAplicaciones = oAplicacionesLista.Count();

            }
            else
            {
                RedirectToAction("Login", "Account");
            }

            return View();
        }


        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(string sAplicacion, string sPassword, string sUrlInicio)
        {
            UsuariosBE itemSecurity = new UsuariosBE();
            itemSecurity = (UsuariosBE)Session["USER_SESSION"];

            StringBuilder strMensaje = new StringBuilder();
            int id = 0;
            bool success = false;
            SecurityServiceClient seguridad = new SecurityServiceClient();
            SecutityDC resSeguridad = new SecutityDC();
            SECURITYWCF.AplicacionBE oAplicacion = new SECURITYWCF.AplicacionBE();

            resSeguridad = seguridad.encryptDesEncrypt(sPassword, 1, long.Parse(ResourceApp.IdApp), ResourceApp.Password);



            oAplicacion.ACTIVO = true;
            oAplicacion.DESCRIPCION = sAplicacion;
            oAplicacion.PASSWORD = resSeguridad.Encriptacion.VALOROUT;
            oAplicacion.URLINICIO = sUrlInicio;
            oAplicacion.IDUSUARIO = itemSecurity.IDUSUARIO;

            resSeguridad = seguridad.addAplicacion(oAplicacion, long.Parse(ResourceApp.IdApp), ResourceApp.Password);

            if (resSeguridad.ResGral.FLAG)
            {
                success = true;
                id = 1;
                strMensaje.Append("Se agrego la aplicacion correctamente.");
            }
            else
            {
                success = false;
                strMensaje.Append("Ups!, Algo salio mal.");
                strMensaje.Append("<br/>");
                strMensaje.Append("Error: ");
                strMensaje.Append(resSeguridad.ResGral.ERRORMESSAGE);

            }

           



            return Json(new Response { IsSuccess = success, Message = strMensaje.ToString(), Id = id, RedirectTo="Aplicaciones" }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Edit(string id)
        {
            SecurityServiceClient seguridad = new SecurityServiceClient();
            SecutityDC resSeguridad = new SecutityDC();
            SECURITYWCF.AplicacionBE oAplicacion = new SECURITYWCF.AplicacionBE();
            resSeguridad = seguridad.getAplicaciones(id,"", long.Parse(ResourceApp.IdApp), ResourceApp.Password);
            oAplicacion = resSeguridad.Aplicaciones.FirstOrDefault();
            resSeguridad = seguridad.encryptDesEncrypt(oAplicacion.PASSWORD, 2, long.Parse(ResourceApp.IdApp), ResourceApp.Password);

            oAplicacion.PASSWORD = resSeguridad.Encriptacion.VALOROUT;

            return View(oAplicacion);
        }

        [HttpPost]
        public ActionResult Edit(string IDAPLICACION, string DESCRIPCION, string sPasswordApp, string chkActivo)
        {
            UsuariosBE itemSecurity = new UsuariosBE();
            itemSecurity = (UsuariosBE)Session["USER_SESSION"];

            StringBuilder strMensaje = new StringBuilder();
            int id = 0;
            bool success = false;
            SecurityServiceClient seguridad = new SecurityServiceClient();
            SecutityDC resSeguridad = new SecutityDC();
            SECURITYWCF.AplicacionBE oAplicacion = new SECURITYWCF.AplicacionBE();

            resSeguridad = seguridad.encryptDesEncrypt(sPasswordApp, 1, long.Parse(ResourceApp.IdApp), ResourceApp.Password);


            chkActivo = string.IsNullOrEmpty(chkActivo) ? "false" : "true";
            oAplicacion.ACTIVO = Convert.ToBoolean(chkActivo);
            oAplicacion.DESCRIPCION = DESCRIPCION;
            oAplicacion.PASSWORD = resSeguridad.Encriptacion.VALOROUT;
            //oAplicacion.URLINICIO = sUrlInicio;
            oAplicacion.IDUSUARIO = itemSecurity.IDUSUARIO;

            resSeguridad = seguridad.updAplicacion(oAplicacion, long.Parse(ResourceApp.IdApp), ResourceApp.Password);

            if (resSeguridad.ResGral.FLAG)
            {
                success = true;
                id = 1;
                strMensaje.Append("Se modifico la aplicacion correctamente.");
            }
            else
            {
                success = false;
                strMensaje.Append("Ups!, Algo salio mal.");
                strMensaje.Append("<br/>");
                strMensaje.Append("Error: ");
                strMensaje.Append(resSeguridad.ResGral.ERRORMESSAGE);

            }





            return Json(new Response { IsSuccess = success, Message = strMensaje.ToString(), Id = id }, JsonRequestBehavior.AllowGet);
        }

    }
}