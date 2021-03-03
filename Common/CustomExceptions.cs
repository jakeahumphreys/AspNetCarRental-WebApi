using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EIRLSS_Data_API.Common
{
    public class ConfigurationNotFoundException : Exception
    {
        public ConfigurationNotFoundException(string message) : base(message) { }
    }
}