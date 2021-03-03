using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EIRLSS_Data_API.Common;
using EIRLSS_Data_API.Models;
using EIRLSS_Data_API.ServiceLayer;
using Microsoft.Owin.Security.Facebook;

namespace EIRLSS_Data_API.Controllers
{
    public class ConfigurationRecordController : Controller
    {
        private readonly ConfigurationService _configurationService;

        public ConfigurationRecordController()
        {
            _configurationService = new ConfigurationService();
        }

        public ActionResult Index()
        {
            return View(_configurationService.GetAll());
        }

        public ActionResult Details(int id)
        {
            try
            {
                return View(_configurationService.GetDetails(id));
            }
            catch (ArgumentException ex)
            {
                return RedirectToAction("Error", "Error", new { errorType = ErrorType.HTTP, message = ex.Message });
            }
            catch (ConfigurationNotFoundException ex)
            {
                return RedirectToAction("Error", "Error", new { errorType = ErrorType.HTTP, message = ex.Message });
            }
        }

        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ConfigurationRecord configuration)
        {
            ServiceResponse response = _configurationService.CreateAction(configuration);

            if (response.Success == true)
            {
                return RedirectToAction("Index", "ConfigurationRecord");
            }
            else
            {
                return RedirectToAction("Error", "Error", new { errorType = ErrorType.System, message = "Error" });
            }
        }

        public ActionResult Edit(int id)
        {
            try
            {
                return View(_configurationService.EditView(id));
            }
            catch (ArgumentException ex)
            {
                return RedirectToAction("Error", "Error", new { errorType = ErrorType.HTTP, message = ex.Message });
            }
            catch (ConfigurationNotFoundException ex)
            {
                return RedirectToAction("Error", "Error", new { errorType = ErrorType.HTTP, message = ex.Message });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ConfigurationRecord configuration)
        {
            ServiceResponse response = _configurationService.EditAction(configuration);

            if (response.Success == true)
            {
                return RedirectToAction("Index", "ConfigurationRecord", null);
            }
            else
            {
                return View();
            }
        }

        public ActionResult Delete(int id)
        {
            try
            {
                return View(_configurationService.DeleteView(id));
            }
            catch (ArgumentException ex)
            {
                return RedirectToAction("Error", "Error", new { errorType = ErrorType.HTTP, message = ex.Message });
            }
            catch (ConfigurationNotFoundException ex)
            {
                return RedirectToAction("Error", "Error", new { errorType = ErrorType.HTTP, message = ex.Message });
            }
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ServiceResponse response = _configurationService.DeleteAction(id);

            if (response.Success == true)
            {
                return RedirectToAction("Index", "ConfigurationRecord", null);
            }
            else
            {
                return View();
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _configurationService.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
