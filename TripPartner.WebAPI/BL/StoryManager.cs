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
                        join t in _db.Trips on s.TripId equals t.Id
                        join o in _db.Locations on t.OriginId equals o.Id
                        join d in _db.Locations on t.DestinationId equals d.Id
                        join u in _db.Users on t.CreatorId equals u.Id
                        join c in _db.Users on s.CreatorId equals c.Id
                        where s.Id == id
                        select new StoryVM
                        {
                            Id = s.Id,
                            Trip = new TripVM
                            {
                                Id = t.Id,
                                Destination = new LocationVM
                                {
                                    Id = d.Id,
                                    Address = d.Address,
                                    Lat = d.LatLng.Latitude.Value,
                                    Long = d.LatLng.Longitude.Value
                                },
                                Origin = new LocationVM
                                {
                                    Id = o.Id,
                                    Address = o.Address,
                                    Lat = o.LatLng.Latitude.Value,
                                    Long = o.LatLng.Longitude.Value
                                },
                                CreatorId = u.Id,
                                CreatorUsername = u.UserName
                            },
                            CreatorId = c.Id,
                            CreatorUsername = c.UserName,
                            Date = s.Date,
                            DateMade = s.DateMade,
                            LastEdit = s.LastEdit,
                            Text = s.Text
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
                        join t in _db.Trips on s.TripId equals t.Id
                        join o in _db.Locations on t.OriginId equals o.Id
                        join d in _db.Locations on t.DestinationId equals d.Id
                        join u in _db.Users on t.CreatorId equals u.Id
                        where s.CreatorId == id
                        select new StoryVM
                        {
                            Id = s.Id,
                            Trip = new TripVM
                            {
                                Id = t.Id,
                                Destination = new LocationVM
                                {
                                    Id = d.Id,
                                    Address = d.Address,
                                    Lat = d.LatLng.Latitude.Value,
                                    Long = d.LatLng.Longitude.Value
                                },
                                Origin = new LocationVM
                                {
                                    Id = o.Id,
                                    Address = o.Address,
                                    Lat = o.LatLng.Latitude.Value,
                                    Long = o.LatLng.Longitude.Value
                                },
                                CreatorId = u.Id,
                                CreatorUsername = u.UserName
                            },
                            CreatorId = id,
                            CreatorUsername = user.UserName,
                            Date = s.Date,
                            DateMade = s.DateMade,
                            LastEdit = s.LastEdit,
                            Text = s.Text
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
                            Id = s.Id,
                            Trip = new TripVM
                            {
                                Id = trip.Id,
                                Destination = new LocationVM
                                {
                                    Id = trip.Destination.Id,
                                    Address = trip.Destination.Address,
                                    Lat = trip.Destination.LatLng.Latitude.Value,
                                    Long = trip.Destination.LatLng.Longitude.Value
                                },
                                Origin = new LocationVM
                                {
                                    Id = trip.Origin.Id,
                                    Address = trip.Origin.Address,
                                    Lat = trip.Origin.LatLng.Latitude.Value,
                                    Long = trip.Origin.LatLng.Longitude.Value
                                },
                                CreatorId = trip.Creator.Id,
                                CreatorUsername = trip.Creator.UserName
                            },
                            CreatorId = s.CreatorId,
                            CreatorUsername = u.UserName,
                            Date = s.Date,
                            DateMade = s.DateMade,
                            LastEdit = s.LastEdit,
                            Text = s.Text
                        };

            return query.ToList();

        }

        public StoryVM newStory(NewStoryVM story)
        {
            var user = getUser(story.CreatorId);
            var trip = getTrip(story.TripId);

            var s = _db.Stories.Add(new Story {
                CreatorId = user.Id,
                TripId = trip.Id,
                DateMade = story.DateMade,
                Date = story.Date,
                LastEdit = story.LastEdit,
                Text = story.Text 
            });

            return new StoryVM {
                Id = s.Id,
                LastEdit = s.LastEdit,
                Date = s.Date,
                DateMade = s.DateMade,
                CreatorId = s.CreatorId,
                CreatorUsername = user.UserName,
                Text = s.Text,
                Trip = new TripVM
                {
                   Id = trip.Id,
                   CreatorId = trip.CreatorId,
                   CreatorUsername = trip.Creator.UserName,
                   DateEnded = trip.DateEnded,
                   DateStarted = trip.DateStarted,
                   Destination = new LocationVM
                    {
                        Id = trip.DestinationId.Value,
                        Address = trip.Destination.Address,
                        Lat = trip.Destination.LatLng.Latitude.Value,
                        Long = trip.Destination.LatLng.Longitude.Value
                    },
                   Origin = new LocationVM
                    {
                        Id = trip.OriginId.Value,
                        Address = trip.Origin.Address,
                        Lat = trip.Origin.LatLng.Latitude.Value,
                        Long = trip.Origin.LatLng.Longitude.Value
                    }
                }
                   
            };
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
                                .Include(t => t.Destination)
                                .Include(t => t.Origin)
                                .FirstOrDefault();
            if (trip == null)
                throw new TripNotFoundException(id);
            return trip;
        }
    }
}