using System;
using System.Collections.Generic;
using Calender.Domain.Queries;
using LaYumba.Functional;
using Microsoft.AspNetCore.Mvc;

namespace Calender.Api.Controllers
{
    [Route("api/[controller]")]
    public class EventsController : Controller
    {
        readonly Func<Guid, Option<EventModel>> getEvent;
        readonly Func<IEnumerable<EventSummaryModel>> getEvents;

        public EventsController(Func<Guid, Option<EventModel>> getEvent,
            Func<IEnumerable<EventSummaryModel>> getEvents)
        {
            this.getEvent = getEvent;
            this.getEvents = getEvents;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]

        public IActionResult Get(Guid id)
        {
            var @event = this.getEvent(id);
            return @event.Match<IActionResult>(NotFound, Ok);
        }

        [HttpGet]
        [ProducesResponseType(200)]
        public IActionResult Get()
        {
            var events = this.getEvents();
            return Ok(events);
        }
    }
}
