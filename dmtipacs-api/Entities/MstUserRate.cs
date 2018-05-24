using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace dmtipacs_api.Entities
{
    public class MstUserRate
    {
        public Int32 Id { get; set; }
        public Int32 UserId { get; set; }
        public Int32 ModalityProcedureId { get; set; }
        public String ModalityProcedure { get; set; }
        public String ModalityProcedureCode { get; set; }
        public Decimal FacilityRate { get; set; }
        public Decimal DoctorRate { get; set; }
        public Decimal ImageRate { get; set; }
        public String Remarks { get; set; }
    }
}