using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PersonalLedger.Models
{
    public class AppUser
    {
        public bool Admin { get; set; }
        public string Id { get; set; }
        public string UserName { get; set; }

        public AppUser() { }

        public AppUser(IdentityUser iu)
        {
            UserName = iu.UserName;
            Id = iu.Id;
        }
    }  //class
}  //namespace
