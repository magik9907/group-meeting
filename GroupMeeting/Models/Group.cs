using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GroupMeeting.Areas.Identity.Data;
using GroupMeeting.Areas.GroupCategories.Models;
namespace GroupMeeting.Models
{
    public class Group
    {
        [Required]
        public int ID { get; set; }
        [MaxLength(200), Required]
        public string Name { get; set; }
        [MaxLength(1000)]
        public string Description { get; set; }
        public City City { get; set; }
        public ICollection<GroupUser> GroupUsers { get; set; }
        public string OwnerID { get; set; }
        public User Owner { get; set; }
        public IList<GroupCategory> GroupCategories { get; set; }
    }
}
