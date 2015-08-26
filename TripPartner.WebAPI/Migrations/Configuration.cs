namespace TripPartner.WebAPI.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Data.Entity.Spatial;
    using System.Linq;
    using TripPartner.WebAPI.BL;
    using TripPartner.WebAPI.Domain_Models;
    using TripPartner.WebAPI.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<TripPartner.WebAPI.Data.ApplicationDbContext>
    {

        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(TripPartner.WebAPI.Data.ApplicationDbContext context)
        {
              //This method will be called after migrating to the latest version.

            LocationManager mngr = new LocationManager(context);
            string id = "c0587f09-f0fc-4428-b186-efeb0ba70405";
            string text1 = "Last summer I went to the Thousand Islands. We started in the morning. The first city we came to was Rochester. Next came Syracuse, Oswego and Watertown. We stayed at a tourist's camp on the banks of the St. Lawrence River. The next morning we took a boat trip to see the islands. We rode about sixteen miles in and out among them. Most of the islands have beautiful homes surrounded by lovely flower beds that reach to the water's edge. There are eight or nine islands joined together by bridges. These are all owned by one man and they are called Little Venice. We saw the shortest international bridge in the world which connects two islands, one in the United States and the other in Canada territory.";
            string text2 = "We next stopped at Hart Island, which has a large castle on it. The castle is unfinished because the owner's wife died while it was being built. The rooms in the castle are very large and have beautiful ceilings. There is much marble and fine lumber standing unused in this house. On the island, also, is a children's play house, which is like the big castle.";

            Location loc1 = new Location { Address = "Sarajevo", LatLng = mngr.CreatePoint(43.85, 18.25) };
            Location loc2 = new Location { Address = "Whispering Pines, NC, USA", LatLng = mngr.CreatePoint(35.255711, -79.372253) };
            Location loc3 = new Location { Address = "Chinna Kesam Palle, Andhra Pradesh, India", LatLng = mngr.CreatePoint(14.709535, 79.040138) };

            context.Locations.AddOrUpdate(
                loc1,
                loc2,
                loc3);
            Trip trip1 = new Trip { CreatorId = id, DateEnded = DateTime.Now, DateStarted = DateTime.Now, Destination = loc1, Origin = loc2 };
            Trip trip2 = new Trip { CreatorId = id, DateStarted = DateTime.Now, DateEnded = DateTime.Now, Destination = loc2, Origin = loc3 };
            Trip trip3 = new Trip { CreatorId = id, DateEnded = DateTime.Now, DateStarted = DateTime.Now, Destination = loc3, Origin = loc1 };

            context.Trips.AddOrUpdate(
                trip1,
                trip2,
                trip3);

            context.Stories.AddOrUpdate(
                new Story { CreatorId = id, Date = DateTime.Now, DateMade = DateTime.Now, LastEdit = DateTime.Now, Rates = 0, Rating = 0, Text = text1, Title = "Story about wonderful time in London", Trip = trip1 },
                new Story { CreatorId = id, Date = DateTime.Now, DateMade = DateTime.Now, LastEdit = DateTime.Now, Rates = 1, Rating = 10, Text = text2, Trip = trip2, Title = "Crazy party time" },
                new Story { CreatorId = id, Date = DateTime.Now, DateMade = DateTime.Now, LastEdit = DateTime.Now, Rates = 1, Rating = 10, Text = text2, Trip = trip3, Title = "Awesome tour in Europe" },
                new Story { CreatorId = id, Date = DateTime.Now, DateMade = DateTime.Now, LastEdit = DateTime.Now, Rates = 0, Rating = 0, Text = text1, Title = "Story about love", Trip = trip1 },
                new Story { CreatorId = id, Date = DateTime.Now, DateMade = DateTime.Now, LastEdit = DateTime.Now, Rates = 1, Rating = 10, Text = text2, Trip = trip2, Title = "The best trip in my life" },
                new Story { CreatorId = id, Date = DateTime.Now, DateMade = DateTime.Now, LastEdit = DateTime.Now, Rates = 1, Rating = 10, Text = text2, Trip = trip3, Title = "Amazing time in India" }

                );

        }
    }
}