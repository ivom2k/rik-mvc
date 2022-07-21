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
    public class PaymentTypesController : ControllerBase
    {
        private readonly IAppBll _bll;
        private readonly Mappers.PublicEntity.PaymentTypeMapper _mapper;


        public PaymentTypesController(IAppBll bll, IMapper autoMapper)
        {
            _bll = bll;
            _mapper = new Mappers.PublicEntity.PaymentTypeMapper(autoMapper);
        }

        // GET: api/PaymentTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PaymentType?>>> GetPaymentTypes()
        {
            if (_bll.PaymentTypes == null)
            {
                return NotFound();
            }
            return (await _bll.PaymentTypes.GetAllAsync()).Select(e => _mapper.Map(e)).ToList();
        }

        // GET: api/PaymentTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PaymentType>> GetPaymentType(Guid id)
        {
            if (_bll.PaymentTypes == null)
            {
                return NotFound();
            }
            var paymentType = await _bll.PaymentTypes.FirstOrDefaultAsync(id);

            if (paymentType == null)
            {
                return NotFound();
            }

            return _mapper.Map(paymentType)!;
        }

        // PUT: api/PaymentTypes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPaymentType(Guid id, PaymentType paymentType)
        {
            if (id != paymentType.Id)
            {
                return BadRequest();
            }

            _bll.PaymentTypes.Update(_mapper.Map(paymentType)!);

            try
            {
                await _bll.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PaymentTypeExists(id))
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

        // POST: api/PaymentTypes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PaymentType>> PostPaymentType(PaymentType paymentType)
        {
            if (_bll.PaymentTypes == null)
            {
                return Problem("Entity set 'ApplicationDbContext.PaymentTypes'  is null.");
            }
            
            var newId = _bll.PaymentTypes.Add(_mapper.Map(paymentType)!).Id;
            await _bll.SaveChangesAsync();
            
            paymentType.Id = newId;

            return CreatedAtAction("GetPaymentType", new { id = paymentType.Id }, paymentType);
        }

        // DELETE: api/PaymentTypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePaymentType(Guid id)
        {
            if (_bll.PaymentTypes == null)
            {
                return NotFound();
            }
            var paymentType = await _bll.PaymentTypes.FirstOrDefaultAsync(id);
            if (paymentType == null)
            {
                return NotFound();
            }

            _bll.PaymentTypes.Remove(paymentType);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        private bool PaymentTypeExists(Guid id)
        {
            return _bll.PaymentTypes.Exists(id);
        }
    }
}