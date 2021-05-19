using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace GroupMeeting.Areas.Identity.Data
{
    public class User : IdentityUser
    {
        [PersonalData, MaxLength(50)]
        public string Name { get; set; }
        [PersonalData, MaxLength(50)]
        public string Surname { get; set; }
    }
}
