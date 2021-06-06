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
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GroupMeeting
{
    public class IndexModel : PageModel
    {
        public class SearchGroup
        {
            [Required]
            public string Name { get; set; }
            public int? CategoryId { get; set; }
        }

        private readonly GroupMeeting.Data.GroupMeetingContext _context;
        [BindProperty]
        public SearchGroup GroupName { get; set; }
        public List<SelectListItem> CategoriesList { get; set; }

        public IndexModel(GroupMeeting.Data.GroupMeetingContext context)
        {
            _context = context;
        }

        public IList<Group> Group { get; set; }

        public async Task OnGetAsync(string? name, int? category)
        {
            if (name == null && category == null)
                Group = await _context.Groups
                    .Include(a => a.Owner).Include(a => a.GroupCategories).ThenInclude(a => a.Category).ToListAsync();
            else
            {
                GroupName = new SearchGroup();
                if (name != null && category != null)
                {
                    Group = await (from c in _context.Groups
                                   where c.GroupCategories.Any(a => a.CategoryId == category)
                                   && c.Name == name
                                   select c).Include(x => x.GroupCategories).ThenInclude(x => x.Category).ToListAsync();
                    GroupName.Name = name;
                    GroupName.CategoryId = category;
                }
                if (name != null)
                {
                    GroupName.Name = name;
                    Group = await _context.Groups.Include(a => a.Owner).Include(a => a.GroupCategories).ThenInclude(a => a.Category).Where(e => e.Name == name).ToListAsync();
                }
                else if (category != null)
                {
                    GroupName.CategoryId = category;
                    Group = await (from c in _context.Groups
                                   where c.GroupCategories.Any(a => a.CategoryId == category)
                                   select c).Include(x => x.GroupCategories).ThenInclude(x => x.Category).ToListAsync();
                }
            }

            CategoriesList = _context.Categories.Select(a =>
                                                            new SelectListItem { Value = a.Id.ToString(), Text = a.Name }
                                                        ).ToList();
        }

        public IActionResult OnPostSearch()
        {
            var str = "/Groups?";
            if (GroupName.Name != "") str += ("name=" + GroupName.Name + "&");
            if (GroupName.CategoryId > -1) str += ("Category=" + GroupName.CategoryId);
            return Redirect(str);
        }
    }
}
