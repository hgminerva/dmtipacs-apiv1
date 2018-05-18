using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace dmtipacs_api.ApiControllers
{
    [Authorize]
    [RoutePrefix("api/bodyPart")]
    public class ApiMstBodyPartController : ApiController
    {
        // ============
        // Data Context
        // ============
        private Data.dmtipacsdbDataContext db = new Data.dmtipacsdbDataContext();

        // ================
        // List - Body Part
        // ================
        [HttpGet, Route("list")]
        public List<Entities.MstBodyPart> ListBodyPart()
        {
            var bodyParts = from d in db.MstBodyParts
                            select new Entities.MstBodyPart
                            {
                                Id = d.Id,
                                BodyPart = d.BodyPart
                            };

            return bodyParts.ToList();
        }

        // ===============
        // Add - Body Part
        // ===============
        [Authorize, HttpPost, Route("add")]
        public HttpResponseMessage AddBodyPart(Entities.MstBodyPart objBodyPart)
        {
            try
            {
                Data.MstBodyPart newBodyPart = new Data.MstBodyPart
                {
                    BodyPart = objBodyPart.BodyPart
                };

                db.MstBodyParts.InsertOnSubmit(newBodyPart);
                db.SubmitChanges();

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // ==================
        // Update - Body Part
        // ==================
        [Authorize, HttpPut, Route("update/{id}")]
        public HttpResponseMessage UpdateBodyPart(String id, Entities.MstBodyPart objBodyPart)
        {
            try
            {
                var bodyPart = from d in db.MstBodyParts
                               where d.Id == Convert.ToInt32(id)
                               select d;

                if (bodyPart.Any())
                {
                    var updateBodyPart = bodyPart.FirstOrDefault();
                    updateBodyPart.BodyPart = objBodyPart.BodyPart;

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

        // ==================
        // Delete - Body Part
        // ==================
        [Authorize, HttpDelete, Route("delete/{id}")]
        public HttpResponseMessage DeleteBodyPart(String id)
        {
            try
            {
                var bodyPart = from d in db.MstBodyParts
                               where d.Id == Convert.ToInt32(id)
                               select d;

                if (bodyPart.Any())
                {
                    db.MstBodyParts.DeleteOnSubmit(bodyPart.First());
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