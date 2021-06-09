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
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MeetingID { get; set; }
        [ForeignKey("MeetingID")]
        public Meeting Meeting { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }
    }
}
