using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace dmtipacs_api.Entities
{
    public class TrnProcedureResult
    {
        public Int32 Id { get; set; }
        public Int32 ProcedureId { get; set; }
        public Int32 ModalityProcedureId { get; set; }
        public String Result { get; set; }
        public Int32 DoctorId { get; set; }
        public String DoctorDateTime { get; set; }
    }
}