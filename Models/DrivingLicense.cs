using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EIRLSS_Data_API.Models
{
    public class DrivingLicense
    {
        public int Id { get; set; }
        public string LicenseNumber { get; set; }
        public string FamilyName { get; set; }
        public string Forenames { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime YearOfIssue { get; set; }
        public DateTime Expires { get; set; }
        public string IssuingAuthority { get; set; }
        public string Address { get; set; }
        public string Status { get; set; }
        public DateTime Date { get; set; }
    }
}