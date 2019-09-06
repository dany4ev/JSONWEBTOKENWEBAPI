using JSONWebTokenWebAPI.Models;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;

namespace JSONWebTokenWebAPI.Controllers
{
    [JWTAuthenticationFilter]
    public class DashboardController : BaseController
    {

        public HttpResponseMessage Get()
        {

            var listOfbooks = GetAllBooks();

            return Request.CreateResponse(HttpStatusCode.OK, listOfbooks);
        }

        private List<Books> GetAllBooks()
        {
            List<Books> book = new List<Books>();
            book.Add(new Books { Id = 1, Name = "ABC Books" });
            book.Add(new Books { Id = 2, Name = "XYZ Books" });
            book.Add(new Books { Id = 3, Name = "DEF Books" });
            return book;
        }
    }
}
