using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http;
using TripPartner.WebAPI.Binding_Models;
using TripPartner.WebAPI.BL;
using TripPartner.WebAPI.Data;
using TripPartner.WebAPI.Providers;

namespace TripPartner.WebAPI.Controllers
{
    [RoutePrefix("api/Location")]

    public class LocationController : ApiController
    {
        private ApplicationDbContext _db;
        private LocationManager _mngr;
        HttpResponseHelper<LocationVM> _helper;
        HttpResponseHelper<HttpError> _errHelper;

        public LocationController()
        {
            _db = new ApplicationDbContext();
            _mngr = new LocationManager(_db);
            _helper = new HttpResponseHelper<LocationVM>(this);
            _errHelper = new HttpResponseHelper<HttpError>(this);
        }

        // GET api/Location/{id:int}
        [Route("{id:int}")]
        [AllowAnonymous]
        [HttpGet]
        public IHttpActionResult GetById(int id)
        {
            IHttpActionResult response;
            HttpResponseMessage responseMsg;

            try
            {
                LocationVM loc = _mngr.GetById(id);
                responseMsg = _helper.CreateCustomResponseMsg(loc, HttpStatusCode.OK);
            }
            catch (Exception e)
            {
                responseMsg = _errHelper.CreateCustomResponseMsg(new HttpError(e.Message), HttpStatusCode.BadRequest);
            }
            response = ResponseMessage(responseMsg);

            return response;
        }


    }
}
