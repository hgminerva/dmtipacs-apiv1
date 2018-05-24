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
    [Authorize, RoutePrefix("api/userRate")]
    public class ApiMstUserRateController : ApiController
    {
        // ============
        // Data Context
        // ============
        private Data.dmtipacsdbDataContext db = new Data.dmtipacsdbDataContext();

        // ================
        // List - User Rate
        // ================
        [HttpGet, Route("list")]
        public List<Entities.MstUserRate> ListUserRate()
        {
            var userRates = from d in db.MstUserRates.OrderByDescending(d => d.Id)
                            select new Entities.MstUserRate
                            {
                                Id = d.Id,
                                UserId = d.UserId,
                                ModalityProcedureId = d.ModalityProcedureId,
                                ModalityProcedure = d.MstModalityProcedure.ModalityProcedure,
                                ModalityProcedureCode = d.ModalityProcedureCode,
                                FacilityRate = d.FacilityRate,
                                DoctorRate = d.DoctorRate,
                                ImageRate = d.ImageRate,
                                Remarks = d.Remarks
                            };

            return userRates.ToList();
        }

        // ===============
        // Add - User Rate
        // ===============
        [HttpPost, Route("add")]
        public HttpResponseMessage AddUserRate(Entities.MstUserRate objUserRate)
        {
            try
            {
                var currentUser = from d in db.MstUsers
                                  where d.AspNetUserId == User.Identity.GetUserId()
                                  select d;

                var currentUserId = currentUser.FirstOrDefault().Id;

                Data.MstUserRate newUserRate = new Data.MstUserRate
                {
                    UserId = currentUserId,
                    ModalityProcedureId = objUserRate.ModalityProcedureId,
                    ModalityProcedureCode = objUserRate.ModalityProcedureCode,
                    FacilityRate = objUserRate.FacilityRate,
                    DoctorRate = objUserRate.DoctorRate,
                    ImageRate = objUserRate.ImageRate,
                    Remarks = objUserRate.Remarks
                };

                db.MstUserRates.InsertOnSubmit(newUserRate);
                db.SubmitChanges();

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }

        // ==================
        // Update - User Rate
        // ==================
        [HttpPut, Route("update/{id}")]
        public HttpResponseMessage UpdateUserRate(String id, Entities.MstUserRate objUserRate)
        {
            try
            {
                var userRate = from d in db.MstUserRates
                               where d.Id == Convert.ToInt32(id)
                               select d;

                if (userRate.Any())
                {
                    var currentUser = from d in db.MstUsers
                                      where d.AspNetUserId == User.Identity.GetUserId()
                                      select d;

                    var currentUserId = currentUser.FirstOrDefault().Id;

                    var updateUserRate = userRate.FirstOrDefault();
                    updateUserRate.UserId = currentUserId;
                    updateUserRate.ModalityProcedureId = objUserRate.ModalityProcedureId;
                    updateUserRate.ModalityProcedureCode = objUserRate.ModalityProcedureCode;
                    updateUserRate.FacilityRate = objUserRate.FacilityRate;
                    updateUserRate.DoctorRate = objUserRate.DoctorRate;
                    updateUserRate.ImageRate = objUserRate.ImageRate;
                    updateUserRate.Remarks = objUserRate.Remarks;

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

        // ==================
        // Delete - User Rate
        // ==================
        [HttpDelete, Route("delete/{id}")]
        public HttpResponseMessage DeleteUserRate(String id)
        {
            try
            {
                var userRate = from d in db.MstUserRates
                               where d.Id == Convert.ToInt32(id)
                               select d;

                if (userRate.Any())
                {
                    db.MstUserRates.DeleteOnSubmit(userRate.First());
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
    }
}