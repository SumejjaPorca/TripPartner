using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TripPartner.WebAPI.Data;
using TripPartner.WebAPI.Binding_Models;
using TripPartner.WebAPI.Exceptions;
using TripPartner.WebAPI.Models;
using TripPartner.WebAPI.Domain_Models;
using System.Data.Entity;

namespace TripPartner.WebAPI.BL
{
    public class StoryManager
    {
        private ApplicationDbContext _db;
        private LocationManager _locMngr;
        private TripManager _tripMngr;

        public StoryManager(ApplicationDbContext db)
        {
            _db = db;
            _locMngr = new LocationManager(_db);
            _tripMngr = new TripManager(_db);
        }

        public StoryVM getById(int id)
        {
            var query = from s in _db.Stories
                        join c in _db.Users on s.CreatorId equals c.Id
                        where s.Id == id
                        select new StoryVM
                        {
                            Title = s.Title,
                            Id = s.Id,
                            TripId = s.TripId.Value,
                            CreatorId = c.Id,
                            CreatorUsername = c.UserName,
                            Date = s.Date,
                            DateMade = s.DateMade,
                            LastEdit = s.LastEdit,
                            Text = s.Text,
                            Rating = s.Rating,
                            Rates = s.Rates
                        };

            StoryVM story = query.FirstOrDefault();
            if (story == null)
                throw new StoryNotFoundException(id);

            return story;

        }

        public List<StoryVM> getByUserId(string id)
        {
            var user = getUser(id);

            var query = from s in _db.Stories
                        where s.CreatorId == id
                        select new StoryVM
                        {
                            Title = s.Title,
                            Id = s.Id,
                            TripId = s.TripId.Value,
                            CreatorId = id,
                            CreatorUsername = user.UserName,
                            Date = s.Date,
                            DateMade = s.DateMade,
                            LastEdit = s.LastEdit,
                            Text = s.Text,
                            Rating = s.Rating,
                            Rates = s.Rates
                        };

            return query.ToList();
        }

        public List<StoryVM> getByTripId(int id)
        {
            var trip = getTrip(id);
            var query = from s in _db.Stories
                        join u in _db.Users on s.CreatorId equals u.Id
                        where s.TripId == id
                        select new StoryVM
                        {
                            Title = s.Title,
                            Id = s.Id,
                            TripId = id,
                            CreatorId = s.CreatorId,
                            CreatorUsername = u.UserName,
                            Date = s.Date,
                            DateMade = s.DateMade,
                            LastEdit = s.LastEdit,
                            Text = s.Text,
                            Rating = s.Rating,
                            Rates = s.Rates
                        };

            return query.ToList();

        }

        public StoryVM newStory(NewStoryVM story)
        {
            var user = getUser(story.CreatorId);
            var trip = getTrip(story.TripId);

            var s = _db.Stories.Add(new Story {
                Title = story.Title,
                CreatorId = user.Id,
                TripId = trip.Id,
                DateMade = story.DateMade,
                Date = story.Date,
                LastEdit = story.LastEdit,
                Text = story.Text,
                Rating = 0,
                Rates = 0
            });

            return new StoryVM {
                Title = story.Title,
                Id = s.Id,
                LastEdit = s.LastEdit,
                Date = s.Date,
                DateMade = s.DateMade,
                CreatorId = s.CreatorId,
                CreatorUsername = user.UserName,
                Text = s.Text,
                Rating = s.Rating,
                Rates = s.Rates,
                TripId = s.TripId.Value
            };
        }

        public List<StoryVM> getAll(string index)
        {
            throw new NotImplementedException();
            //TO DO: this
        }

        private ApplicationUser getUser(string id)
        {
            var user = _db.Users.FirstOrDefault(u => u.Id == id);
            if (user == null)
                throw new UserNotFoundException(id);
            return user;
        }


        private Trip getTrip (int id)
        {
            var trip = _db.Trips.Where(t => t.Id == id) //nice to show another way of loading entities from db :)
                                .Include(t => t.Creator)
                                .FirstOrDefault();
            if (trip == null)
                throw new TripNotFoundException(id);
            return trip;
        }
    }
}