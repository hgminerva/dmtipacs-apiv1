using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.AspNet.Identity;

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
                            UserType = d.MstUserType.UserType,
                            AspNetUserId = d.AspNetUserId
                        };

            return users.ToList();
        }

        // ========================
        // List By User Type - User
        // ========================
        [HttpGet, Route("list/byUserType/{userTypeId}")]
        public List<Entities.MstUser> ListUserByUserType(String userTypeId)
        {
            var users = from d in db.MstUsers
                        where d.UserTypeId == Convert.ToInt32(userTypeId)
                        select new Entities.MstUser
                        {
                            Id = d.Id,
                            UserName = d.UserName,
                            FullName = d.FullName,
                            Address = d.Address,
                            ContactNumber = d.ContactNumber,
                            UserTypeId = d.UserTypeId,
                            UserType = d.MstUserType.UserType,
                            AspNetUserId = d.AspNetUserId
                        };

            return users.ToList();
        }

        // =============
        // Detail - User
        // =============
        [HttpGet, Route("detail/{id}")]
        public Entities.MstUser DetailUser(String id)
        {
            var user = from d in db.MstUsers
                       where d.Id == Convert.ToUInt32(id)
                       select new Entities.MstUser
                       {
                           Id = d.Id,
                           UserName = d.UserName,
                           FullName = d.FullName,
                           Address = d.Address,
                           ContactNumber = d.ContactNumber,
                           UserTypeId = d.UserTypeId,
                           UserType = d.MstUserType.UserType,
                           AspNetUserId = d.AspNetUserId
                       };

            return user.FirstOrDefault();
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
                    updateUser.FullName = objUser.FullName;
                    updateUser.Address = objUser.Address;
                    updateUser.ContactNumber = objUser.ContactNumber;
                    updateUser.UserTypeId = objUser.UserTypeId;

                    db.SubmitChanges();

                    return Request.CreateResponse(HttpStatusCode.OK);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }

        // ==============
        // Current - User
        // ==============
        [HttpGet, Route("current")]
        public Entities.MstUser CurrentUser()
        {
            var currentUser = from d in db.MstUsers
                              where d.AspNetUserId == User.Identity.GetUserId()
                              select d;

            var currentUserId = currentUser.FirstOrDefault().Id;

            var user = from d in db.MstUsers
                       where d.Id == Convert.ToUInt32(currentUserId)
                       select new Entities.MstUser
                       {
                           Id = d.Id,
                           UserName = d.UserName,
                           FullName = d.FullName,
                           Address = d.Address,
                           ContactNumber = d.ContactNumber,
                           UserTypeId = d.UserTypeId,
                           UserType = d.MstUserType.UserType,
                           AspNetUserId = d.AspNetUserId
                       };

            return user.FirstOrDefault();
        }
    }
}