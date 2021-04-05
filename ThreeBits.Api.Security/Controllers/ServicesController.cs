using LatinoSeguros.Bussines.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Web.Http;
using ThreeBits.Business.Security;
using ThreeBits.Entities.Common;
using ThreeBits.Entities.Security;

namespace ThreeBits.Api.Security.Controllers
{
    [RoutePrefix("v1/api")]
    public class ServicesController : ApiController
    {
        private string ServiceNameClass = MethodBase.GetCurrentMethod().DeclaringType.Name;

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
        public ProcessResult encryptDecryptChain(EncryptionBE request)
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
                    Respuesta.data = oSecurityRes.Encriptacion = oSecBr.encryptDecryptChain(request.TIPO, request.VALORIN, request.LLAVE, oApp.IDAPLICACION);
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


        [AllowAnonymous]
        [Route("Registry")]
        [AcceptVerbs("POST")]
        public ProcessResult Registry(Registry oRegistry)
        {
            ProcessResult oRes = new ProcessResult();
            Utils oUtils = new Utils();
            SecurityBR oSecBr = new SecurityBR();
            AplicacionBE oApp = new AplicacionBE();
            FovisssteBR oFovissste = new FovisssteBR();

            try
            {
                var re = Request;
                var headers = re.Headers;
                var xAppId = re.Headers.GetValues("XAPPID").First();

                oApp = oSecBr.getAppInfo(xAppId.ToString());

                if (xAppId == null || xAppId == "")
                {

                    oRes.flag = false;
                    oRes.errorMessage = "Missing XAPPID Header";



                }
                else
                {
                    oRegistry.xAppId = xAppId;
                    oRes = oUtils.ValidaDatosFov(oRegistry.rfc, oRegistry.email, oRegistry.password, oRegistry.cpassword, false, true);
                    if (oRes.flag)
                        oRes = oFovissste.VerificaIdentidad(oRegistry);
                }
            }
            catch (Exception ex)
            {
                oRes.flag = false;
                oRes.errorMessage = ex.Message;
            }

            return oRes;


        }





    }
}
