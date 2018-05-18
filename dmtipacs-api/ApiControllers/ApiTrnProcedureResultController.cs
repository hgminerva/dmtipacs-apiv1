using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace dmtipacs_api.ApiControllers
{
    [Authorize, RoutePrefix("api/procedureResultResult")]
    public class ApiTrnProcedureResultResultController : ApiController
    {
        // ============
        // Data Context
        // ============
        private Data.dmtipacsdbDataContext db = new Data.dmtipacsdbDataContext();

        // =======================
        // List - Procedure Result
        // =======================
        [HttpGet, Route("list")]
        public List<Entities.TrnProcedureResult> ListProcedureResult()
        {
            var procedureResults = from d in db.TrnProcedureResults
                                   select new Entities.TrnProcedureResult
                                   {
                                       Id = d.Id,
                                       ProcedureId = d.ProcedureId,
                                       ModalityProcedureId = d.ModalityProcedureId,
                                       Result = d.Result,
                                       DoctorId = d.DoctorId,
                                       DoctorDateTime = d.DoctorDateTime.ToShortDateString()
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
                Data.TrnProcedureResult newProcedureResult = new Data.TrnProcedureResult
                {
                    ProcedureId = objProcedureResult.ProcedureId,
                    ModalityProcedureId = objProcedureResult.ModalityProcedureId,
                    Result = objProcedureResult.Result,
                    DoctorId = objProcedureResult.DoctorId,
                    DoctorDateTime = Convert.ToDateTime(objProcedureResult.DoctorDateTime)
                };

                db.TrnProcedureResults.InsertOnSubmit(newProcedureResult);
                db.SubmitChanges();

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch
            {
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
                var procedureResult = from d in db.TrnProcedureResults
                                      where d.Id == Convert.ToInt32(id)
                                      select d;

                if (procedureResult.Any())
                {
                    var updateProcedureResult = procedureResult.FirstOrDefault();
                    updateProcedureResult.ProcedureId = objProcedureResult.ProcedureId;
                    updateProcedureResult.ModalityProcedureId = objProcedureResult.ModalityProcedureId;
                    updateProcedureResult.Result = objProcedureResult.Result;
                    updateProcedureResult.DoctorId = objProcedureResult.DoctorId;
                    updateProcedureResult.DoctorDateTime = Convert.ToDateTime(objProcedureResult.DoctorDateTime);

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
            catch
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }
    }
}