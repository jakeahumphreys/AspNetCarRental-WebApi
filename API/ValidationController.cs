using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EIRLSS_Data_API.DTO;
using EIRLSS_Data_API.ServiceLayer;

namespace EIRLSS_Data_API.API
{
    public class ValidationController : ApiController
    {
        private readonly DocumentValidationService _documentValidationService;

        public ValidationController()
        {
            _documentValidationService = new DocumentValidationService();
        }

        [Route("api/Validation/DVLA")]
        [HttpGet]
        public IHttpActionResult Get(string licenseNumber)
        {
            var isPresent = _documentValidationService.CheckDvlaImport(licenseNumber);

            if (isPresent)
            {
                return Content(HttpStatusCode.Found, "Submitted License has been reported to the DVLA.");
            }
            else
            {
                return Content(HttpStatusCode.NotFound, "Submitted license number has not been reported to the DVLA");
            }
        }

        [Route("api/Validation/ABI")]
        [HttpPost]
        public IHttpActionResult Get([FromBody] AbiRequest request)
        {
            var result =
                _documentValidationService.CheckAbiImport(request.FamilyName, request.Forenames, request.Address);


            if (result.Success)
            {
                return Content(HttpStatusCode.Found, "Submitted individual is present on the ABI database.");
            }
            else
            {
                if (result.ErrorMessage != "")
                {
                    return Content(HttpStatusCode.InternalServerError, result.ErrorMessage);
                }

                return Content(HttpStatusCode.NotFound, "Submitted individual is not present on the ABI database");
            }
        }
    }
}
