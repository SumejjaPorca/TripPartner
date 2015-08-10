using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TripPartner.WebAPI.Exceptions
{
    public class StoryNotFoundException : Exception
    {
        public StoryNotFoundException()
            : base("Story was not found.")
        {

        }

        public StoryNotFoundException(int id)
            : base("Story with id: " + id.ToString() + " was not found.")
        {

        }
    }
}