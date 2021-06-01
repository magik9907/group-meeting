using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GroupMeeting.Data;
using GroupMeeting.Models;

namespace GroupMeeting
{
    public class DetailsModel : PageModel
    {
        private readonly GroupMeeting.Data.GroupMeetingContext _context;

        public DetailsModel(GroupMeeting.Data.GroupMeetingContext context)
        {
            _context = context;
        }

        public Group Group { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Group = await _context.Groups
                                    .Include(a => a.Owner)
                                    .Include(g=>g.GroupUsers).ThenInclude(gu=>gu.User)
                                    .FirstOrDefaultAsync(m => m.ID == id);

            if (Group == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
