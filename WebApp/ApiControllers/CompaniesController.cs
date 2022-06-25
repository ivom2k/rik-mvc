using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BLL.Interfaces.App;
using DTO.Public;
using AutoMapper;

namespace WebApp.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {

        private readonly IAppBll _bll;
        private readonly Mappers.PublicEntity.CompanyMapper _mapper;

        public CompaniesController(IAppBll bll, IMapper autoMapper)
        {
            _bll = bll;
            _mapper = new Mappers.PublicEntity.CompanyMapper(autoMapper);
        }

        // GET: api/Companies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Company>>> GetCompanies()
        {
            if (_bll.Companies == null)
            {
                return NotFound();
            }
            return (await _bll.Companies.GetAllAsync()).Select(e => _mapper.Map(e)).ToList()!;
        }

        // GET: api/Companies/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Company>> GetCompany(Guid id)
        {
            if (_bll.Companies == null)
            {
                return NotFound();
            }
            var company = await _bll.Companies.FirstOrDefaultAsync(id);

            if (company == null)
            {
                return NotFound();
            }

            return _mapper.Map(company);
        }

        // PUT: api/Companies/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCompany(Guid id, Company company)
        {
            if (id != company.Id)
            {
                return BadRequest();
            }

            if (await _bll.Companies.ExistsAsync(id) == false)
            {
                return NotFound();
            }

            _bll.Companies.Update(_mapper.Map(company));

            try
            {
                await _bll.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CompanyExists(id))
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

        // POST: api/Companies
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Company>> PostCompany(Company company)
        {
            if (_bll.Companies == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Companies'  is null.");
            }
            
            var newId = _bll.Companies.Add(_mapper.Map(company)).Id;
            await _bll.SaveChangesAsync();

            company.Id = newId;

            return CreatedAtAction("GetCompany", new { id = company.Id }, company);
        }

        // DELETE: api/Companies/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCompany(Guid id)
        {
            if (_bll.Companies == null)
            {
                return NotFound();
            }
            
            var company = await _bll.Companies.FirstOrDefaultAsync(id);
            
            if (company == null)
            {
                return NotFound();
            }

            _bll.Companies.Remove(company);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        private bool CompanyExists(Guid id)
        {
            return _bll.Companies.Exists(id);
        }
    }
}