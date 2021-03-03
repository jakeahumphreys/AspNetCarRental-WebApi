using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EIRLSS_Data_API.Models;

namespace EIRLSS_Data_API.DAL
{
    public interface IDrivingLicenseRepository : IDisposable
    {
        IList<DrivingLicense> GetDrivingLicenses();
        DrivingLicense GetDrivingLicenseById(int id);
        void Insert(DrivingLicense DrivingLicense);
        void Update(DrivingLicense DrivingLicense);
        void Delete(DrivingLicense DrivingLicense);
        void Save();
        void Truncate();
    }
}