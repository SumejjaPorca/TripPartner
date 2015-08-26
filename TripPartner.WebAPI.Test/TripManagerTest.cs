using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TripPartner.WebAPI.Data;
using TripPartner.WebAPI.Binding_Models;
using TripPartner.WebAPI.Controllers;
using TripPartner.WebAPI.Domain_Models;
using System.Collections.Generic;
using System.Globalization;
using System.Data.Entity.Spatial;
using System.Web.Http;
using System.Net;
using System.Net.Http;
using TripPartner.WebAPI.BL;
using System.Linq;
using TripPartner.WebAPI.Exceptions;
using TripPartner.WebAPI.Models;
using TripPartner.WebAPI.Domain_Models;
using System.Data.Entity;

namespace TripPartner.WebAPI.Test
{
    [TestClass]
    public class TripManagerTest
    {
        private ApplicationDbContext _db;
        private NewTripVM _newTrip;
        private TripManager _mngr;
        private List<Trip> _newlyAddedTrips;
        private Location addedLoc;
        private Trip addedTrip;

        [TestInitialize]
        //TO DO
        public void TestInitialize()
        {
            _db = new ApplicationDbContext();
            _mngr = new TripManager(_db);
            _newlyAddedTrips = new List<Trip>();


            _newTrip = new NewTripVM
            {
                CreatorId = _db.Users.FirstOrDefault().Id,
                DateEnded = DateTime.Now,
                DateStarted = DateTime.Now,
                Destination = new NewLocVM
                {
                    Lat = 43.85,
                    Long = 18.25,
                    Address = "Sarajevo, Bosnia and Herzegovina"
                },
                Origin = new NewLocVM
                {
                    Lat = 43.85,
                    Long = 18.25,
                    Address = "Sarajevo, Bosnia and Herzegovina"
                }
            };

            addedLoc = new Location
                {
                    LatLng = CreatePoint(43.85, 18.25),
                    Address = "Sarajevo, Bosnia and Herzegovina"
                }; 
            _db.Locations.Add(addedLoc);

            _db.SaveChanges();

            addedTrip = new Trip
            {
                DestinationId = addedLoc.Id,
                OriginId = addedLoc.Id,
            };

            _db.Trips.Add(addedTrip);

            _newlyAddedTrips.Add(addedTrip);

            _db.SaveChanges();
        }

        //TO DO
        [TestCleanup]
        public void TestCleanUp()
        {
            foreach (Trip t in _newlyAddedTrips)
            {
                _db.Trips.Remove(t);
            }
           _db.Locations.Remove(addedLoc);
        }

        [TestMethod]
        public void TestById()
        {
           Assert.AreEqual(addedTrip.Id, _mngr.getById(addedTrip.Id).Id);
        }

        [TestMethod]
        public void TestAdd()
        {
           Assert.AreEqual(_mngr.GetNumberOfTrips() + 1, _mngr.NewTrip(_newTrip).Id);
        }

        [TestMethod]
        [ExpectedException(typeof(Exceptions.UserNotFoundException))]
        public void TestByUserId()
        {
            List<TripVM> trips = _mngr.getByUserId("dsgdg");
        }

        public DbGeography CreatePoint(double latitude, double longitude)
        {
            var text = string.Format(CultureInfo.InvariantCulture.NumberFormat, "POINT({0} {1})", longitude, latitude);
            // 4326 is most common coordinate system used by GPS/Maps
            return DbGeography.PointFromText(text, 4326);


        }

       
    }
}
