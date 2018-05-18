using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace dmtipacs_api.ApiControllers
{
    [Authorize, RoutePrefix("api/modalityProcedure")]
    public class ApiMstModalityProcedureController : ApiController
    {
        // ============
        // Data Context
        // ============
        private Data.dmtipacsdbDataContext db = new Data.dmtipacsdbDataContext();

        // =========================
        // List - Modality Procedure
        // =========================
        [HttpGet, Route("list")]
        public List<Entities.MstModalityProcedure> ListModalityProcedure()
        {
            var modalityProcedures = from d in db.MstModalityProcedures
                                     select new Entities.MstModalityProcedure
                                     {
                                         Id = d.Id,
                                         ModalityId = d.ModalityId,
                                         ModalityProcedure = d.ModalityProcedure,
                                         ModalityResultTemplate = d.ModalityResultTemplate,
                                         DoctorId = d.DoctorId
                                     };

            return modalityProcedures.ToList();
        }

        // ========================
        // Add - Modality Procedure
        // ========================
        [HttpPost, Route("add")]
        public HttpResponseMessage AddModalityProcedure(Entities.MstModalityProcedure objModalityProcedure)
        {
            try
            {
                Data.MstModalityProcedure newModalityProcedure = new Data.MstModalityProcedure
                {
                    ModalityId = objModalityProcedure.ModalityId,
                    ModalityProcedure = objModalityProcedure.ModalityProcedure,
                    ModalityResultTemplate = objModalityProcedure.ModalityResultTemplate,
                    DoctorId = objModalityProcedure.DoctorId
                };

                db.MstModalityProcedures.InsertOnSubmit(newModalityProcedure);
                db.SubmitChanges();

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }

        // ===========================
        // Update - Modality Procedure
        // ===========================
        [HttpPut, Route("update/{id}")]
        public HttpResponseMessage UpdateModalityProcedure(String id, Entities.MstModalityProcedure objModalityProcedure)
        {
            try
            {
                var modalityProcedure = from d in db.MstModalityProcedures
                                        where d.Id == Convert.ToInt32(id)
                                        select d;

                if (modalityProcedure.Any())
                {
                    var updateModalityProcedure = modalityProcedure.FirstOrDefault();
                    updateModalityProcedure.ModalityId = objModalityProcedure.ModalityId;
                    updateModalityProcedure.ModalityProcedure = objModalityProcedure.ModalityProcedure;
                    updateModalityProcedure.ModalityResultTemplate = objModalityProcedure.ModalityResultTemplate;
                    updateModalityProcedure.DoctorId = objModalityProcedure.DoctorId;

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

        // ===========================
        // Delete - Modality Procedure
        // ===========================
        [HttpDelete, Route("delete/{id}")]
        public HttpResponseMessage DeleteModalityProcedure(String id)
        {
            try
            {
                var modalityProcedure = from d in db.MstModalityProcedures
                                        where d.Id == Convert.ToInt32(id)
                                        select d;

                if (modalityProcedure.Any())
                {
                    db.MstModalityProcedures.DeleteOnSubmit(modalityProcedure.First());
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