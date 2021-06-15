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
    public class DetailsModel : PageModel
    {
        private readonly GroupMeetingContext _context;
        private readonly UserManager<User> _userManager;
        public DetailsModel(GroupMeetingContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public List<Meeting> Meetings { get; set; }
        public Group Group { get; set; }
        public string userID;
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            if(user != null)
                userID = user.Id;
            if (id == null)
            {
                return NotFound();
            }

            Group = await _context.Groups
                                    .Include(a => a.Owner)
                                    .Include(g=>g.GroupUsers).ThenInclude(gu=>gu.User)
                                    .Include(e=>e.GroupCategories).ThenInclude(e=>e.Category)
                                    .FirstOrDefaultAsync(m => m.ID == id);
            if (Group == null)
            {
                return NotFound();
            }
            Meetings = await _context.Meetings.Where(x => x.Group == Group).ToListAsync();
          //  Group.City = _context.Cities.FirstOrDefault(c => c.ID == Group.CityID);
            return Page();
        }
    }
}
