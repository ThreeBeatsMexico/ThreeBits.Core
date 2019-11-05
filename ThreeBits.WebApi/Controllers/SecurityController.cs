using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Web.Http;
using ThreeBits.Business.Security;
using ThreeBits.Entities.Security;

namespace ThreeBits.WebApi.Controllers
{
    public class SecurityController : ApiController
    {
        private string ServiceNameClass = MethodBase.GetCurrentMethod().DeclaringType.Name;
        [HttpGet]
        //public SecurityDC checkPermisoXMethServ(string MethodName, string ServiceName, Int64 App, string PasswordApp)
        //{
        //    SecurityDC Respuesta = new SecurityDC();
        //    Respuesta.ResGral.FLAG = true;
        //    try
        //    {
        //        ////Checamos si tiene acceso al servicio y al metodo, en caso contrario mandamos un error controlado
        //        SecurityBR Seguridad = new SecurityBR();
        //        Seguridad.checkPermisoMethServ(App, PasswordApp, MethodName, ServiceName);
        //    }
        //    catch (Exception ex)
        //    {
        //        Respuesta.ResGral.FLAG = false;
        //        Respuesta.ResGral.TRACE = ex.StackTrace;
        //        Respuesta.ResGral.ERRORMESSAGE = ex.Message;
        //    }
        //    return Respuesta;
        //}





    }
}
