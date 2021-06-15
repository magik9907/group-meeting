using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using GroupMeeting.Areas.GroupCategories.Models;
using GroupMeeting.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace GroupMeeting.Areas.Identity.Data
{
    public class User : IdentityUser
    {
        [PersonalData, MaxLength(50)]
        public string FirstName { get; set; }
        [PersonalData, MaxLength(50)]
        public string Surname { get; set; }

        public IEnumerable<Category> Categories { get; set; }
        public ICollection<GroupUser> GroupUsers { get; set; }
        [MaxLength(10)]
          public ICollection<MeetingUser> MeetingUsers { get; set; }
        [NotMapped]
        public string FullName { get => this.FirstName +" "+ this.Surname; }
    }
}
