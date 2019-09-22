using System;
using Calender.Data;
using Calender.Domain.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Calender.Api.Controllers
{
    [Route("api/[controller]")]
    public class EventsController : Controller
    {
        readonly IEventsQuery getEvents;
        readonly IEventQuery getEvent;

        public EventsController(IEventsQuery getEvents, IEventQuery getEvent)
        {
            this.getEvents = getEvents;
            this.getEvent = getEvent;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]

        public IActionResult Get(Guid id)
        {
            var @event = this.getEvent.Get(id);
            return @event.Match<IActionResult>(NotFound, Ok);
        }

        [HttpGet]
        [ProducesResponseType(200)]
        public IActionResult Get()
        {
            var events = this.getEvents.Get();
            return Ok(events);
        }
    }
}
