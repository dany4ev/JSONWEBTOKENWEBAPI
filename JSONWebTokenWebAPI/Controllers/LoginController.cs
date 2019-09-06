using JSONWebTokenWebAPI.Models;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace JSONWebTokenWebAPI.Controllers
{
    public class LoginController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage LoginDemo(LoginProfile loginProfile)
        {

            MockAuthenticationService demoService = new MockAuthenticationService();
            UserProfile user = demoService.GetUser(loginProfile.Username, loginProfile.Password);
            if (user == null)
            {
                return Request.CreateResponse(HttpStatusCode.Unauthorized, "Invalid User", Configuration.Formatters.JsonFormatter);
            }
            else
            {

                AuthenticationModule authentication = new AuthenticationModule();
                string token = authentication.GenerateTokenForUser(user.UserName, user.UserId);
                return Request.CreateResponse(HttpStatusCode.OK, token, Configuration.Formatters.JsonFormatter);
            }

        }
    }
}
