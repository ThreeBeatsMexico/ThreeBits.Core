using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using ThreeBits.Business.Login;
using ThreeBits.Entities.Common;
using ThreeBits.Entities.Security;

namespace ThreeBits.Api.Security.Controllers
{
    [EnableCors("*", "*", "*")]
    [RoutePrefix("api/login")]
    public class LoginController : ApiController
    {
        // POST: api/Login
        [AllowAnonymous]
        public HttpResponseMessage Post(Credential value)
        {

            var re = Request;
            var headers = re.Headers;
            ProcessResult oRes = new ProcessResult();
            Login oLoginBr = new Login();
            try
            {


                var xAppId = re.Headers.GetValues("XAPPID").First();


                if (xAppId == null || xAppId == "")
                {

                    oRes.flag = false;
                    oRes.errorMessage = "Missing XAPPID Header";
                    return Request.CreateResponse(HttpStatusCode.Unauthorized, oRes);


                }
                else
                {
                    value.xAppId = xAppId;


                    oRes = oLoginBr.authenticate(value);

                    if (oRes.flag)
                    {
                        Credential oCred = (Credential)oRes.data;

                        oRes.data = oLoginBr.CreaToken(oCred);
                        return Request.CreateResponse(HttpStatusCode.OK, oRes);
                    }
                    else
                    {
                        oRes.flag = false;
                        oRes.errorMessage = "Usuario o password incorrectos";
                        return Request.CreateResponse(HttpStatusCode.Forbidden, oRes);
                    }
                }
            }
            catch (Exception e)
            {
                oRes.flag = false;
                oRes.errorMessage = e.Message;
                return Request.CreateResponse(HttpStatusCode.Conflict, oRes);
            }
        }
    }
}
