using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GroupMeeting.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Identity;
using GroupMeeting.Areas.Identity.Data;
using System.Security.Claims;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace GroupMeeting.Pages.Meetings
{
    public class LeaveModel : PageModel
    {
        private readonly Data.GroupMeetingContext _context;
        private readonly UserManager<User> _userManager;

        public LeaveModel(Data.GroupMeetingContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [BindProperty]
        public Meeting Meeting { get; set; }
        [BindProperty]
        public User User2 { get; set; }
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Meeting = await _context.Meetings
                                    .Include(a => a.Group)
                                    .ThenInclude(a => a.Owner)
                                    .FirstOrDefaultAsync(m => m.ID == id);
            if (Meeting == null)
            {
                return NotFound();
            }
            User2 = await _userManager.GetUserAsync(HttpContext.User);
            if (User2 == null)
                return RedirectToPage("./Login");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var meetingUser = await _context.MeetingUser.FirstOrDefaultAsync(mu => mu.MeetingID == Meeting.ID && mu.UserId == User2.Id);

            if (meetingUser != null)
            {
                _context.MeetingUser.Remove(new MeetingUser() { Meeting = Meeting, User = User2 });
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}