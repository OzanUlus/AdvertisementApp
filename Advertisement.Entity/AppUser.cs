﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvertisementApp.Entity
{
    public class AppUser : BaseEntity
    {
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }

        public IEnumerable<AppUserRole> AppUserRoles { get; set; }
        public int GenderId { get; set; }
        public Gender Gender { get; set; }

        public IEnumerable<AdvertisementAppUser> AdvertisementAppUsers { get; set; }
    }
}
