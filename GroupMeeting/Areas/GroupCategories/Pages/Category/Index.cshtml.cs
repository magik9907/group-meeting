using System;
using System.Collections.Generic;
using System.Linq;
using GroupMeeting.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using GroupMeeting.Areas.Identity.Data;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace GroupMeeting.Areas.GroupCategories.Pages.Category
{
    [Authorize]
    public class IndexModel : PageModel
    {
        public class Category
        {
            [Required]
            public string Name { get; set; }
        }

        private readonly GroupMeetingContext _context;
        private readonly UserManager<User> _userManager;
        public IList<Models.Category> Categories { get; set; }
        [BindProperty]
        public Category NewCategory { get; set; }
        public string Id;
        public IndexModel(GroupMeetingContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }


        public void OnGet()
        {
            Id = _userManager.GetUserId(this.User);
            Categories = _context.Categories.OrderBy(e => e.UserId).ToList<Models.Category>();
        }

        public async Task<IActionResult> OnPostCreateAsync()
        {
            if(!ModelState.IsValid)
            {
                return Page();
            }
            string name = NewCategory.Name.ToLower();
            var user = await _userManager.GetUserAsync(this.User);
            if (_context.Categories.Any(e => e.Name.ToLower() == name) || !ModelState.IsValid || user == null)
            {
                ViewData["Duplicate"] = "Kategoria ju? istnieje";
                OnGet();
                return Page();
            }

            var category = new Models.Category()
            {
                Name = NewCategory.Name,
                User = user
            };
            _context.Add(category);
            _context.SaveChanges();
            return Redirect("./Category");
        }

        public IActionResult OnPostRemove(int id)
        {
            var category = _context.Categories.FirstOrDefault(e => e.Id == id);
            _context.Categories.Remove(category);
            _context.SaveChanges();
            return Redirect("./Category");
        }
    }
}
