using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace GroupMeeting.Models
{
    public class City
    {
        public City(string name)
        {
            Name = name;
        }
        public City()
        { }
        [Required]
        public int ID { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }
        public ICollection<GroupCity> GroupCities { get; set; }
    }
}
