using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace dmtipacs_api.ApiControllers
{
    [Authorize, RoutePrefix("api/procedureDetailReport")]
    public class ApiRepProcedureDetailReportController : ApiController
    {
        // ============
        // Data Context
        // ============
        private Data.dmtipacsdbDataContext db = new Data.dmtipacsdbDataContext();

        // ============================================
        // List By Date Range - Procedure Detail Report
        // ============================================
        [HttpGet, Route("list/byDateRange/{startDate}/{endDate}/{facilityId}")]
        public List<Entities.TrnProcedureResult> ListProcedureDetailReportByDateRange(String startDate, String endDate, String facilityId)
        {
            var procedureResults = from d in db.TrnProcedureResults
                                   where d.TrnProcedure.TransactionDateTime >= Convert.ToDateTime(startDate)
                                   && d.TrnProcedure.TransactionDateTime <= Convert.ToDateTime(endDate)
                                   && d.TrnProcedure.UserId == Convert.ToInt32(facilityId)
                                   select new Entities.TrnProcedureResult
                                   {
                                       Id = d.Id,
                                       Facility = d.TrnProcedure.MstUser.FullName,
                                       TransactionNumber = d.TrnProcedure.TransactionNumber,
                                       TransactionDateTime = d.TrnProcedure.TransactionDateTime.ToShortDateString(),
                                       Patient = d.TrnProcedure.PatientName,
                                       Modality = d.TrnProcedure.MstModality.Modality,
                                       ModalityProcedure = d.MstModalityProcedure.ModalityProcedure,
                                       Doctor = d.MstUser.FullName,
                                       FacilityRate = d.TrnProcedure.MstUser.MstUserRates.FirstOrDefault().FacilityRate,
                                       DoctorRate = d.MstUser.MstUserRates.FirstOrDefault().DoctorRate,
                                       ImageRate = d.MstModalityProcedure.MstUserRates.FirstOrDefault().ImageRate,
                                   };

            return procedureResults.ToList();
        }
    }
}
