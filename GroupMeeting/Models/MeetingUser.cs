using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GroupMeeting.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GroupMeeting.Models
{
    public class MeetingUser
    {
        public int MeetingID { get; set; }
        public Meeting Meeting { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
    }
}
