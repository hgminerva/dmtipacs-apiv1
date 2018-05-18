using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace dmtipacs_api.ApiControllers
{
    [Authorize, RoutePrefix("api/user")]
    public class ApiMstUserController : ApiController
    {
        // ============
        // Data Context
        // ============
        private Data.dmtipacsdbDataContext db = new Data.dmtipacsdbDataContext();

        // ===========
        // List - User
        // ===========
        [HttpGet, Route("list")]
        public List<Entities.MstUser> ListUser()
        {
            var users = from d in db.MstUsers
                        select new Entities.MstUser
                        {
                            Id = d.Id,
                            UserName = d.UserName,
                            FullName = d.FullName,
                            Address = d.Address,
                            ContactNumber = d.ContactNumber,
                            UserTypeId = d.UserTypeId,
                            AspNetUserId = d.AspNetUserId
                        };

            return users.ToList();
        }

        // ==========
        // Add - User
        // ==========
        [HttpPost, Route("add")]
        public HttpResponseMessage AddUser(Entities.MstUser objUser)
        {
            try
            {
                Data.MstUser newUser = new Data.MstUser
                {
                    UserName = objUser.UserName,
                    FullName = objUser.FullName,
                    Address = objUser.Address,
                    ContactNumber = objUser.ContactNumber,
                    UserTypeId = objUser.UserTypeId,
                    AspNetUserId = objUser.AspNetUserId
                };

                db.MstUsers.InsertOnSubmit(newUser);
                db.SubmitChanges();

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }

        // =============
        // Update - User
        // =============
        [HttpPut, Route("update/{id}")]
        public HttpResponseMessage UpdateUser(String id, Entities.MstUser objUser)
        {
            try
            {
                var users = from d in db.MstUsers
                            where d.Id == Convert.ToInt32(id)
                            select d;

                if (users.Any())
                {
                    var updateUser = users.FirstOrDefault();
                    updateUser.UserName = objUser.UserName;
                    updateUser.FullName = objUser.FullName;
                    updateUser.Address = objUser.Address;
                    updateUser.ContactNumber = objUser.ContactNumber;
                    updateUser.UserTypeId = objUser.UserTypeId;
                    updateUser.AspNetUserId = objUser.AspNetUserId;

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

        // =============
        // Delete - User
        // =============
        [HttpDelete, Route("delete/{id}")]
        public HttpResponseMessage DeleteUser(String id)
        {
            try
            {
                var users = from d in db.MstUsers
                            where d.Id == Convert.ToInt32(id)
                            select d;

                if (users.Any())
                {
                    db.MstUsers.DeleteOnSubmit(users.First());
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