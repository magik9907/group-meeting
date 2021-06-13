using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GroupMeeting.Data;
using GroupMeeting.Areas.GroupCategories.Models;
using GroupMeeting.Models;
using GroupMeeting.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Identity;

namespace GroupMeeting
{
    public class IndexModel : PageModel
    {
        public class SearchGroup
        {
            public string? Name { get; set; }
            public int? CategoryId { get; set; }
            public string? City { get; set; }
        }

        private readonly GroupMeetingContext _context;
        private readonly UserManager<User> _userManager;
        [BindProperty]
        public SearchGroup GroupName { get; set; }
        public User user;
        public bool QueryValue;
        public List<SelectListItem> CategoriesList { get; set; }
        public IndexModel(GroupMeetingContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IList<Group> Group { get; set; }

        public async Task OnGetAsync(string? name, int? category, string? city, bool? allGroups)
        {
            GroupName = new SearchGroup() { Name = name, CategoryId = category, City = city };
            string? userId = null;
            QueryValue = (allGroups != null) ? (bool)allGroups : false;
            user = await _userManager.GetUserAsync(HttpContext.User);
            if (user != null)
                userId = await _userManager.GetUserIdAsync(user);

            Group = await _context.Groups
                .Where(x => (
                (x.OwnerID == userId || x.GroupUsers.Any(x => x.UserID == userId))
                || user == null
                || QueryValue)
                && (x.Name == name || name == null)
                && (x.GroupCategories.Any(x => x.CategoryId == category) || category == null)
                && (x.City.Name == city || city == null))
                .Include(x => x.GroupCategories)
                .ThenInclude(x => x.Category)
                .Include(x => x.Owner)
                .Include(x => x.City)
                .Include(x => x.GroupUsers)
                .OrderByDescending(x => x.CreateDate)
                .ToListAsync();


            CategoriesList = _context.Categories
                .Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.Name })
                .ToList();
        }

        public IActionResult OnPostSearch()
        {
            return RedirectToPage("/Groups/Index", new { name = GroupName.Name, category = GroupName.CategoryId, city = GroupName.City, allGroups = QueryValue });
        }
    }
}
