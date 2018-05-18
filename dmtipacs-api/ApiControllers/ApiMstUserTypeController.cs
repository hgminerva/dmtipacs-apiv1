using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace dmtipacs_api.ApiControllers
{
    [Authorize, RoutePrefix("api/userType")]
    public class ApiMstUserTypeController : ApiController
    {
        // ============
        // Data Context
        // ============
        private Data.dmtipacsdbDataContext db = new Data.dmtipacsdbDataContext();

        // ================
        // List - User Type
        // ================
        [HttpGet, Route("list")]
        public List<Entities.MstUserType> ListUserType()
        {
            var userTypes = from d in db.MstUserTypes
                            select new Entities.MstUserType
                            {
                                Id = d.Id,
                                UserType = d.UserType
                            };

            return userTypes.ToList();
        }

        // ===============
        // Add - User Type
        // ===============
        [HttpPost, Route("add")]
        public HttpResponseMessage AddUserType(Entities.MstUserType objUserType)
        {
            try
            {
                Data.MstUserType newUserType = new Data.MstUserType
                {
                    UserType = objUserType.UserType
                };

                db.MstUserTypes.InsertOnSubmit(newUserType);
                db.SubmitChanges();

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }

        // ==================
        // Update - User Type
        // ==================
        [HttpPut, Route("update/{id}")]
        public HttpResponseMessage UpdateUserType(String id, Entities.MstUserType objUserType)
        {
            try
            {
                var userType = from d in db.MstUserTypes
                               where d.Id == Convert.ToInt32(id)
                               select d;

                if (userType.Any())
                {
                    var updateUserType = userType.FirstOrDefault();
                    updateUserType.UserType = objUserType.UserType;

                    db.SubmitChanges();

                    return Request.CreateResponse(HttpStatusCode.OK);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }

        // ==================
        // Delete - User Type
        // ==================
        [HttpDelete, Route("delete/{id}")]
        public HttpResponseMessage DeleteUserType(String id)
        {
            try
            {
                var userType = from d in db.MstUserTypes
                               where d.Id == Convert.ToInt32(id)
                               select d;

                if (userType.Any())
                {
                    db.MstUserTypes.DeleteOnSubmit(userType.First());
                    db.SubmitChanges();

                    return Request.CreateResponse(HttpStatusCode.OK);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }
    }
}