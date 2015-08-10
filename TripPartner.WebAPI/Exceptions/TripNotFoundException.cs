using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TripPartner.WebAPI.Exceptions
{
    public class TripNotFoundException : Exception
    {
        public TripNotFoundException()
            : base("Trip was not found.")
        {

        }

        public TripNotFoundException(int id)
            : base("Trip with id: " + id.ToString() + " was not found.")
        {

        }
    }
}