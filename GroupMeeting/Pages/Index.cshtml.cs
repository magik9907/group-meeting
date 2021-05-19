using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GroupMeeting.Services;
using SendGrid;
using SendGrid.Helpers.Mail;
using Microsoft.AspNetCore.Identity.UI.Services;


namespace GroupMeeting.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IEmailSender _emailSender;

        public IndexModel(ILogger<IndexModel> logger, IEmailSender emailSender)
        {
            _logger = logger;
            _emailSender = emailSender;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            await _emailSender.SendEmailAsync("kamilewski@yahoo.com", "wat", "t");

            return Page();
        }
    }
}
