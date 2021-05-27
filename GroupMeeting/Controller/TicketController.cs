using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GroupMeeting.Data;
using Newtonsoft.Json;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GroupMeeting.Controller
{
    [Route("api/ticket")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        // GET: api/ticket
        /*  [HttpGet]
          public IEnumerable<string> Get()
          {
              return new string[] { "value1", "value2" };
          }*/

        // GET api/ticket/[id]
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return JsonConvert.SerializeObject(new Ticket()
            {
                Name = "John Johnson",
                Date = "24.05.2020 14:40",
                Id = id.ToString(),
                MeetingName ="To jest spotkanie pobrane z api"
            }) ;
        }

        // POST api/ticket
        /*[HttpPost]
        public void Post([FromBody] string value)
        {
        }*/

        // PUT api/ticket/5
        /*  [HttpPut("{id}")]
          public void Put(int id, [FromBody] string value)
          {
          }*/

        // DELETE api/ticket/5
        /* [HttpDelete("{id}")]
         public void Delete(int id)
         {
         }*/
    }
}
