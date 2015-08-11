using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TripPartner.WebAPI.Exceptions
{
    public class LocationNotFoundException : Exception
    {
           public LocationNotFoundException()
            : base("Location was not found.")
        {

        }

           public LocationNotFoundException(int id)
               : base("Location with id: " + id.ToString() + " was not found.")
        {

        }
    }
}