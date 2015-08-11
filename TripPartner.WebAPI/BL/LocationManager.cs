using System;
using System.Collections.Generic;
using System.Data.Entity.Spatial;
using System.Globalization;
using System.Linq;
using System.Web;
using TripPartner.WebAPI.Binding_Models;
using TripPartner.WebAPI.Data;
using TripPartner.WebAPI.Domain_Models;
using TripPartner.WebAPI.Exceptions;

namespace TripPartner.WebAPI.BL
{
    public class LocationManager
    {
        private ApplicationDbContext _db;
        public LocationManager(ApplicationDbContext db)
        {
            _db = db;
        }

        public LocationVM Add(NewLocVM loc) {
            Location l = getLocation(loc);
            if (l == null)
             l =  _db.Locations.Add(new Location
                {
                    Address = loc.Address,
                    LatLng = CreatePoint(loc.Lat, loc.Long)
                });
            return new LocationVM
            {
                Id = l.Id,
                Address = l.Address,
                Lat = l.LatLng.Latitude.Value,
                Long = l.LatLng.Longitude.Value
            };
        }

        public bool DeleteById(int id)
        {
            try
            {
                Location loc = _db.Locations.Single(l => l.Id == id);
                _db.Locations.Remove(loc);
                return true;
            }
            catch (Exception)
            {
                throw new LocationNotFoundException(id);
            }
        }

        public LocationVM GetById(int id)
        {
            LocationVM loc = (from l in _db.Locations
                             where l.Id == id
                             select new LocationVM{
                                 Id = l.Id,
                                 Address = l.Address,
                                 Lat = l.LatLng.Latitude.Value,
                                 Long = l.LatLng.Longitude.Value
                             }).ToList()[0];
            if (loc == null)
                throw new LocationNotFoundException(id);
            return loc;
        }

        public int GetNumberOfLocations()
        {
            return _db.Locations.Count(l => 1 == 1);
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