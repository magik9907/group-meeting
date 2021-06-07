﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

using GroupMeeting.Models;
using GroupMeeting.Data;

namespace GroupMeeting.Pages.Meetings
{
    public class DetailsModel : PageModel
    {
        private readonly GroupMeetingContext _context;

        public DetailsModel(GroupMeetingContext context)
        {
            _context = context;
        }

        public Meeting Meeting { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Meeting = await _context.Meetings.Include(x => x.Group).FirstOrDefaultAsync(m => m.ID == id);

            if (Meeting == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
