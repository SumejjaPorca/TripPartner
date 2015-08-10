using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TripPartner.WebAPI.Exceptions
{
    public class UserNotFoundException : Exception
    {
        public UserNotFoundException()
            : base("User was not found.")
        {

        }

        public UserNotFoundException(string id)
            : base("User with id: " + id + " was not found.")
        {

        }
    }
}