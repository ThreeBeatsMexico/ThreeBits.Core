using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Filters;
using ThreeBits.Business.Security;

namespace LatinoSeguros.Bussines.Filters
{
    public class JwtAuthenticationAttribute : Attribute, IAuthenticationFilter
    {
        public string Realm { get; set; }
        public bool AllowMultiple => false;

        public async Task AuthenticateAsync(HttpAuthenticationContext context, CancellationToken cancellationToken)
        {
            try
            {
                var request = context.Request;
                var authorization = request.Headers.Authorization;
                var xAppId = request.Headers.GetValues("XAPPID").First();

                if (xAppId == null || xAppId == "")
                {
                    context.ErrorResult = new AuthenticationFailureResult("Missing XAPPID header", request);
                    return;

                }


                if (authorization == null)
                {
                    context.ErrorResult = new AuthenticationFailureResult("Missing autorization header", request);
                    return;
                }

                if (authorization.Scheme != "Bearer")
                {
                    context.ErrorResult = new AuthenticationFailureResult("Bearer inexistente ", request);
                    return;
                }

                if (string.IsNullOrEmpty(authorization.Parameter))
                {
                    context.ErrorResult = new AuthenticationFailureResult("Jwt Token Perdido", request);
                    return;
                }

                var token = authorization.Parameter;
                var principal = await AuthenticateJwtToken(token,xAppId);

                if (principal == null)
                    context.ErrorResult = new AuthenticationFailureResult("Token no valido ", request);

                else
                    context.Principal = principal;
            }
            catch (Exception ex)
            {
                context.ErrorResult = new AuthenticationFailureResult("Exception: \n" + ex.Message, context.Request);
            }
        }

        private static bool ValidateToken(string token,string xAppId, out string username, out string name, out string userid, out string roleid)
        {
            username = null;
            name = null;
            userid = null;
            roleid = null;
                    
            try
            {
                var simplePrinciple = JwtManager.GetPrincipal(token, xAppId);
                var identity = simplePrinciple.Identity as ClaimsIdentity;

                if (identity == null)
                    return false;

                if (!identity.IsAuthenticated)
                    return false;

                var usernameClaim = identity.FindFirst(ClaimTypes.Name);
                username = usernameClaim?.Value;
                if (string.IsNullOrEmpty(username))
                    return false;

                var nameClaim = identity.FindFirst(ClaimTypes.UserData);
                name = nameClaim?.Value;
                if (string.IsNullOrEmpty(name))
                    return false;

                var userIdClaim = identity.FindFirst(ClaimTypes.NameIdentifier);
                userid = userIdClaim?.Value;
                if (string.IsNullOrEmpty(userid))
                    return false;

                var rolIdClaim = identity.FindFirst(ClaimTypes.Role);
                roleid = rolIdClaim?.Value;
                if (string.IsNullOrEmpty(roleid))
                    return false;
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected Task<IPrincipal> AuthenticateJwtToken(string token, string xAppId)
        {
            string username;
            string name;
            string userid;
            string roleid;

            try
            {
                if (ValidateToken(token,xAppId, out username, out name, out userid, out roleid))
                {
                    // based on username to get more information from database in order to build local identity
                    var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, username),
                    new Claim(ClaimTypes.UserData, name),
                    new Claim(ClaimTypes.NameIdentifier, userid),
                    new Claim(ClaimTypes.Role, roleid),
                };

                    var identity = new ClaimsIdentity(claims, "Jwt");
                    IPrincipal user = new ClaimsPrincipal(identity);

                    return Task.FromResult(user);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return Task.FromResult<IPrincipal>(null);
        }

        public Task ChallengeAsync(HttpAuthenticationChallengeContext context, CancellationToken cancellationToken)
        {
            Challenge(context);
            return Task.FromResult(0);
        }

        private void Challenge(HttpAuthenticationChallengeContext context)
        {
            string parameter = null;

            if (!string.IsNullOrEmpty(Realm))
                parameter = "realm=\"" + Realm + "\"";

            context.ChallengeWith("Bearer", parameter);
        }
    }
}
