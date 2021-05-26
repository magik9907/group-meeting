using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GroupMeeting.Models;
namespace GroupMeeting.Areas.GroupCategories.Models
{
    public class GroupCategory
    {
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public int GroupId { get; set; }
        public Group Group{ get; set; }
    }
}
