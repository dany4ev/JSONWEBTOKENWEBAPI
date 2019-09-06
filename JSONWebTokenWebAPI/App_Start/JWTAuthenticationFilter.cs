using System.Web.Http.Filters;
using System.Web.Http.Controllers;
using System;
using System.IdentityModel.Tokens;
using System.Security.Principal;
using System.Threading;
using System.Net.Http;
using System.Net;
using JSONWebTokenWebAPI.Models;

namespace JSONWebTokenWebAPI
{
    public class JWTAuthenticationFilter : AuthorizationFilterAttribute
    {

        public override void OnAuthorization(HttpActionContext filterContext)
        {
            if (!IsUserAuthorized(filterContext))
            {
                ShowAuthenticationError(filterContext);
                return;
            }

            base.OnAuthorization(filterContext);
        }

        private static void ShowAuthenticationError(HttpActionContext filterContext)
        {
            var responseDTO = new ResponseDTO() { Code = 401, Message = "Unable to access, Please login again" };
            filterContext.Response =
            filterContext.Request.CreateResponse(HttpStatusCode.Unauthorized, responseDTO);
        }

        public bool IsUserAuthorized(HttpActionContext actionContext)
        {
            var authHeader = FetchFromHeader(actionContext); // fetch authorization token from header


            if (authHeader != null)
            {
                var auth = new AuthenticationModule();
                JwtSecurityToken userPayloadToken = auth.GenerateUserClaimFromJWT(authHeader);

                if (userPayloadToken != null)
                {

                    JWTAuthenticationIdentity identity = auth.PopulateUserIdentity(userPayloadToken);
                    string[] roles = { "All" };
                    var genericPrincipal = new GenericPrincipal(identity, roles);
                    Thread.CurrentPrincipal = genericPrincipal;
                    var authenticationIdentity = Thread.CurrentPrincipal.Identity as JWTAuthenticationIdentity;
                    if (authenticationIdentity != null && !String.IsNullOrEmpty(authenticationIdentity.UserName))
                    {
                        authenticationIdentity.UserId = identity.UserId;
                        authenticationIdentity.UserName = identity.UserName;
                    }
                    return true;
                }

            }
            return false;


        }

        private string FetchFromHeader(HttpActionContext actionContext)
        {
            string requestToken = null;

            var authRequest = actionContext.Request.Headers.Authorization;
            if (authRequest != null)
            {
                requestToken = authRequest.Parameter;
            }

            return requestToken;
        }
    }
}
