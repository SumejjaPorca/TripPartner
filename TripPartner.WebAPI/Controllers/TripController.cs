﻿using System;
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
    [RoutePrefix("api/Trip")]

    public class TripController : ApiController
    {
        private ApplicationDbContext _db;
        private TripManager _mngr;
        HttpResponseHelper<TripVM> _helper;
        HttpResponseHelper<HttpError> _errHelper;

        public TripController()
        {
            _db = new ApplicationDbContext();
            _mngr = new TripManager(_db);
            _helper = new HttpResponseHelper<TripVM>(this);
            _errHelper = new HttpResponseHelper<HttpError>(this);
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
                responseMsg = _helper.CreateCustomResponseMsg(trip, HttpStatusCode.OK);
                responseMsg.Headers.CacheControl.NoCache = true;

            }
            catch (Exception e)
            {
                responseMsg = _errHelper.CreateCustomResponseMsg(new HttpError(e.Message), HttpStatusCode.BadRequest);
            }
            response = ResponseMessage(responseMsg);

            return response;
        }

        [Route("ByUserId")]
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
                responseMsg = _helper.CreateCustomResponseMsg(trips, HttpStatusCode.OK);
                responseMsg.Headers.CacheControl.NoCache = true;

            }
            catch (Exception e)
            {
                responseMsg = _errHelper.CreateCustomResponseMsg(new HttpError(e.Message), HttpStatusCode.BadRequest);
            }
            response = ResponseMessage(responseMsg);
            return response;
        }



        // POST api/Trip/Add
        [Route("Add")]
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
                responseMsg = _helper.CreateCustomResponseMsg(t, HttpStatusCode.OK);

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
