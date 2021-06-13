using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

using GroupMeeting.Models;
using GroupMeeting.Data;

namespace GroupMeeting.Pages.Meetings
{

    public class CreateModel : PageModel
    {
        private readonly GroupMeetingContext _context;

        public CreateModel(GroupMeetingContext context)
        {
            _context = context;
        }

        public IActionResult OnGet(int id)
        {
            Meeting = new Meeting();
            Meeting.Group_id = id;
            Meeting.Start_Date = DateTime.Now;
            Meeting.Start_Time = DateTime.Now.TimeOfDay;
            return Page();
        }

        [BindProperty]
        public Meeting Meeting { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            DateTime time = Meeting.Start_Date;
            time = time.Add(Meeting.Start_Time);
            if (DateTime.Compare(time, DateTime.Now) < 1)
                return Page();
            _context.Meetings.Add(Meeting);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
