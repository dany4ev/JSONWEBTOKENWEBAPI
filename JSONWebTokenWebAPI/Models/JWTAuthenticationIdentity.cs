using System.Security.Principal;

namespace JSONWebTokenWebAPI.Models
{
    public class JWTAuthenticationIdentity : GenericIdentity
    {

        public string UserName { get; set; }
        public string UserId { get; set; }

        public JWTAuthenticationIdentity(string userName)
            : base(userName)
        {
            UserName = userName;
        }
    }
}
