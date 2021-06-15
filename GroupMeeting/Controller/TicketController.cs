using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GroupMeeting.Models;
using Newtonsoft.Json;
using GroupMeeting.Data;
using Microsoft.AspNetCore.Identity;
using GroupMeeting.Areas.Identity.Data;
using Microsoft.EntityFrameworkCore;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GroupMeeting.Controller
{

    [ApiController]
    //  [Microsoft.AspNetCore.Authorization.Authorize]
    [Route("api/ticket")]

    public class TicketController : ControllerBase
    {
        private class Ticket
        {
            public string Name { get; set; }
            public string Date { get; set; }
            public string Id { get; set; }
            public string MeetingName { get; set; }
            public string GroupName { get; set; }
            public string Address { get; set; }
            public bool isOnline { get; set; }
        }

        private readonly GroupMeetingContext _context;
        private readonly UserManager<User> _userManager;
        public TicketController(GroupMeetingContext context, UserManager<User> userManager)
        {
            _userManager = userManager;
            _context = context;
        }
        // GET api/ticket/[id]
        [HttpGet("{id}")]
        public async Task<string> Get(int id)
        {
            if (!User.Identity.IsAuthenticated)
                return "403";
            var meeting = _context.Meetings
                .Include(x => x.Group)
                .Include(x => x.MeetingUsers)
                .FirstOrDefault(x => x.ID == id);
            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (meeting == null || user == null)
                return "404";

            var list = await _context.MeetingUser.Where(x => x.MeetingID == meeting.ID).OrderBy(x => x.UserId).ToListAsync();
            var ticket = new Ticket()
            {
                Name = user.FullName,
                Address = meeting.Localisation,
                Date = meeting.Start_Date.Add(meeting.Start_Time).ToString("yyyy/MM//dd HH:mm"),
                GroupName = meeting.Group.Name,
                MeetingName = meeting.Name,
                Id = String.Concat(
                    meeting.Start_Date.ToString("yy/dd/mm"),
                    "/",
                    meeting.ID,
                    list.IndexOf(new MeetingUser() { UserId = user.Id, MeetingID = meeting.ID }
                    )
                ),
                isOnline = meeting.IsOnline
            };

            return JsonConvert.SerializeObject(ticket);
        }


    }
}
