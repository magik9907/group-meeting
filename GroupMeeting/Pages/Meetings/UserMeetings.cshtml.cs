using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using GroupMeeting.Areas.Identity.Data;
using GroupMeeting.Models;
using Microsoft.AspNetCore.Authorization;
using GroupMeeting.Data;
using System.Security.Claims;

namespace GroupMeeting.Pages.Meetings
{
    [Authorize]
    public class UserMeetingsModel : PageModel
    {
        private readonly GroupMeetingContext _context;
        private readonly UserManager<User> _userManager;
        [BindProperty]
        public User User2 { get; set; }
        public UserMeetingsModel(Data.GroupMeetingContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IList<Meeting> Meeting { get;set; }

        public async Task OnGetAsync()
        {
            Meeting = new List<Meeting>();
            User2 = await _userManager.GetUserAsync(HttpContext.User);
            var meetingUsers = await _context.MeetingUser
                    .Where(x => x.UserId == User2.Id)
                    .ToListAsync();

            foreach (MeetingUser meetingUser in meetingUsers)
            {
                var meeting = await _context.Meetings.Include(x => x.Group).FirstOrDefaultAsync(x => x.ID == meetingUser.MeetingID);
                Meeting.Add(meeting);
            }
            
        }
    }
}
