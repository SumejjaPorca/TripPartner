using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TripPartner.WebAPI.Data;
using TripPartner.WebAPI.Binding_Models;
using TripPartner.WebAPI.Domain_Models;
using TripPartner.WebAPI.Models;
using System.Data.Entity.Spatial;
using System.Globalization;
using TripPartner.WebAPI.Exceptions;
using System.Data.Entity;


namespace TripPartner.WebAPI.BL
{
    public class TripManager
    {
        private ApplicationDbContext _db;
        private LocationManager _mngr;
        public TripManager(ApplicationDbContext db)
        {
            _db = db;
            _mngr = new LocationManager(_db);
        }
        public TripVM getById(int id)
        {
            var query = from t in _db.Trips
                        join o in _db.Locations on t.OriginId equals o.Id
                        join d in _db.Locations on t.DestinationId equals d.Id
                        join c in _db.Users on t.CreatorId equals c.Id
                        where t.Id == id
                        select new TripVM
                        {
                            Id = t.Id,
                            DateEnded = t.DateEnded,
                            DateStarted = t.DateStarted,
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
                            CreatorId = c.Id,
                            CreatorUsername = c.UserName
                        };

            TripVM trip = query.FirstOrDefault();
            if (trip == null)
                throw new TripNotFoundException(id);

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
                            CreatorId = id,
                            CreatorUsername = user.UserName,
                            DateEnded = t.DateEnded,
                            DateStarted = t.DateStarted
                        };

            return query.ToList();
        }

        public List<TripVM> getByLocationId(int id)
        {
            var trips = _db.Trips.Where(t => t.DestinationId == id || t.OriginId == id)
                                 .Include(t => t.Creator)
                                 .Include(t => t.Destination)
                                 .Include(t => t.Origin)
                                 .Select(t => new TripVM{
                                      CreatorId = t.CreatorId,
                                      CreatorUsername = t.Creator.UserName,
                                      DateEnded = t.DateEnded,
                                      DateStarted = t.DateStarted,
                                      Destination = new LocationVM{
                                           Address = t.Destination.Address,
                                           Lat = t.Destination.LatLng.Latitude.Value,
                                           Long = t.Destination.LatLng.Longitude.Value
                                      },
                                      Origin = new LocationVM{
                                          Address = t.Origin.Address,
                                          Lat = t.Origin.LatLng.Latitude.Value,
                                          Long = t.Origin.LatLng.Longitude.Value
                                      },
                                      Id = t.Id
                                 }).Distinct().ToList();
                
            return trips;
        }

        public TripVM NewTrip(NewTripVM trip)
        {
            var user = getUser(trip.CreatorId);

            var dest = _mngr.Add(trip.Destination);
            var origin = _mngr.Add(trip.Origin);

            var t = _db.Trips.Add(
               new Trip
               {
                   Creator = user,
                   CreatorId = user.Id,
                   DateEnded = trip.DateEnded,
                   DateStarted = trip.DateStarted,
                   DestinationId = dest.Id,
                   OriginId = origin.Id                    
               });

            _db.SaveChanges();

            return new TripVM
            {
                Id = t.Id,
                Destination = dest,
                Origin = origin,
                CreatorId = user.Id,
                CreatorUsername = user.UserName,
                DateEnded = t.DateEnded,
                DateStarted = t.DateStarted
            };
        }

        public List<TripVM> GetAll()
        {
            var trips = _db.Trips.Include(t => t.Origin)
                            .Include(t => t.Destination)
                            .Select(t => new TripVM{
                                 Destination = new LocationVM{
                                     Address = t.Destination.Address,
                                     Id = t.Destination.Id,
                                     Lat = t.Destination.LatLng.Latitude.Value,
                                     Long = t.Destination.LatLng.Longitude.Value
                                 },
                                 Origin = new LocationVM{
                                     Address = t.Origin.Address,
                                     Id = t.Origin.Id,
                                     Lat = t.Origin.LatLng.Latitude.Value,
                                     Long = t.Origin.LatLng.Longitude.Value
                                 },
                                 Id = t.Id,
                                 CreatorId = t.CreatorId,
                                 CreatorUsername = t.Creator.UserName,
                                 DateEnded = t.DateEnded,
                                 DateStarted = t.DateStarted
                            }).ToList();
            return trips;
        }

        public int GetNumberOfTrips()
        {
            return _db.Trips.Count(t => 1 == 1);
        }

 

        private ApplicationUser getUser(string id)
        {
            var user = _db.Users.FirstOrDefault(u => u.Id == id);
            if (user == null)
                throw new UserNotFoundException(id);
            return user;
        }


        public List<TripVM> getByLatLng(double lat, double lng)
        {
            List<LocationVM> locs = _mngr.getNearest(lat, lng);
            List<int> ids = locs.Select(l => l.Id).ToList();
            if (locs == null)
                return new List<TripVM>();
          

            var trips = _db.Trips.Where(t => ids.Contains(t.DestinationId.Value) || ids.Contains(t.OriginId.Value))
                                .Include(t => t.Creator)
                                .Include(t => t.Destination)
                                .Include(t => t.Origin)
                                .Select(t => new TripVM
                                {
                                    CreatorId = t.CreatorId,
                                    CreatorUsername = t.Creator.UserName,
                                    DateEnded = t.DateEnded,
                                    DateStarted = t.DateStarted,
                                    Destination = new LocationVM
                                    {
                                        Address = t.Destination.Address,
                                        Lat = t.Destination.LatLng.Latitude.Value,
                                        Long = t.Destination.LatLng.Longitude.Value
                                    },
                                    Origin = new LocationVM
                                    {
                                        Address = t.Origin.Address,
                                        Lat = t.Origin.LatLng.Latitude.Value,
                                        Long = t.Origin.LatLng.Longitude.Value
                                    },
                                    Id = t.Id
                                }).Distinct().ToList();

            return trips;
        }
    }
}