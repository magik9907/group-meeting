using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using GroupMeeting.Data;
using GroupMeeting.Models;
using Microsoft.AspNetCore.Identity;
using GroupMeeting.Areas.Identity.Data;
using Microsoft.AspNetCore.Authorization;

namespace GroupMeeting
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly GroupMeetingContext _context;
        private readonly UserManager<User> _userManager;
        public CreateModel(GroupMeetingContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult OnGet()
        {
            var user = _userManager.GetUserId(HttpContext.User);
            if (_context.Groups.Count(e => e.OwnerID == user) >= 10)
                return RedirectToPage("/Groups/TooManyGroups");
            return Page();
        }

        [BindProperty]
        public Group Group { get; set; }
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            Group.OwnerID = _userManager.GetUserId(HttpContext.User);
            var cityName = Group.City.Name.ToLower();
            var city= _context.Cities.FirstOrDefault(x => x.Name.ToLower() == cityName);
            Group.City =  (city != null)?city:Group.City;
            _context.Groups.Add(Group);
            Group.CreateDate = DateTime.UtcNow;
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
