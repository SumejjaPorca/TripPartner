using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using TripPartner.WebAPI.Models;
using System.Data.Entity;
using TripPartner.WebAPI.Domain_Models;
using TripPartner.WebAPI.Migrations;


namespace TripPartner.WebAPI.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
        {
            public DbSet<Location> Locations { get; set; }
            public DbSet<Trip> Trips { get; set; }
            public DbSet<Story> Stories { get; set; }
            public ApplicationDbContext()
                : base("DefaultConnection", throwIfV1Schema: false)
            {
                Database.SetInitializer(new MigrateDatabaseToLatestVersion<ApplicationDbContext, Configuration>());
 
            }

            public static ApplicationDbContext Create()
            {
                return new ApplicationDbContext();
            }
        }
    
}