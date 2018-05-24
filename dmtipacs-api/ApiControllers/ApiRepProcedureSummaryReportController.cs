using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace dmtipacs_api.ApiControllers
{
    [Authorize, RoutePrefix("api/procedureSummaryReport")]
    public class ApiRepProcedureSummaryReportController : ApiController
    {
        // ============
        // Data Context
        // ============
        private Data.dmtipacsdbDataContext db = new Data.dmtipacsdbDataContext();

        // =============================================
        // List By Date Range - Procedure Summary Report
        // =============================================
        [HttpGet, Route("list/byDateRange/{startDate}/{endDate}/{facilityId}")]
        public List<Entities.TrnProcedure> ListProcedureSummaryReportByDateRange(String startDate, String endDate, String facilityId)
        {
            var procedures = from d in db.TrnProcedures
                             where d.TransactionDateTime >= Convert.ToDateTime(startDate)
                             && d.TransactionDateTime <= Convert.ToDateTime(endDate)
                             && d.UserId == Convert.ToInt32(facilityId)
                             select new Entities.TrnProcedure
                             {
                                 Id = d.Id,
                                 Facility = d.MstUser.FullName,
                                 TransactionNumber = d.TransactionNumber,
                                 TransactionDateTime = d.TransactionDateTime.ToShortDateString(),
                                 TransactionTime = d.TransactionDateTime.ToShortTimeString(),
                                 PatientName = d.PatientName,
                                 Age = d.Age,
                                 Modality = d.MstModality.Modality,
                                 Doctor = d.TrnProcedureResults.Any() ? d.TrnProcedureResults.Where(r => r.ProcedureId == d.Id).Select(r => r.MstUser.FullName).FirstOrDefault() : ""
                             };

            return procedures.ToList();
        }
    }
}
