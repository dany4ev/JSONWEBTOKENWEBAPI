using JSONWebTokenWebAPI.Models;
using System;

namespace JSONWebTokenWebAPI
{
    internal class MockAuthenticationService
    {
        public MockAuthenticationService()
        {
        }

        public UserProfile GetUser(string userName, string password)
        {
            if (userName == "danish" && password == "123")
            {
                return new UserProfile { UserId = new Guid().ToString(), UserName = "danish" };
            }

            return null;
        }
    }
}