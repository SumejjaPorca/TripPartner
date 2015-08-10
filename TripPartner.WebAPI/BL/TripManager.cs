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
        public TripManager(ApplicationDbContext db)
        {
            _db = db;
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
                    LatLng = CreatePoint(trip.Destination.Lat, trip.Destination.Long)
                });

            if (origin == null)
                origin = _db.Locations.Add(new Location
                {
                    LatLng = CreatePoint(trip.Origin.Lat, trip.Origin.Long)
                });


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
                Destination = new LocationVM { Id = dest.Id, Lat = dest.LatLng.Latitude.Value, Long = dest.LatLng.Longitude.Value },
                Origin = new LocationVM { Id = origin.Id, Lat = origin.LatLng.Latitude.Value, Long = origin.LatLng.Longitude.Value },
                CreatorId = user.Id,
                CreatorUsername = user.UserName,
                DateEnded = t.DateEnded,
                DateStarted = t.DateStarted
            };
        }

        private ApplicationUser getUser(string id)
        {
            var user = _db.Users.FirstOrDefault(u => u.Id == id);
            if (user == null)
                throw new UserNotFoundException(id);
            return user;
        }

        private Location getLocation(NewLocVM loc)
        {
            return _db.Locations.FirstOrDefault(l => l.LatLng.Latitude == loc.Lat && l.LatLng.Longitude == loc.Long);
        }

        public DbGeography CreatePoint(double latitude, double longitude)
        {
            var text = string.Format(CultureInfo.InvariantCulture.NumberFormat, "POINT({0} {1})", longitude, latitude);
            // 4326 is most common coordinate system used by GPS/Maps
            return DbGeography.PointFromText(text, 4326);


        }
    }
}