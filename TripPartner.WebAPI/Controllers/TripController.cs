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

namespace TripPartner.WebAPI.Controllers
{
    [RoutePrefix("api/Trip")]

    public class TripController : ApiController
    {
        private ApplicationDbContext _db;
        private TripManager _mngr;

        public TripController()
        {
            _db = new ApplicationDbContext();
            _mngr = new TripManager(_db);
        }

        // GET api/Trip/ById/6
        [Route("ById")]
        [AllowAnonymous]
        [HttpGet]
        public IHttpActionResult ById(int id)
        {
            IHttpActionResult response;
            HttpResponseMessage responseMsg;

            try
            {
                TripVM trip = _mngr.getById(id);
                responseMsg = CreateTripResponseMsg(trip, HttpStatusCode.OK);
                responseMsg.Headers.CacheControl.NoCache = true;

            }
            catch (Exception e)
            {
                responseMsg = CreateCustomErrResponseMsg(new HttpError(e.Message), HttpStatusCode.BadRequest);
            }
            response = ResponseMessage(responseMsg);

            return response;
        }

        [Route("ById")]
        [HttpGet]
        [AllowAnonymous]
        // GET api/Trip/ByUserId/dsfs1f
        public IHttpActionResult ByUserId(string id)
        {
            IHttpActionResult response;
            HttpResponseMessage responseMsg;

            try
            {
                List<TripVM> trips = _mngr.getByUserId(id);
                responseMsg = CreateTripsResponseMsg(trips, HttpStatusCode.OK);
                responseMsg.Headers.CacheControl.NoCache = true;

            }
            catch (Exception e)
            {
                responseMsg = CreateCustomErrResponseMsg(new HttpError(e.Message), HttpStatusCode.BadRequest);
            }
            response = ResponseMessage(responseMsg);
            return response;
        }

        // POST api/Trip/Add
        [Route("ById")]
        [Authorize]
        [HttpPost]
        public IHttpActionResult Add(NewTripVM trip)
        {
            IHttpActionResult response;
            HttpResponseMessage responseMsg;

            try
            {
                string creatorId = Microsoft.AspNet.Identity.IdentityExtensions.GetUserId(RequestContext.Principal.Identity);
                trip.CreatorId = creatorId;
                TripVM t = _mngr.NewTrip(trip);
                return Ok();

            }
            catch (Exception e)
            {
                responseMsg = CreateCustomErrResponseMsg(new HttpError(e.Message), HttpStatusCode.BadRequest);
            }
            response = ResponseMessage(responseMsg);
            

            return response;
        }


        //TO DO: turn the following method to generic method
        private HttpResponseMessage CreateTripsResponseMsg(List<TripVM> trips, HttpStatusCode httpStatusCode)
        {
            IContentNegotiator negotiator = this.Configuration.Services.GetContentNegotiator();
            ContentNegotiationResult result = negotiator.Negotiate(typeof(List<TripVM>), this.Request,
                this.Configuration.Formatters);
            if (result == null)
            {
                var response = new HttpResponseMessage(HttpStatusCode.NotAcceptable);
                throw new HttpResponseException(response);
            }
            return new HttpResponseMessage()
            {
                Content = new ObjectContent<List<TripVM>>(
                    trips,		        // What we are serializing 
                    result.Formatter,           // The media formatter
                    result.MediaType.MediaType  // The MIME type
                ),
                StatusCode = httpStatusCode
            };
        }


        private HttpResponseMessage CreateTripResponseMsg(TripVM trip, HttpStatusCode code)
        {
            IContentNegotiator negotiator = this.Configuration.Services.GetContentNegotiator();
            ContentNegotiationResult result = negotiator.Negotiate(typeof(TripVM), this.Request,
                this.Configuration.Formatters);
            if (result == null)
            {
                var response = new HttpResponseMessage(HttpStatusCode.NotAcceptable);
                throw new HttpResponseException(response);
            }
            return new HttpResponseMessage()
            {
                Content = new ObjectContent<TripVM>(
                    trip,		        // What we are serializing 
                    result.Formatter,           // The media formatter
                    result.MediaType.MediaType  // The MIME type
                ),
                StatusCode = code
            };
        }

        private HttpResponseMessage CreateCustomErrResponseMsg(HttpError err, HttpStatusCode code)
        {
            IContentNegotiator negotiator = this.Configuration.Services.GetContentNegotiator();
            ContentNegotiationResult result = negotiator.Negotiate(typeof(HttpError), this.Request,
                this.Configuration.Formatters);
            if (result == null)
            {
                var response = new HttpResponseMessage(HttpStatusCode.NotAcceptable);
                throw new HttpResponseException(response);
            }
            return new HttpResponseMessage()
            {
                Content = new ObjectContent<HttpError>(
                    err,		        // What we are serializing 
                    result.Formatter,           // The media formatter
                    result.MediaType.MediaType  // The MIME type
                ),
                StatusCode = code
            };


        }

        //GET api/values

        public string GetString()
        {
            return "BULLSHITfdgf";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
