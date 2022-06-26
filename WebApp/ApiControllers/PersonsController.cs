using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using DTO.Public;
using BLL.Interfaces.App;

namespace WebApp.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonsController : ControllerBase
    {
        private readonly IAppBll _bll;
        private readonly Mappers.PublicEntity.PersonMapper _mapper;

        public PersonsController(IAppBll bll, IMapper autoMapper)
        {
            _bll = bll;
            _mapper = new Mappers.PublicEntity.PersonMapper(autoMapper);
        }

        // GET: api/Persons
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Person>>> GetPersons()
        {
            if (_bll.Persons == null)
            {
                return NotFound();
            }
            
            return (await _bll.Persons.GetAllWithFullNameAsync()).Select(e => _mapper.Map(e)).ToList();
        }

        // GET: api/Persons/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Person>> GetPerson(Guid id)
        {
            if (_bll.Persons == null)
            {
                return NotFound();
            }
            var person = await _bll.Persons.GetFirstOrDefaultWithFullNameAsync(id);

            if (person == null)
            {
                return NotFound();
            }

            return _mapper.Map(person);
        }

        // PUT: api/Persons/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPerson(Guid id, Person person)
        {
            if (id != person.Id)
            {
                return BadRequest();
            }

            _bll.Persons.Update(_mapper.Map(person));

            try
            {
                await _bll.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonExists(id))
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

        // POST: api/Persons
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Person>> PostPerson(Person person)
        {
            if (_bll.Persons == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Persons'  is null.");
            }
            
            var newId = _bll.Persons.Add(_mapper.Map(person)).Id;
            await _bll.SaveChangesAsync();

            person.Id = newId;

            return CreatedAtAction("GetPerson", new { id = person.Id }, person);
        }

        // DELETE: api/Persons/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePerson(Guid id)
        {
            if (_bll.Persons == null)
            {
                return NotFound();
            }
            var person = await _bll.Persons.FirstOrDefaultAsync(id);
            if (person == null)
            {
                return NotFound();
            }

            _bll.Persons.Remove(person);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        private bool PersonExists(Guid id)
        {
            return _bll.Persons.Exists(id);
        }
    }
}
