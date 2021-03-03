using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EIRLSS_Data_API.Common;

namespace EIRLSS_Data_API.Controllers
{
    [AllowAnonymous]
    public class ErrorController : Controller
    {

        public ActionResult Error(ErrorType errorType, string message)
        {
            ViewBag.ErrorType = errorType;
            ViewBag.Message = message;
            return View();
        }
    }
}