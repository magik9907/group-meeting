using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GroupMeeting.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Identity;
using GroupMeeting.Areas.Identity.Data;
using GroupMeeting.Data;
using System.Security.Claims;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace GroupMeeting.Pages.Meetings
{
    public class JoinModel : PageModel
    {
        private readonly GroupMeetingContext _context;
        private readonly UserManager<User> _userManager;

        public JoinModel(GroupMeetingContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [BindProperty]
        public Meeting Meeting { get; set; }
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
            User2 = await _userManager.GetUserAsync(HttpContext.User);

            var meeting = _context.Meetings
                .Where(x => x.ID == id && x.UserCounter < x.UserMaxLimit && !x.MeetingUsers.Any(x => x.MeetingID == id && x.UserId == User2.Id))
                .FirstOrDefault();


            if (meeting != null)
            {
                meeting.UserCounter++;
                _context.Meetings.Update(meeting);
                this._context.MeetingUser.Add(new MeetingUser() {UserId=User2.Id, MeetingID=meeting.ID });
                await this._context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}