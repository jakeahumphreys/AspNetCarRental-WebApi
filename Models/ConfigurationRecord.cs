using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Web;

namespace EIRLSS_Data_API.Models
{
    public class ConfigurationRecord
    {
        public int Id { get; set; }
        [Display(Name = "Record Created")]
        public DateTime RecordCreated { get; set; }
        [Required]
        [Display(Name = "DVLA Import File Path")]
        public string DvlaImportPath { get; set; }
        [Required]
        [Display(Name = "ABI Import File Path")]
        public string AbiImportPath { get; set; }
    }
}