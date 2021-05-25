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
    public class IndexModel : PageModel
    {
        private readonly GroupMeeting.Data.GroupMeetingContext _context;

        public IndexModel(GroupMeeting.Data.GroupMeetingContext context)
        {
            _context = context;
        }

        public IList<Group> Group { get;set; }

        public async Task OnGetAsync()
        {
            Group = await _context.Groups
                .Include(a => a.Owner).ToListAsync();
        }
    }
}
