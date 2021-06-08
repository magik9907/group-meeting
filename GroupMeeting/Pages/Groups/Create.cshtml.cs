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

namespace GroupMeeting
{
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
        ViewData["OwnerID"] = new SelectList(_context.Users, "Id", "Id");
            return Page();
        }
        
        [BindProperty]
        public Group Group { get; set; }
        [BindProperty]
        public City City { get; set; }
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var city = _context.Cities.FirstOrDefault(c => c.Name == City.Name);
            if (city == null)
            {
                city = new City();
                city.Name = City.Name;
                _context.Cities.Add(city);
                await _context.SaveChangesAsync();
            }
            Group.OwnerID = _userManager.GetUserId(HttpContext.User);
            Group.CityID = city.ID;
            Group.City = city;
            var groupOwner = new GroupOwner();
            groupOwner.OwnerID = Group.OwnerID;
            groupOwner.Owner = await _userManager.GetUserAsync(HttpContext.User);
            _context.Groups.Add(Group);
            await _context.SaveChangesAsync();
            groupOwner.GroupID = Group.ID;
            var groupCity = new GroupCity();
            groupCity.CityID = city.ID;
            groupCity.GroupID = Group.ID;
            _context.GroupCity.Add(groupCity);
            _context.GroupOwner.Add(groupOwner);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
