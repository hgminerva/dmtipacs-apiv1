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
    [Authorize, RoutePrefix("api/procedureResult")]
    public class ApiTrnProcedureResultResultController : ApiController
    {
        // ============
        // Data Context
        // ============
        private Data.dmtipacsdbDataContext db = new Data.dmtipacsdbDataContext();

        // =======================
        // List - Procedure Result
        // =======================
        [HttpGet, Route("list/{procedureId}")]
        public List<Entities.TrnProcedureResult> ListProcedureResult(String procedureId)
        {
            var procedureResults = from d in db.TrnProcedureResults.OrderByDescending(d => d.Id)
                                   where d.ProcedureId == Convert.ToInt32(procedureId)
                                   select new Entities.TrnProcedureResult
                                   {
                                       Id = d.Id,
                                       ProcedureId = d.ProcedureId,
                                       ModalityProcedureId = d.ModalityProcedureId,
                                       ModalityProcedure = d.MstModalityProcedure.ModalityProcedure,
                                       Result = d.Result,
                                       DoctorId = d.DoctorId,
                                       Doctor = d.MstUser.FullName,
                                       DoctorDateTime = d.DoctorDateTime.ToShortDateString(),
                                       DoctorTime = d.DoctorDateTime.ToShortTimeString()
                                   };

            return procedureResults.ToList();
        }

        // ======================
        // Add - Procedure Result
        // ======================
        [HttpPost, Route("add")]
        public HttpResponseMessage AddProcedureResult(Entities.TrnProcedureResult objProcedureResult)
        {
            try
            {
                var currentUser = from d in db.MstUsers
                                  where d.AspNetUserId == User.Identity.GetUserId()
                                  select d;

                var currentUserId = currentUser.FirstOrDefault().Id;
                var currentUserTypeId = currentUser.FirstOrDefault().UserTypeId;

                if (currentUserTypeId == 2)
                {
                    Data.TrnProcedureResult newProcedureResult = new Data.TrnProcedureResult
                    {
                        ProcedureId = objProcedureResult.ProcedureId,
                        ModalityProcedureId = objProcedureResult.ModalityProcedureId,
                        Result = objProcedureResult.Result,
                        DoctorId = currentUserId,
                        DoctorDateTime = DateTime.Now
                    };

                    db.TrnProcedureResults.InsertOnSubmit(newProcedureResult);
                    db.SubmitChanges();

                    return Request.CreateResponse(HttpStatusCode.OK);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest);
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }

        // =========================
        // Update - Procedure Result
        // =========================
        [HttpPut, Route("update/{id}")]
        public HttpResponseMessage UpdateProcedureResult(String id, Entities.TrnProcedureResult objProcedureResult)
        {
            try
            {
                var currentUser = from d in db.MstUsers
                                  where d.AspNetUserId == User.Identity.GetUserId()
                                  select d;

                var currentUserId = currentUser.FirstOrDefault().Id;
                var currentUserTypeId = currentUser.FirstOrDefault().UserTypeId;

                if (currentUserTypeId == 2)
                {
                    var procedureResult = from d in db.TrnProcedureResults
                                          where d.Id == Convert.ToInt32(id)
                                          select d;

                    if (procedureResult.Any())
                    {
                        var updateProcedureResult = procedureResult.FirstOrDefault();
                        updateProcedureResult.ProcedureId = objProcedureResult.ProcedureId;
                        updateProcedureResult.ModalityProcedureId = objProcedureResult.ModalityProcedureId;
                        updateProcedureResult.Result = objProcedureResult.Result;
                        updateProcedureResult.DoctorId = currentUserId;
                        updateProcedureResult.DoctorDateTime = DateTime.Now;

                        db.SubmitChanges();

                        return Request.CreateResponse(HttpStatusCode.OK);
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.NotFound);
                    }
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest);
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }

        // =========================
        // Delete - Procedure Result
        // =========================
        [HttpDelete, Route("delete/{id}")]
        public HttpResponseMessage DeleteProcedureResult(String id)
        {
            try
            {
                var procedureResult = from d in db.TrnProcedureResults
                                      where d.Id == Convert.ToInt32(id)
                                      select d;

                if (procedureResult.Any())
                {
                    db.TrnProcedureResults.DeleteOnSubmit(procedureResult.First());
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