using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GroupMeeting.Data;
using GroupMeeting.Models;
using System.ComponentModel.DataAnnotations;

namespace GroupMeeting
{
    public class IndexModel : PageModel
    {
        public class SearchGroup
        {
            [Required]
            public string Name { get; set; }
        }

        private readonly GroupMeeting.Data.GroupMeetingContext _context;
        [BindProperty]
        public SearchGroup GroupName { get; set; }

        public IndexModel(GroupMeeting.Data.GroupMeetingContext context)
        {
            _context = context;
        }

        public IList<Group> Group { get; set; }

        public async Task OnGetAsync(string? name)
        {
            if (name == null)
                Group = await _context.Groups
                    .Include(a => a.Owner).ToListAsync();
            else
            {
                Group = await _context.Groups.Where(e => e.Name == name).ToListAsync();

                GroupName = new SearchGroup() { Name = name };
            }
        }

        public IActionResult OnPostSearch()
        {
            return Redirect(String.Concat("/Groups?name=", GroupName.Name));
        }
    }
}
