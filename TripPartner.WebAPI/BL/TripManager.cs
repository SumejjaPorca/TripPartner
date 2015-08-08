using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TripPartner.WebAPI.Data;
using TripPartner.WebAPI.View_Models;

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
                       where t.Id == id
                       select new TripVM
                       {

                       };
           TripVM trip = query.FirstOrDefault();
           if (trip == null)
               throw new Exception("Trip with id = " + id + " was not found.");

           return trip;
       }
    }
}