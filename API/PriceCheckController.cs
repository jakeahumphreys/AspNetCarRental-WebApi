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
    public class PriceCheckController : ApiController
    {
        private readonly PriceCheckService _priceCheckService;

        public PriceCheckController()
        {
            _priceCheckService = new PriceCheckService();
        }

        [Route("api/PriceCheck/afford")]
        [HttpPost]
        public IHttpActionResult GetAffordPrice([FromBody] PriceRequest request)
        {
            var result = _priceCheckService.GetAffordPrice(request.VehicleType, request.StartDate, request.EndDate);

            if (result != 0)
            {
                return Json(new PriceResponse {Price = result});
            }
            else
            {
                return Content(HttpStatusCode.InternalServerError, "An error occurred processing your request");
            }
        }
    }
}
