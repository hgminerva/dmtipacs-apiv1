using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace dmtipacs_api.ApiControllers
{
    [Authorize, RoutePrefix("api/procedure")]
    public class ApiTrnProcedureController : ApiController
    {
        // ============
        // Data Context
        // ============
        private Data.dmtipacsdbDataContext db = new Data.dmtipacsdbDataContext();

        // ================
        // List - Procedure
        // ================
        [HttpGet, Route("list")]
        public List<Entities.TrnProcedure> ListProcedure()
        {
            var procedures = from d in db.TrnProcedures
                             select new Entities.TrnProcedure
                             {
                                 Id = d.Id,
                                 TransactionNumber = d.TransactionNumber,
                                 TransactionDateTime = d.TransactionDateTime.ToShortDateString(),
                                 DICOMFileName = d.DICOMFileName,
                                 PatientName = d.PatientName,
                                 Gender = d.Gender,
                                 DateOfBirth = d.DateOfBirth.ToShortDateString(),
                                 Age = d.Age,
                                 Particulars = d.Particulars,
                                 ModalityId = d.ModalityId,
                                 BodyPartId = d.BodyPartId,
                                 UserId = d.UserId,
                                 PatientAddress = d.PatientAddress,
                                 ReferringPhysician = d.ReferringPhysician,
                                 StudyDate = d.StudyDate,
                                 HospitalNumber = d.HospitalNumber,
                                 HospitalWardNumber = d.HospitalWardNumber,
                                 StudyInstanceId = d.StudyInstanceId
                             };

            return procedures.ToList();
        }

        // ===============
        // Add - Procedure
        // ===============
        [HttpPost, Route("add")]
        public HttpResponseMessage AddProcedure(Entities.TrnProcedure objProcedure)
        {
            try
            {
                Data.TrnProcedure newProcedure = new Data.TrnProcedure
                {
                    TransactionNumber = objProcedure.TransactionNumber,
                    TransactionDateTime = Convert.ToDateTime(objProcedure.TransactionDateTime),
                    DICOMFileName = objProcedure.DICOMFileName,
                    PatientName = objProcedure.PatientName,
                    Gender = objProcedure.Gender,
                    DateOfBirth = Convert.ToDateTime(objProcedure.DateOfBirth),
                    Age = objProcedure.Age,
                    Particulars = objProcedure.Particulars,
                    ModalityId = objProcedure.ModalityId,
                    BodyPartId = objProcedure.BodyPartId,
                    UserId = objProcedure.UserId,
                    PatientAddress = objProcedure.PatientAddress,
                    ReferringPhysician = objProcedure.ReferringPhysician,
                    StudyDate = objProcedure.StudyDate,
                    HospitalNumber = objProcedure.HospitalNumber,
                    HospitalWardNumber = objProcedure.HospitalWardNumber,
                    StudyInstanceId = objProcedure.StudyInstanceId
                };

                db.TrnProcedures.InsertOnSubmit(newProcedure);
                db.SubmitChanges();

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }

        // ==================
        // Update - Procedure
        // ==================
        [HttpPut, Route("update/{id}")]
        public HttpResponseMessage UpdateProcedure(String id, Entities.TrnProcedure objProcedure)
        {
            try
            {
                var procedure = from d in db.TrnProcedures
                                where d.Id == Convert.ToInt32(id)
                                select d;

                if (procedure.Any())
                {
                    var updateProcedure = procedure.FirstOrDefault();
                    updateProcedure.TransactionNumber = objProcedure.TransactionNumber;
                    updateProcedure.TransactionDateTime = Convert.ToDateTime(objProcedure.TransactionDateTime);
                    updateProcedure.DICOMFileName = objProcedure.DICOMFileName;
                    updateProcedure.PatientName = objProcedure.PatientName;
                    updateProcedure.Gender = objProcedure.Gender;
                    updateProcedure.DateOfBirth = Convert.ToDateTime(objProcedure.DateOfBirth);
                    updateProcedure.Age = objProcedure.Age;
                    updateProcedure.Particulars = objProcedure.Particulars;
                    updateProcedure.ModalityId = objProcedure.ModalityId;
                    updateProcedure.BodyPartId = objProcedure.BodyPartId;
                    updateProcedure.UserId = objProcedure.UserId;
                    updateProcedure.PatientAddress = objProcedure.PatientAddress;
                    updateProcedure.ReferringPhysician = objProcedure.ReferringPhysician;
                    updateProcedure.StudyDate = objProcedure.StudyDate;
                    updateProcedure.HospitalNumber = objProcedure.HospitalNumber;
                    updateProcedure.HospitalWardNumber = objProcedure.HospitalWardNumber;
                    updateProcedure.StudyInstanceId = objProcedure.StudyInstanceId;

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

        // ==================
        // Delete - Procedure
        // ==================
        [HttpDelete, Route("delete/{id}")]
        public HttpResponseMessage DeleteProcedure(String id)
        {
            try
            {
                var procedure = from d in db.TrnProcedures
                                where d.Id == Convert.ToInt32(id)
                                select d;

                if (procedure.Any())
                {
                    db.TrnProcedures.DeleteOnSubmit(procedure.First());
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
