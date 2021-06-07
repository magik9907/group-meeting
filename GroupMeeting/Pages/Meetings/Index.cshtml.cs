using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

using GroupMeeting.Models;
using Microsoft.AspNetCore.Authorization;
using GroupMeeting.Data;

namespace GroupMeeting.Pages.Meetings
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly GroupMeetingContext _context;

        public IndexModel(GroupMeetingContext context)
        {
            _context = context;
        }

        public IList<Meeting> Meeting { get;set; }

        public async Task OnGetAsync()
        {
            Meeting = await _context.Meetings.Include(x => x.Group).ToListAsync();
        }
    }
}
