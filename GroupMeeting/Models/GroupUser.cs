using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GroupMeeting.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations;

namespace GroupMeeting.Models
{
    public class GroupUser
    {
        public string UserID { get; set; }
        public int GroupID { get; set; }
        public Group Group { get; set; }
        public User User { get; set; }
    }
}
