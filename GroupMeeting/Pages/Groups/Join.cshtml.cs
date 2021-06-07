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

namespace GroupMeeting.Pages.Groups
{
    public class JoinModel : PageModel
    {
        private readonly Data.GroupMeetingContext _context;
        private readonly UserManager<User> _userManager;

        public JoinModel(Data.GroupMeetingContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [BindProperty]
        public Group Group { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Group = await _context.Groups
                                    .Include(a => a.Owner)
                                    .FirstOrDefaultAsync(m => m.ID == id);

            if (Group == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Group = await _context.Groups.Include(g => g.GroupUsers).ThenInclude(g=>g.User).FirstOrDefaultAsync(g=>g.ID == id);

            if(Group == null)
            {
                return NotFound();
            }

            ClaimsPrincipal currentUser = this.User;
            var user = await _userManager.GetUserAsync(currentUser);

            if (user != null && !Group.GroupUsers.Any(x => x.User == user))
            {
                Group.GroupUsers.Add(new GroupUser() { Group = Group, User = user });
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
