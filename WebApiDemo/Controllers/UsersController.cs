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

        public HttpResponseMessage Delete(int id)
        {
            try
            {
                using (ProdArcEWIEntities entities = new ProdArcEWIEntities())
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
                using (ProdArcEWIEntities entities = new ProdArcEWIEntities())
                {
                    var entity = entities.Users.FirstOrDefault(e => e.UserID == id);
                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "User with UserID = " + id + " not found to update.");
                    }
                    else
                    {
                        entity.AccessLevel = user.AccessLevel;
                        entity.Agency = user.Agency;
                        entity.AgencyActive = user.AgencyActive;
                        entity.BadgeNo = user.BadgeNo;
                        entity.CellPhone = user.CellPhone;
                        entity.CheckFirstTimeLogin = user.CheckFirstTimeLogin;
                        entity.City = user.City;
                        entity.CreatedBy = user.CreatedBy;
                        entity.CreatedDate = user.CreatedDate;
                        entity.Division = user.Division;
                        entity.Email = user.Email;
                        entity.EntryDate = user.EntryDate;
                        entity.FirstName = user.FirstName;
                        entity.HomePhone = user.HomePhone;
                        entity.JacketNo = user.JacketNo;
                        entity.LastName = user.LastName;
                        entity.LoginID = user.LoginID;
                        entity.MiddleName = user.MiddleName;
                        entity.ModifiedBy = user.ModifiedBy;
                        entity.ModifiedDate = user.ModifiedDate;
                        entity.OfficialTitle = user.OfficialTitle;
                        entity.Password = user.Password;
                        entity.PasswordExpiryDate = user.PasswordExpiryDate;
                        entity.State = user.State;
                        entity.Street = user.Street;
                        entity.UserActive = user.UserActive;
                        entity.UserRoleID = user.UserRoleID;
                        entity.WarrantLocation = user.WarrantLocation;
                        entity.WorkPhone = user.WorkPhone;
                        entity.Zip = user.Zip;
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
