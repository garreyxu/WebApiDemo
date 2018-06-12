using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using UsersDataAccess;

namespace WebApiDemo.Controllers
{
    public class UsersController : ApiController
    {
        public IEnumerable<User> Get()
        {
            using (ProdArcEWIEntities entities = new ProdArcEWIEntities())
            {
                return entities.Users.ToList();
            }
        }

        public User Get(int id)
        {
            using (ProdArcEWIEntities entities = new ProdArcEWIEntities())
            {
                return entities.Users.FirstOrDefault<User>(e => e.UserID == id);
            }
        }

        public HttpResponseMessage Post([FromBody] User user)
        {
            try
            {
                using (ProdArcEWIEntities entities = new ProdArcEWIEntities())
                {
                    entities.Users.Add(user);
                    entities.SaveChanges();

                    var message = Request.CreateResponse(HttpStatusCode.Created, user);
                    message.Headers.Location = new Uri(Request.RequestUri + user.UserID.ToString());
                    return message;
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}
