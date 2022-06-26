using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DTO.Public;
using AutoMapper;
using BLL.Interfaces.App;

namespace WebApp.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {

        private readonly IAppBll _bll;
        private readonly Mappers.PublicEntity.EventMapper _mapper;

        public EventsController(IAppBll bll, IMapper autoMapper)
        {
            _bll = bll;
            _mapper = new Mappers.PublicEntity.EventMapper(autoMapper);
        }

        // GET: api/Events
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Event>>> GetEvents()
        {
            if (_bll.Events == null)
            {
                return NotFound();
            }
            // return (await _bll.Events.GetAllAsync()).Select(e => _mapper.Map(e)).ToList();

            return (await _bll.Events.GetAllAsync()).Select(e => {
                var valueWithParticipantsCount = _bll.GetEventWithParticipantsCount(e.Id);

                return _mapper.Map(valueWithParticipantsCount.Result);
            }).ToList();
        }

        // GET: api/Events/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Event>> GetEvent(Guid id)
        {
            if (_bll.Events == null)
            {
                return NotFound();
            }
            // var @event = await _bll.Events.FirstOrDefaultAsync(id);
            var @event = await _bll.GetEventWithParticipantsCount(id);

            if (@event == null)
            {
                return NotFound();
            }

            return _mapper.Map(@event);
        }

        // PUT: api/Events/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEvent(Guid id, Event @event)
        {
            if (id != @event.Id)
            {
                return BadRequest();
            }

            _bll.Events.Update(_mapper.Map(@event));

            try
            {
                await _bll.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EventExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Events
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Event>> PostEvent(Event @event)
        {
            if (_bll.Events == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Events'  is null.");
            }
            var newId = _bll.Events.Add(_mapper.Map(@event)).Id;
            await _bll.SaveChangesAsync();

            @event.Id = newId;

            return CreatedAtAction("GetEvent", new { id = @event.Id }, @event);
        }

        // DELETE: api/Events/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvent(Guid id)
        {
            if (_bll.Events == null)
            {
                return NotFound();
            }

            var @event = await _bll.Events.FirstOrDefaultAsync(id);

            if (@event == null)
            {
                return NotFound();
            }

            _bll.Events.Remove(@event);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        private bool EventExists(Guid id)
        {
            return _bll.Events.Exists(id);
        }
    }
}
