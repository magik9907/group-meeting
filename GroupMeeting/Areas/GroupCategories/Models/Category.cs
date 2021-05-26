using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GroupMeeting.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GroupMeeting.Areas.GroupCategories.Models
{
    public class Category
    {

        public int Id { get; set; }
        [StringLength(100)]
        //[Column(TypeName = "VARCHAR")]
        [Required]
        public string Name { get; set; }
        [Required]
        public string UserId { get; set; }
        public User User { get; set; }
        public IList<GroupCategory> GroupCategories { get; set; }
    }
}
