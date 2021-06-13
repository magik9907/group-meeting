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
    public class IndexModel : PageModel
    {
        private readonly GroupMeetingContext _context;
        private readonly UserManager<User> _userManager;
        [BindProperty]
        public User User2 { get; set; }
        public IndexModel(Data.GroupMeetingContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IList<Meeting> Meeting { get; set; }

        public async Task OnGetAsync(int? id)
        {
            Meeting = await _context.Meetings
                .Where(x => x.Group_id == id || id == null)
                .Include(x => x.Group).ToListAsync();
            ClaimsPrincipal currentUser = this.User;
            User2 = await _userManager.GetUserAsync(currentUser);
        }
    }
}
