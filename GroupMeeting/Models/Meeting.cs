using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GroupMeeting.Models
{
    public class Meeting
    {
        public int ID { get; set; }
        [ForeignKey("Group")]
        public int Group_id { get; set; }
        public string Name { get; set; }
        [DataType(DataType.Date)]
        public DateTime Start_Date { get; set; }
        [DataType(DataType.Time)]
        public DateTime Start_Time { get; set; }
        public Group Group { get; set; }

        
    }
}
