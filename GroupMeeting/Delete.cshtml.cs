using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GroupMeeting.Data;
using GroupMeeting.Models;

namespace GroupMeeting
{
    public class DeleteModel : PageModel
    {
        private readonly GroupMeeting.Data.GroupMeetingContext _context;

        public DeleteModel(GroupMeeting.Data.GroupMeetingContext context)
        {
            _context = context;
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
                .Include(a => a.Owner).FirstOrDefaultAsync(m => m.ID == id);

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

            Group = await _context.Groups.FindAsync(id);

            if (Group != null)
            {
                _context.Groups.Remove(Group);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
