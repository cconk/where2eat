using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using where2eat.Models;
using where2eat.ViewModels;
using where2eat.Services;
using where2eat.Data;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace where2eat.API
{
    [Route("api/[controller]")]
    public class EventsController : Controller
    {
        private EventsService _eventService;
        public EventsController(EventsService eventService)
        {
            _eventService = eventService;
        }

        // GET: api/values
        [HttpGet]
        public IList<EventVM> Get()
        {
            return _eventService.ListAllEvents();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public IList<EventVM> Get(string id)
        {
            return _eventService.ListAllEventsByOrganizer(id);
        }

        // POST api/values
        [HttpPost("{id}")]
        public IActionResult Post(string id, [FromBody]Event newEvent)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(this.ModelState);
            }

            if (newEvent.EventName == "")
            {
                return NotFound();
            }

            else
            {
                _eventService.AddNewEvent(id, newEvent);
                return Ok(newEvent);
            }

        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(this.ModelState);

            }

            else
            {
                _eventService.DeleteEvent(id);
                return Ok();
            }
        }
    }
}
