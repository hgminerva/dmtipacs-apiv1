﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace dmtipacs_api.Entities
{
    public class MstUser
    {
        public Int32 Id { get; set; }
        public String UserName { get; set; }
        public String FullName { get; set; }
        public String Address { get; set; }
        public String ContactNumber { get; set; }
        public Int32 UserTypeId { get; set; }
        public String AspNetUserId { get; set; }
    }
}