using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GroupMeeting.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GroupMeeting.Models
{
    public class GroupOwner
    {
        public string OwnerID { get; set; }
        public int GroupID { get; set; }
        public Group Group { get; set; }
        public User Owner { get; set; }
    }
}
