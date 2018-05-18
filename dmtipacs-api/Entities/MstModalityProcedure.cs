using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace dmtipacs_api.Entities
{
    public class MstModalityProcedure
    {
        public Int32 Id { get; set; }
        public Int32 ModalityId { get; set; }
        public String ModalityProcedure { get; set; }
        public String ModalityResultTemplate { get; set; }
        public Int32 DoctorId { get; set; }
    }
}