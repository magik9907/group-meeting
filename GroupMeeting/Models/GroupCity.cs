using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace GroupMeeting.Models
{
    public class GroupCity
    {
        public int GroupID { get; set; }
        public int CityID { get; set; }
        public Group Group { get; set; }
        public City City { get; set; }
    }
}
