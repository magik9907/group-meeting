using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroupMeeting.Models
{
    public class Ticket
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Date { get; set; }
        public string MeetingName { get; set; }
        public string GroupName { get; set; }
        public string Address { get; set; }
    }
}
