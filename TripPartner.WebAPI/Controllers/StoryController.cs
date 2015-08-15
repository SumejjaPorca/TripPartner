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

        // GET api/Story/6
        [Route("{id:int}")]
        [AllowAnonymous]
        [HttpGet]
        public IHttpActionResult GetById(int id)
        {
            IHttpActionResult response;
            HttpResponseMessage responseMsg;

            try
            {
                StoryVM story = _mngr.getById(id);
                responseMsg = _helper.CreateCustomResponseMsg(story, HttpStatusCode.OK);

            }
            catch (Exception e)
            {
                responseMsg = _errHelper.CreateCustomResponseMsg(new HttpError(e.Message), HttpStatusCode.BadRequest);
            }
            response = ResponseMessage(responseMsg);

            return response;
        }

        [Route("~/api/User/{creatorId}/Story")]
        [HttpGet]
        [AllowAnonymous]
        // GET api/User/dsfs1f/Story
        public IHttpActionResult GetByCreatorId(string creatorId)
        {
            IHttpActionResult response;
            HttpResponseMessage responseMsg;

            try
            {
                List<StoryVM> stories = _mngr.getByUserId(creatorId);
                responseMsg = _helper.CreateCustomResponseMsg(stories, HttpStatusCode.OK);

            }
            catch (Exception e)
            {
                responseMsg = _errHelper.CreateCustomResponseMsg(new HttpError(e.Message), HttpStatusCode.BadRequest);
            }
            response = ResponseMessage(responseMsg);
            return response;
        }

        [Route("~/api/Trip/{tripId:int}/Story")]
        [HttpGet]
        [AllowAnonymous]
        // GET api/Trip/423/Story
        public IHttpActionResult GetByTripId(int tripId)
        {
            IHttpActionResult response;
            HttpResponseMessage responseMsg;

            try
            {
                List<StoryVM> stories = _mngr.getByTripId(tripId);
                responseMsg = _helper.CreateCustomResponseMsg(stories, HttpStatusCode.OK);

            }
            catch (Exception e)
            {
                responseMsg = _errHelper.CreateCustomResponseMsg(new HttpError(e.Message), HttpStatusCode.BadRequest);
            }
            response = ResponseMessage(responseMsg);
            return response;
        }

        [Route("{index:alpha}")]
        [AllowAnonymous]
        [HttpGet]
        // GET api/Story/Rating
        public IHttpActionResult GetAll([FromUri]string index) //the biggest gets first
        {
            IHttpActionResult response;
            HttpResponseMessage responseMsg;

            try
            {
                if (index == "")
                    index = "DateMade";
                List<StoryVM> stories = _mngr.getAll(index);
                responseMsg = _helper.CreateCustomResponseMsg(stories, HttpStatusCode.OK);

            }
            catch (Exception e)
            {
                responseMsg = _errHelper.CreateCustomResponseMsg(new HttpError(e.Message), HttpStatusCode.BadRequest);
            }
            response = ResponseMessage(responseMsg);
            return response;
        }


        // POST api/Story
        [Route("")]
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