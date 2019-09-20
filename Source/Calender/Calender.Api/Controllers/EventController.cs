using System;
using Calender.Domain.Commands;
using Microsoft.AspNetCore.Mvc;

namespace Calender.Api.Controllers
{
    [Route("api/[controller]")]
    public class EventController : Controller
    {
        readonly ICommandHandler<AddEventCommand> add;
        readonly ICommandHandler<DeleteEventCommand> delete;

        public EventController(
            ICommandHandler<AddEventCommand> add,
            ICommandHandler<DeleteEventCommand> delete)
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

            this.add.Handle(command); // no feedback

            return Ok();
        }

        [HttpDelete]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult Delete([FromQuery] DeleteEventCommand command)
        {
            if (command == null) return BadRequest("Cannot read command");

            this.delete.Handle(command); // no feedback
            
            return Ok();
        }
    }
}
