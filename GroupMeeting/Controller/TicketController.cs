using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GroupMeeting.Models;
using Newtonsoft.Json;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GroupMeeting.Controller
{
    [Microsoft.AspNetCore.Authorization.Authorize]
    [Route("api/ticket")]
    [ApiController]
    public class TicketController : ControllerBase
    {
      

        // GET api/ticket/[id]
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return JsonConvert.SerializeObject(new Ticket()
            {
                Name = "John Johnson",
                Date = "24.05.2020 14:40",
                Id = id.ToString(),
                MeetingName = "To jest spotkanie pobrane z api",
                GroupName = "To jest bardzo długa nazwa grupy",
                Address = "Białystpk"
            });
        }

      
    }
}
