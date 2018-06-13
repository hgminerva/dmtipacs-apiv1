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
        [HttpGet, Route("list/byDateRange/{startDate}/{endDate}/{facilityId}")]
        public List<Entities.TrnProcedure> ListProcedureByDateRange(String startDate, String endDate, String facilityId)
        {
            var procedures = from d in db.TrnProcedures.OrderByDescending(d => d.Id)
                             where d.UserId == Convert.ToInt32(facilityId)
                             && d.TransactionDateTime >= Convert.ToDateTime(startDate)
                             && d.TransactionDateTime <= Convert.ToDateTime(endDate).AddHours(24)
                             select new Entities.TrnProcedure
                             {
                                 Id = d.Id,
                                 TransactionNumber = d.TransactionNumber,
                                 TransactionDateTime = d.TransactionDateTime.ToShortDateString(),
                                 TransactionTime = d.TransactionDateTime.ToShortTimeString(),
                                 PatientName = d.PatientName,
                                 Gender = d.Gender,
                                 Age = d.Age,
                                 Modality = d.MstModality.Modality,
                                 BodyPart = d.MstBodyPart.BodyPart,
                                 Doctor = d.TrnProcedureResults.Any() ? d.TrnProcedureResults.Where(r => r.ProcedureId == d.Id).Select(r => r.MstUser.FullName).FirstOrDefault() : ""
                             };

            return procedures.ToList();
        }


        // ============================
        // List - Procedure Comparative
        // ============================
        [HttpGet, Route("list/comparative/{id}/{facilityId}")]
        public List<Entities.TrnProcedure> ListProcedureComparative(String id, String facilityId)
        {
            var originalProcedure = from d in db.TrnProcedures
                                    where d.Id == Convert.ToInt32(id)
                                    select d;

            if (originalProcedure.Any())
            {
                var procedures = from d in db.TrnProcedures.OrderByDescending(d => d.TransactionNumber)
                                 where d.UserId == Convert.ToInt32(facilityId)
                                 && d.PatientName.Equals(originalProcedure.FirstOrDefault().PatientName)
                                 && d.Id != Convert.ToInt32(id)
                                 select new Entities.TrnProcedure
                                 {
                                     Id = d.Id,
                                     TransactionNumber = d.TransactionNumber,
                                     TransactionDateTime = d.TransactionDateTime.ToShortDateString(),
                                     TransactionTime = d.TransactionDateTime.ToShortTimeString(),
                                     DICOMFileName = d.DICOMFileName,
                                     PatientName = d.PatientName,
                                     DateOfBirth = d.DateOfBirth.ToShortDateString(),
                                     Gender = d.Gender,
                                     Age = d.Age,
                                     Particulars = d.Particulars,
                                     ModalityId = d.ModalityId,
                                     Modality = d.MstModality.Modality,
                                     BodyPartId = d.BodyPartId,
                                     BodyPart = d.MstBodyPart.BodyPart,
                                     UserId = d.UserId,
                                     User = d.MstUser.FullName,
                                     PatientAddress = d.PatientAddress,
                                     ReferringPhysician = d.ReferringPhysician,
                                     StudyDate = d.StudyDate,
                                     HospitalNumber = d.HospitalNumber,
                                     HospitalWardNumber = d.HospitalWardNumber,
                                     StudyInstanceId = d.StudyInstanceId,
                                     Doctor = d.TrnProcedureResults.Any() ? d.TrnProcedureResults.Where(r => r.ProcedureId == d.Id).Select(r => r.MstUser.FullName).FirstOrDefault() : ""
                                 };

                return procedures.ToList();
            }
            else
            {
                return new List<Entities.TrnProcedure>();
            }
        }

        // ==================
        // Detail - Procedure
        // ==================
        [HttpGet, Route("detail/{id}")]
        public Entities.TrnProcedure DetailProcedure(String id)
        {
            var procedures = from d in db.TrnProcedures.OrderByDescending(d => d.Id)
                             where d.Id == Convert.ToInt32(id)
                             select new Entities.TrnProcedure
                             {
                                 Id = d.Id,
                                 TransactionNumber = d.TransactionNumber,
                                 TransactionDateTime = d.TransactionDateTime.ToShortDateString(),
                                 TransactionTime = d.TransactionDateTime.ToShortTimeString(),
                                 DICOMFileName = d.DICOMFileName,
                                 PatientName = d.PatientName,
                                 DateOfBirth = d.DateOfBirth.ToShortDateString(),
                                 Gender = d.Gender,
                                 Age = d.Age,
                                 Particulars = d.Particulars,
                                 ModalityId = d.ModalityId,
                                 Modality = d.MstModality.Modality,
                                 BodyPartId = d.BodyPartId,
                                 BodyPart = d.MstBodyPart.BodyPart,
                                 UserId = d.UserId,
                                 User = d.MstUser.FullName,
                                 PatientAddress = d.PatientAddress,
                                 ReferringPhysician = d.ReferringPhysician,
                                 StudyDate = d.StudyDate,
                                 HospitalNumber = d.HospitalNumber,
                                 HospitalWardNumber = d.HospitalWardNumber,
                                 StudyInstanceId = d.StudyInstanceId
                             };

            return procedures.FirstOrDefault();
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
            catch (Exception e)
            {
                Debug.WriteLine(e);
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }
    }
}