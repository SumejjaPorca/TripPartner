using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using TripPartner.WebAPI.Binding_Models;
using TripPartner.WebAPI.BL;
using TripPartner.WebAPI.Data;
using TripPartner.WebAPI.Providers;

namespace TripPartner.WebAPI.Controllers
{
    [RoutePrefix("api/Story")]
    public class StoryController : ApiController
    {
        private ApplicationDbContext _db;
        private StoryManager _mngr;
        HttpResponseHelper<StoryVM> _helper;
        HttpResponseHelper<HttpError> _errHelper;

        public StoryController()
        {
            _db = new ApplicationDbContext();
            _mngr = new StoryManager(_db);
            _helper = new HttpResponseHelper<StoryVM>(this);
            _errHelper = new HttpResponseHelper<HttpError>(this);
        }

        // GET api/Story/ById/6
        [Route("ById")]
        [AllowAnonymous]
        [HttpGet]
        public IHttpActionResult ById(int id)
        {
            IHttpActionResult response;
            HttpResponseMessage responseMsg;

            try
            {
                StoryVM story = _mngr.getById(id);
                responseMsg = _helper.CreateCustomResponseMsg(story, HttpStatusCode.OK);
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
        // GET api/Story/ByUserId/dsfs1f
        public IHttpActionResult ByUserId(string id)
        {
            IHttpActionResult response;
            HttpResponseMessage responseMsg;

            try
            {
                List<StoryVM> stories = _mngr.getByUserId(id);
                responseMsg = _helper.CreateCustomResponseMsg(stories, HttpStatusCode.OK);
                responseMsg.Headers.CacheControl.NoCache = true;

            }
            catch (Exception e)
            {
                responseMsg = _errHelper.CreateCustomResponseMsg(new HttpError(e.Message), HttpStatusCode.BadRequest);
            }
            response = ResponseMessage(responseMsg);
            return response;
        }

        [Route("ByTripId")]
        [HttpGet]
        [AllowAnonymous]
        // GET api/Story/ByTripId/423
        public IHttpActionResult ByTripId(int id)
        {
            IHttpActionResult response;
            HttpResponseMessage responseMsg;

            try
            {
                List<StoryVM> stories = _mngr.getByTripId(id);
                responseMsg = _helper.CreateCustomResponseMsg(stories, HttpStatusCode.OK);
                responseMsg.Headers.CacheControl.NoCache = true;

            }
            catch (Exception e)
            {
                responseMsg = _errHelper.CreateCustomResponseMsg(new HttpError(e.Message), HttpStatusCode.BadRequest);
            }
            response = ResponseMessage(responseMsg);
            return response;
        }


        // POST api/Story/Add
        [Route("Add")]
        [Authorize]
        [HttpPost]
        public IHttpActionResult Add(NewStoryVM story)
        {
            IHttpActionResult response;
            HttpResponseMessage responseMsg;

            try
            {
                string creatorId = Microsoft.AspNet.Identity.IdentityExtensions.GetUserId(RequestContext.Principal.Identity);
                story.CreatorId = creatorId;
                story.DateMade = DateTime.Now;
                story.LastEdit = story.DateMade;
                if (story.Date == null)
                    story.Date = story.DateMade;

                StoryVM s = _mngr.newStory(story);
                responseMsg = _helper.CreateCustomResponseMsg(s, HttpStatusCode.OK);

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