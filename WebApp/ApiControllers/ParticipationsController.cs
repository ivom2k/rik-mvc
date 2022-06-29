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
    public class ParticipationsController : ControllerBase
    {
        private readonly IAppBll _bll;
        private readonly Mappers.PublicEntity.ParticipationMapper _mapper;

        public ParticipationsController(IAppBll bll, IMapper autoMapper)
        {
            _bll = bll;
            _mapper = new Mappers.PublicEntity.ParticipationMapper(autoMapper);
        }

        // GET: api/Participations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Participation>>> GetParticipations([FromQuery] string? IdEvent)
        {
            if (_bll.Participations == null)
            {
                return NotFound();
            }
            
            var query = await _bll.Participations.GetAllAsync();
            
            if (IdEvent != null)
            {
                query = query.Where(p => p.EventId.Equals(Guid.Parse(IdEvent)));
            }

            return query.Select(e => _mapper.Map(e)).ToList();
        }

        // GET: api/Participations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Participation>> GetParticipation(Guid id)
        {
            if (_bll.Participations == null)
            {
                return NotFound();
            }
            var participation = await _bll.Participations.FirstOrDefaultAsync(id);

            if (participation == null)
            {
                return NotFound();
            }

            return _mapper.Map(participation);
        }

        // PUT: api/Participations/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutParticipation(Guid id, Participation participation)
        {
            if (id != participation.Id)
            {
                return BadRequest();
            }

            _bll.Participations.Update(_mapper.Map(participation));

            try
            {
                await _bll.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ParticipationExists(id))
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

        // POST: api/Participations
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Participation>> PostParticipation(Participation participation)
        {
            if (_bll.Participations == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Participations'  is null.");
            }
            var newId = _bll.Participations.Add(_mapper.Map(participation)).Id;
            await _bll.SaveChangesAsync();

            participation.Id = newId;

            return CreatedAtAction("GetParticipation", new { id = participation.Id }, participation);
        }

        // DELETE: api/Participations/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteParticipation(Guid id)
        {
            if (_bll.Participations == null)
            {
                return NotFound();
            }
            var participation = await _bll.Participations.FirstOrDefaultAsync(id);
            if (participation == null)
            {
                return NotFound();
            }

            _bll.Participations.Remove(participation);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        private bool ParticipationExists(Guid id)
        {
            return _bll.Participations.Exists(id);
        }
    }
}