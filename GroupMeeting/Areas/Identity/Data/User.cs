﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using GroupMeeting.Models;

namespace GroupMeeting.Areas.Identity.Data
{
    public class User : IdentityUser
    {
        [PersonalData, MaxLength(50)]
        public string FirstName { get; set; }
        [PersonalData, MaxLength(50)]
        public string Surname { get; set; }
        public ICollection<GroupUser> GroupUsers { get; set; }
        [MaxLength(10)]
        public ICollection<GroupOwner> GroupOwners { get; set; }
    }
}