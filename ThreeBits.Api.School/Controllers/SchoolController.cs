using LatinoSeguros.Bussines.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ThreeBits.Business.Security;
using ThreeBits.Entities.Common;
using ThreeBits.Entities.Security;

namespace ThreeBits.Api.School.Controllers
{
    public class SchoolController : ApiController
    {

        //[JwtAuthentication]
        [Route("getAlumnos")]
        [AcceptVerbs("GET")]
        public ProcessResult getAlumnos()
        {
            ProcessResult Respuesta = new ProcessResult();
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
                    //Respuesta.data = ThreeBits.Business.;
                }
            }
            catch (Exception ex)
            {
                Respuesta.flag = false;

                Respuesta.errorMessage = ex.Message;
            }
            return Respuesta;
        }

        //[JwtAuthentication]
        [Route("postAlumnos")]
        [AcceptVerbs("POST")]
        public ProcessResult postAlumnos()
        {
            ProcessResult Respuesta = new ProcessResult();
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
                    //Respuesta.data = ThreeBits.Business.;
                }
            }
            catch (Exception ex)
            {
                Respuesta.flag = false;

                Respuesta.errorMessage = ex.Message;
            }
            return Respuesta;
        }

        //[JwtAuthentication]
        [Route("putAlumnos")]
        [AcceptVerbs("PUT")]
        public ProcessResult putAlumnos()
        {
            ProcessResult Respuesta = new ProcessResult();
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
                    //Respuesta.data = ThreeBits.Business.;
                }
            }
            catch (Exception ex)
            {
                Respuesta.flag = false;

                Respuesta.errorMessage = ex.Message;
            }
            return Respuesta;
        }
        //[JwtAuthentication]
        [Route("deleteAlumnos")]
        [AcceptVerbs("DELETE")]
        public ProcessResult deleteAlumnos()
        {
            ProcessResult Respuesta = new ProcessResult();
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
                    //Respuesta.data = ThreeBits.Business.;
                }
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
