using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.AspNet.Identity;

namespace dmtipacs_api.ApiControllers
{
    [Authorize, RoutePrefix("api/facility")]
    public class ApiMstFacilityController : ApiController
    {
        // ============
        // Data Context
        // ============
        private Data.dmtipacsdbDataContext db = new Data.dmtipacsdbDataContext();

        // ===============
        // List - Facility
        // ===============
        [HttpGet, Route("list")]
        public List<Entities.MstUserDoctor> ListFacility()
        {
            var currentUser = from d in db.MstUsers
                              where d.AspNetUserId == User.Identity.GetUserId()
                              select d;

            var currentUserId = currentUser.FirstOrDefault().Id;
            var currentUserTypeId = currentUser.FirstOrDefault().UserTypeId;

            if (currentUserTypeId == 1)
            {
                var userFacilities = from d in db.MstUsers
                                     where d.AspNetUserId == User.Identity.GetUserId()
                                     && d.UserTypeId == 1
                                     && d.Id == currentUserId
                                     select new Entities.MstUserDoctor
                                     {
                                         Id = d.Id,
                                         UserId = d.Id,
                                         UserFacility = d.FullName,
                                         UserTypeId = d.MstUserType.Id
                                     };

                return userFacilities.ToList();
            }
            else
            {
                var userFacilities = from d in db.MstUserDoctors
                                     where d.DoctorId == currentUserId
                                     select new Entities.MstUserDoctor
                                     {
                                         Id = d.Id,
                                         UserId = d.UserId,
                                         UserFacility = d.MstUser.FullName,
                                         UserTypeId = d.MstUser1.MstUserType.Id
                                     };

                return userFacilities.ToList();
            }
        }
    }
}
