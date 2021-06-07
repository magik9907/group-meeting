using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using GroupMeeting.Areas.GroupCategories.Models;
using System.ComponentModel.DataAnnotations;

namespace GroupMeeting.Areas.GroupCategories.Pages.Category
{
    public class EditModel : PageModel
    {
        public class Category
        {
            [Required]
            public string Name { get; set; }
            [Required]
            public int Id { get; set; }
        }

        private readonly GroupMeeting.Data.GroupMeetingContext _context;
        [BindProperty]
        public Category newCategory { get; set; }
        public EditModel(GroupMeeting.Data.GroupMeetingContext context)
        {
            _context = context;
        }

        public IActionResult OnGet(int? id)
        {
            Models.Category cat;
            if (id == null) return NotFound();
            cat = _context.Categories.FirstOrDefault(x => x.Id == id);
            newCategory = new Category() { Id = cat.Id, Name = cat.Name };
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (!ModelState.IsValid)
                return Page();
            Models.Category category = await _context.Categories.FindAsync(id);
            if (await TryUpdateModelAsync<Models.Category>(category, "newCategory", x => x.Name))
            {
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }

            return Page();
        }
    }
}
