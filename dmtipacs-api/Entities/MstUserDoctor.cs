using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace dmtipacs_api.Entities
{
    public class MstUserDoctor
    {
        public Int32 Id { get; set; }
        public Int32 UserId { get; set; }
        public Int32 DoctorId { get; set; }
    }
}