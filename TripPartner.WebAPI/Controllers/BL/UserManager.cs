using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TripPartner.WebAPI.Binding_Models;
using TripPartner.WebAPI.Data;
using TripPartner.WebAPI.Exceptions;

namespace TripPartner.WebAPI.BL
{
    public class UserManager
    {
        private ApplicationDbContext _db;
        public UserManager(ApplicationDbContext db)
        {
            _db = db;
        }

        public UserInfoVM getUserInfo(string id)
        {
            var user = _db.Users.Where(u => u.Id == id)
                                .FirstOrDefault();
            if (user == null)
                throw new UserNotFoundException(id);
            return new UserInfoVM
            {
                Username = user.UserName,
                Email = user.Email,
                Id = user.Id
            };
        }
    }
}