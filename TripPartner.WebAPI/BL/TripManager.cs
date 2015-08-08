using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TripPartner.WebAPI.Data;
using TripPartner.WebAPI.Binding_Models;
using TripPartner.WebAPI.Domain_Models;
using TripPartner.WebAPI.Models;
using System.Spatial;

namespace TripPartner.WebAPI.BL
{
    public class TripManager
    {
        private ApplicationDbContext _db;
        public TripManager(ApplicationDbContext db)
        {
            _db = db;
        }
       public TripVM getById(int id){
           var query = from t in _db.Trips
                       join o in _db.Locations on t.OriginId equals o.Id
                       join d in _db.Locations on t.DestinationId equals d.Id
                       join c in _db.Users on t.CreatorId equals c.Id
                       where t.Id == id
                       select new TripVM
                       {
                           Id = t.Id,
                           Destination = new LocationVM { 
                               Address = d.Address,
                               Lat = d.LatLng.Latitude,
                               Long = d.LatLng.Longitude
                           },
                           Origin = new LocationVM
                           {
                               Address = o.Address,
                               Lat = o.LatLng.Latitude,
                               Long = o.LatLng.Longitude
                           },
                           CreatorId = c.Id,
                           CreatorUsername = c.UserName
                       };

           TripVM trip = query.FirstOrDefault();
           if (trip == null)
               throw new Exception("Trip with id = " + id + " was not found.");

           return trip;
       }

       public List<TripVM> getByUserId(string id)
       {
           var user = getUser(id);

           var query = from t in _db.Trips
                       join o in _db.Locations on t.OriginId equals o.Id
                       join d in _db.Locations on t.DestinationId equals d.Id
                       where t.CreatorId == id
                       select new TripVM
                       {
                           Id = t.Id,
                           Destination = new LocationVM
                           {
                               Address = d.Address,
                               Lat = d.LatLng.Latitude,
                               Long = d.LatLng.Longitude
                           },
                           Origin = new LocationVM
                           {
                               Address = o.Address,
                               Lat = o.LatLng.Latitude,
                               Long = o.LatLng.Longitude
                           },
                           CreatorId = id,
                           CreatorUsername = user.UserName
                       };

           return query.ToList();
       }

       public TripVM NewTrip(NewTripVM trip)
       {
           var user = getUser(trip.CreatorId);

           var dest = getLocation(trip.Destination);
           var origin = getLocation(trip.Origin);

           if (dest == null)
             dest = _db.Locations.Add(new Location
               {
                   LatLng = GeographyPoint.Create(trip.Destination.Lat, trip.Destination.Long)
               });

           if(origin == null)
               origin = _db.Locations.Add(new Location{
                   LatLng = GeographyPoint.Create(trip.Origin.Lat, trip.Origin.Long)
               });

         
            var t =  _db.Trips.Add(
               new Trip
               {
                   Creator = user,
                   CreatorId = user.Id,
                   DateEnded = trip.DateEnded,
                   DateStarted = trip.DateStarted,
                   DestinationId = dest.Id,
                   OriginId = origin.Id,
               });
            return new TripVM
            {
                Id = t.Id,
                Destination = new LocationVM{Id = dest.Id, Lat = dest.LatLng.Latitude, Long = dest.LatLng.Longitude},
                Origin = new LocationVM{Id = origin.Id, Lat = origin.LatLng.Latitude, Long = origin.LatLng.Longitude},
                CreatorId = user.Id,
                CreatorUsername = user.UserName,
                DateEnded = t.DateEnded,
                DateStarted = t.DateStarted
            };
       }

        private ApplicationUser getUser(string id){
           var user = _db.Users.FirstOrDefault(u => u.Id == id);
           if (user == null)
               throw new Exception("User with id = " + id + " was not found.");
           return user;
        }

        private Location getLocation(NewLocVM loc)
        {
            return _db.Locations.FirstOrDefault(l => l.LatLng.Latitude == loc.Lat && l.LatLng.Longitude == loc.Long);
        }
    }
}