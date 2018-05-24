using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace dmtipacs_api.Entities
{
    public class TrnProcedure
    {
        public Int32 Id { get; set; }
        public String Facility { get; set; }
        public String TransactionNumber { get; set; }
        public String TransactionDateTime { get; set; }
        public String TransactionTime { get; set; }
        public String DICOMFileName { get; set; }
        public String PatientName { get; set; }
        public String Gender { get; set; }
        public String DateOfBirth { get; set; }
        public Int32 Age { get; set; }
        public String Particulars { get; set; }
        public Int32 ModalityId { get; set; }
        public String Modality { get; set; }
        public String Doctor { get; set; }
        public Int32 BodyPartId { get; set; }
        public Int32 UserId { get; set; }
        public String PatientAddress { get; set; }
        public String ReferringPhysician { get; set; }
        public String StudyDate { get; set; }
        public String HospitalNumber { get; set; }
        public String HospitalWardNumber { get; set; }
        public String StudyInstanceId { get; set; }
    }
}