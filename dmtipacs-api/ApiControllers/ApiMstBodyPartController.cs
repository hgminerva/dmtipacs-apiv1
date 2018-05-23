using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace dmtipacs_api.ApiControllers
{
    [Authorize, RoutePrefix("api/bodyParts")]
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
            var bodyParts = from d in db.MstBodyParts.OrderByDescending(d => d.Id)
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
        [HttpPost, Route("add")]
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
            catch (Exception e)
            {
                Debug.WriteLine(e);
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }

        // ==================
        // Update - Body Part
        // ==================
        [HttpPut, Route("update/{id}")]
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
            catch (Exception e)
            {
                Debug.WriteLine(e);
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }

        // ==================
        // Delete - Body Part
        // ==================
        [HttpDelete, Route("delete/{id}")]
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
            catch (Exception e)
            {
                Debug.WriteLine(e);
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }
    }
}