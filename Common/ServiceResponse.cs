using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EIRLSS_Data_API.Common
{
    public class ServiceResponse
    {
        public bool Success { get; set; }
        public Object ServiceObject { get; set; }
        public string ErrorMessage { get; set; }
    }
}