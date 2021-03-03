using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EIRLSS_Data_API.Models;
using EIRLSS_Data_API.ServiceLayer;

namespace EIRLSS_Data_API.Common
{
    public class DirectoryHelper
    {
        public ConfigurationService _ConfigurationService;

        public DirectoryHelper()
        {
            _ConfigurationService = new ConfigurationService();
        }

        public ConfigurationRecord GetLatestRecord()
        {
            return _ConfigurationService.GetAll().OrderByDescending(x => x.RecordCreated).FirstOrDefault();
        }
    }
}