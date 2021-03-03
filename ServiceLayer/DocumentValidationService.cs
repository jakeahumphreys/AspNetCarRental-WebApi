using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using EIRLSS_Data_API.Common;
using EIRLSS_Data_API.Models;
using EIRLSSAssignment1.DAL;

namespace EIRLSS_Data_API.ServiceLayer
{
    public class DocumentValidationService
    {
        private readonly DrivingLicenseRepository _drivingLicenseRepository;
        private readonly DirectoryHelper _directoryHelper;

        public DocumentValidationService()
        {
            _directoryHelper = new DirectoryHelper();
            _drivingLicenseRepository = new DrivingLicenseRepository(new ApplicationDbContext());
        }

        public void ImportLicenses()
        {
            var configuredPath = _directoryHelper.GetLatestRecord().DvlaImportPath;
            var importFileName = "dvla.csv";
            var filePath = Path.Combine(configuredPath, importFileName);

            if (!Directory.Exists(configuredPath))
            {
                CreateDirectory(configuredPath);
            }

            if (File.Exists(filePath))
            {
                _drivingLicenseRepository.Truncate();

                Regex CSVSplitter = new Regex(",(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))");

                foreach (var entry in File.ReadAllLines(filePath).Skip(1).Select(line => CSVSplitter.Split(line)))
                {
                    var license = new DrivingLicense
                    {
                        Id = 0,
                        LicenseNumber = entry[0],
                        FamilyName = entry[1],
                        Forenames = entry[2],
                        DateOfBirth = DateTime.Parse(entry[3]),
                        YearOfIssue = DateTime.Parse(entry[4]),
                        Expires = DateTime.Parse(entry[5]),
                        IssuingAuthority = entry[6],
                        Address = entry[7].Trim('"'),
                        Status = entry[8],
                        Date = DateTime.Parse(entry[9])
                    };

                    _drivingLicenseRepository.Insert(license);
                    _drivingLicenseRepository.Save();
                }

                File.Delete(filePath);
            }
        }

        public bool CheckDvlaImport(string licenseNumber)
        {
            ImportLicenses();
            var existingLicense = _drivingLicenseRepository.GetDrivingLicenses()
                .SingleOrDefault(x => x.LicenseNumber == licenseNumber);

            if (existingLicense != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public ServiceResponse CheckAbiImport(string familyName, string forenames, string address)
        {
            OleDbConnection connection = ConnectToAbiDatabase();

            try
            {
                connection.Open();
                var count = 0;
                OleDbCommand command =
                    new OleDbCommand(
                        $"SELECT COUNT(*) from fraudulent_claim_data WHERE FAMILY_NAME='{familyName}' AND FORENAMES='{forenames}' AND ADDRESS_OF_CLAIM='{address}'", connection);
                OleDbDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    count = (int)reader[0];
                }

                if (count > 0)
                {
                    return new ServiceResponse {Success = true, ErrorMessage = null};
                }
                else
                {
                    return new ServiceResponse {Success = false, ErrorMessage = null};
                }
            }
            catch (Exception ex)
            {
                return new ServiceResponse {Success = false, ErrorMessage = ex.ToString()};
            }
            finally
            {
                connection.Close();
            }
        }



        private OleDbConnection ConnectToAbiDatabase()
        {
            var configuredPath = _directoryHelper.GetLatestRecord().AbiImportPath;
            var importFileName = "ABI_DRIVER_FRAUD.accdb";
            var filePath = Path.Combine(configuredPath, importFileName);


            if (!Directory.Exists(configuredPath))
            {
                CreateDirectory(configuredPath);
            }

            if (File.Exists(filePath))
            {
                OleDbConnection connection = new OleDbConnection();

                connection.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;" + $@"Data source={filePath}";

                return connection;
            }
            else
            {
                return null;
            }
        }

        private void CreateDirectory(string filePath)
        {
            Directory.CreateDirectory(filePath);
        }

    }
}