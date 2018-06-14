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
            using (UsersDbEntities entities = new UsersDbEntities())
            {
                return entities.Users.ToList();
            }
        }

        public User Get(int id)
        {
            using (UsersDbEntities entities = new UsersDbEntities())
            {
                return entities.Users.FirstOrDefault<User>(e => e.UserID == id);
            }
        }

        public HttpResponseMessage Post([FromBody] User user)
        {
            try
            {
                using (UsersDbEntities entities = new UsersDbEntities())
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

        public HttpResponseMessage Delete(int id)
        {
            try
            {
                using (UsersDbEntities entities = new UsersDbEntities())
                {
                    var entity = entities.Users.FirstOrDefault(e => e.UserID == id);
                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "User with UserID = " + id + " is not found.");
                    }
                    else
                    {
                        entities.Users.Remove(entity);
                        entities.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK);
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        public HttpResponseMessage Delete(int id, [FromBody]User user)
        {
            try
            {
                using (UsersDbEntities entities = new UsersDbEntities())
                {
                    var entity = entities.Users.FirstOrDefault(e => e.UserID == id);
                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "User with UserID = " + id + " not found to update.");
                    }
                    else
                    {
                        entity.LoginID = user.LoginID;
                        entity.FirstName = user.FirstName;
                        entity.LastName = user.LastName;
                        entity.CheckFirstTimeLogin = user.CheckFirstTimeLogin;

                        entities.SaveChanges();

                        return Request.CreateResponse(HttpStatusCode.OK, entity);
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}
