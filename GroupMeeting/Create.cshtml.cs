using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using GroupMeeting.Data;
using GroupMeeting.Models;

namespace GroupMeeting
{
    public class CreateModel : PageModel
    {
        private readonly GroupMeeting.Data.GroupMeetingContext _context;

        public CreateModel(GroupMeeting.Data.GroupMeetingContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["OwnerID"] = new SelectList(_context.Users, "Id", "Id");
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

            _context.Groups.Add(Group);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
