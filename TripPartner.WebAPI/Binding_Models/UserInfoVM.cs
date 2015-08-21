using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace TripPartner.WebAPI.Binding_Models
{
    public class UserInfoVM
    {
        [DisplayName("Email")]
        public string Email { get; set; }
        [DisplayName("Username")]
        public string Username { get; set; }
        [DisplayName("Id")]
        public string Id { get; set; }

    }
}