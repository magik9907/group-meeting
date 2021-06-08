using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GroupMeeting.Data;
using GroupMeeting.Models;
using Microsoft.AspNetCore.Identity;
using GroupMeeting.Areas.Identity.Data;

namespace GroupMeeting
{
    public class DeleteModel : PageModel
    {
        private readonly GroupMeetingContext _context;
        private readonly UserManager<User> _userManager;
        public DeleteModel(GroupMeetingContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [BindProperty]
        public Group Group { get; set; }
        private User user;
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            
            user = await _userManager.GetUserAsync(HttpContext.User);
            if (id == null)
            {
                return NotFound();
            }
            

            Group = await _context.Groups
                .Include(a => a.Owner).FirstOrDefaultAsync(m => m.ID == id);
            if (Group == null)
            {
                return NotFound();
            }
            Group.City = _context.Cities.FirstOrDefault(c => c.ID == Group.CityID);
            if (user == null || user.Id != Group.OwnerID)
                return Redirect("./Details?id=" + id);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Group = await _context.Groups.FindAsync(id);
            var groupOwner = await _context.GroupOwner.FirstAsync(go => go.GroupID == Group.ID && go.OwnerID == Group.OwnerID);
            var groupCity = await _context.GroupCity.FirstAsync(go => go.GroupID == Group.ID && go.CityID == Group.CityID);

            if (Group != null)
            {
                _context.Groups.Remove(Group);
                if(groupOwner != null)
                    _context.GroupOwner.Remove(groupOwner);
                if (groupCity != null)
                    _context.GroupCity.Remove(groupCity);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
