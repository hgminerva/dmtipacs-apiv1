using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace dmtipacs_api.ApiControllers
{
    [Authorize]
    [RoutePrefix("api/modality")]
    public class ApiMstModalityController : ApiController
    {
        // ============
        // Data Context
        // ============
        private Data.dmtipacsdbDataContext db = new Data.dmtipacsdbDataContext();

        // ===============
        // List - Modality
        // ===============
        [HttpGet, Route("list")]
        public List<Entities.MstModality> ListModality()
        {
            var modalities = from d in db.MstModalities
                             select new Entities.MstModality
                             {
                                 Id = d.Id,
                                 Modality = d.Modality
                             };

            return modalities.ToList();
        }

        // ==============
        // Add - Modality
        // ==============
        [Authorize, HttpPost, Route("add")]
        public HttpResponseMessage AddModality(Entities.MstModality objModality)
        {
            try
            {
                Data.MstModality newModality = new Data.MstModality
                {
                    Modality = objModality.Modality
                };

                db.MstModalities.InsertOnSubmit(newModality);
                db.SubmitChanges();

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // =================
        // Update - Modality
        // =================
        [Authorize, HttpPut, Route("update/{id}")]
        public HttpResponseMessage UpdateModality(String id, Entities.MstModality objModality)
        {
            try
            {
                var modality = from d in db.MstModalities
                               where d.Id == Convert.ToInt32(id)
                               select d;

                if (modality.Any())
                {
                    var updateModality = modality.FirstOrDefault();
                    updateModality.Modality = objModality.Modality;

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
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // =================
        // Delete - Modality
        // =================
        [Authorize, HttpDelete, Route("delete/{id}")]
        public HttpResponseMessage DeleteModality(String id)
        {
            try
            {
                var modality = from d in db.MstModalities
                               where d.Id == Convert.ToInt32(id)
                               select d;

                if (modality.Any())
                {
                    db.MstModalities.DeleteOnSubmit(modality.First());
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
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }
    }
}