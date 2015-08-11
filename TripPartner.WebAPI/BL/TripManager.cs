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
                            CreatorUsername = user.UserName
                        };

            return query.ToList();
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
                   OriginId = origin.Id,
               });

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

        public int GetNumberOfTrips()
        {
            return _db.Trips.Count(t => 1 == 1);
        }

        public bool DeleteById(int id)
        {
            try
            {
                Trip trip = _db.Trips.Single(t => t.Id == id);
                _db.Trips.Remove(trip);
                return true;
            }
            catch(Exception)
            {
                throw new TripNotFoundException(id);
            }
        }

        private ApplicationUser getUser(string id)
        {
            var user = _db.Users.FirstOrDefault(u => u.Id == id);
            if (user == null)
                throw new UserNotFoundException(id);
            return user;
        }

    }
}