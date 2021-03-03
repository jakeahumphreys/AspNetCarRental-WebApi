using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace EIRLSS_Data_API.DTO
{
    [DataContract(Name = "Price")]
    public class PriceResponse
    {
        [DataMember(Name = "PRICE")]
        public double Price { get; set; }
    }
}