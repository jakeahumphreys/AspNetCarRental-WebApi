using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EIRLSS_Data_API.Models;

namespace EIRLSS_Data_API.DAL
{
    public interface IConfigurationRepository : IDisposable
    {
        IList<ConfigurationRecord> GetConfigurations();
        ConfigurationRecord GetConfigurationById(int id);
        void Insert(ConfigurationRecord Configuration);
        void Update(ConfigurationRecord Configuration);
        void Delete(ConfigurationRecord Configuration);
        void Save();
    }
}