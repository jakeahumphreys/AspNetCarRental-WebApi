using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EIRLSS_Data_API.Common;
using EIRLSS_Data_API.DAL;
using EIRLSS_Data_API.Models;

namespace EIRLSS_Data_API.ServiceLayer
{
    public class ConfigurationService
    {
        private ConfigurationRepository _configurationRepository;

        public ConfigurationService()
        {
            _configurationRepository = new ConfigurationRepository(new ApplicationDbContext());
        }

        public IList<ConfigurationRecord> GetAll()
        {
            return _configurationRepository.GetConfigurations();
        }

        public ConfigurationRecord GetDetails(int id)
        {
            if (id == 0)
            {
                throw new ArgumentException("Expected an int");
            }
            ConfigurationRecord configuration = _configurationRepository.GetConfigurationById(id);
            if (configuration == null)
            {
                throw new ConfigurationNotFoundException("Configuration not found.");
            }
            return configuration;
        }

        public ServiceResponse CreateAction(ConfigurationRecord configuration)
        {
            configuration.RecordCreated = DateTime.Now;

            _configurationRepository.Insert(configuration);
            _configurationRepository.Save();

            ServiceResponse response = new ServiceResponse
            {
                Success = true,
                ServiceObject = configuration
            };

            return response;
        }

        public ConfigurationRecord EditView(int id)
        {
            if (id == 0)
            {
                throw new ArgumentException("Expected parameter");
            }
            ConfigurationRecord configuration = _configurationRepository.GetConfigurationById(id);
            if (configuration == null)
            {
                throw new ConfigurationNotFoundException("Configuration not found.");
            }
            return configuration;
        }

        public ServiceResponse EditAction(ConfigurationRecord configuration)
        {

            ConfigurationRecord configToUpdate = _configurationRepository.GetConfigurationById(configuration.Id);

            configToUpdate.RecordCreated = DateTime.Now;
            configToUpdate.AbiImportPath = configuration.AbiImportPath;
            configToUpdate.DvlaImportPath = configuration.DvlaImportPath;

            _configurationRepository.Update(configToUpdate);
            _configurationRepository.Save();

            ServiceResponse response = new ServiceResponse
            {
                Success = true,
                ServiceObject = configToUpdate
            };

            return response;

        }

        public ConfigurationRecord DeleteView(int id)
        {
            if (id == 0)
            {
                throw new ArgumentException("Expected an int");
            }
            ConfigurationRecord configuration = _configurationRepository.GetConfigurationById(id);
            if (configuration == null)
            {
                throw new ConfigurationNotFoundException("Configuration not found");
            }
            return configuration;
        }

        public ServiceResponse DeleteAction(int id)
        {
            ConfigurationRecord configuration = _configurationRepository.GetConfigurationById(id);
            _configurationRepository.Delete(configuration);
            _configurationRepository.Save();
            return new ServiceResponse { Success = true };
        }

        public void Dispose()
        {
            _configurationRepository.Dispose();
        }

    }
}