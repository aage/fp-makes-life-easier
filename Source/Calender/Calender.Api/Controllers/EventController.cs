using System;
using Calender.Domain.Commands;
using LaYumba.Functional;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Calender.Api.Controllers
{
    [Route("api/[controller]")]
    public class EventController : Controller
    {
        readonly Func<AddEventCommand, Validation<Event>> add;
        readonly Func<DeleteEventCommand, Validation<Event>> delete;

        public EventController(
            Func<AddEventCommand, Validation<Event>> add,
            Func<DeleteEventCommand, Validation<Event>> delete)
        {
            this.add = add;
            this.delete = delete;
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult Post(AddEventCommand command)
        {
            if (command == null) return BadRequest("Cannot read command");

            var val = this.add(command);
            return val.Match<IActionResult>(
                (errors) => BadRequest(errors.Select(e => e.Message)),
                (@event) => Ok(@event.Id));
        }

        [HttpDelete]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult Delete([FromQuery] DeleteEventCommand command)
        {
            if (command == null) return BadRequest("Cannot read command");

            var val = this.delete(command);
            return val.Match<IActionResult>(
                (errors) => BadRequest(errors.Select(e => e.Message)),
                (_) => Ok());
        }
    }
}
