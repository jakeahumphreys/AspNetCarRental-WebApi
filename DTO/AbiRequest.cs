using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace EIRLSS_Data_API.DTO
{
    [DataContract]
    public class AbiRequest
    {
        [DataMember(Name = "ABI_FAMILY_NAME")]
        public string FamilyName { get; set; }
        [DataMember(Name = "ABI_FORENAMES")]
        public string Forenames { get; set; }
        [DataMember(Name = "ABI_ADDRESS")]
        public string Address { get; set; }
    }
}