using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TripPartner.WebAPI.Data;
using TripPartner.WebAPI.Binding_Models;

namespace TripPartner.WebAPI.BL
{
    public class StoryManager
    {
        private ApplicationDbContext _db;
        public StoryManager(ApplicationDbContext db)
        {
            _db = db;
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
                                    Address = d.Address,
                                    Lat = d.LatLng.Latitude.Value,
                                    Long = d.LatLng.Longitude.Value
                                },
                                Origin = new LocationVM
                                {
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
                throw new Exception("Story with id = " + id + " was not found.");

            return story;
 
        }

        public List<StoryVM> getByUserId(string id)
        {
            var user = _db.Users.FirstOrDefault(u => u.Id == id);
            if (user == null)
                throw new Exception("User with id = " + id + " was not found.");

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
                                    Address = d.Address,
                                    Lat = d.LatLng.Latitude.Value,
                                    Long = d.LatLng.Longitude.Value
                                },
                                Origin = new LocationVM
                                {
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
    }
}