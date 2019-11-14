using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AwesomeBank.Domain;
using AwesomeBank.Infra.DataAccess.Contexts;

namespace AwesomeBank.Application.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountHoldersController : ControllerBase
    {
        private readonly AwesomeBankContext _context;

        //Constructor
        public AccountHoldersController(AwesomeBankContext context)
        {
            _context = context;
        }

        // GET: api/AccountHolders
        [HttpGet]
        public IEnumerable<AccountHolder> GetAccountHolders()
        {
            return _context.AccountHolders;
        }

        // GET: api/AccountHolders/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAccountHolder([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var accountHolder = await _context.AccountHolders.FindAsync(id);

            if (accountHolder == null)
            {
                return NotFound();
            }

            return Ok(accountHolder);
        }

        // PUT: api/AccountHolders/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAccountHolder([FromRoute] Guid id, [FromBody] AccountHolder accountHolder)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != accountHolder.Id)
            {
                return BadRequest();
            }

            _context.Entry(accountHolder).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AccountHolderExists(id))
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

        // POST: api/AccountHolders
        [HttpPost]
        public async Task<IActionResult> PostAccountHolder([FromBody] AccountHolder accountHolder)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.AccountHolders.Add(accountHolder);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAccountHolder", new { id = accountHolder.Id }, accountHolder);
        }

        // DELETE: api/AccountHolders/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccountHolder([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var accountHolder = await _context.AccountHolders.FindAsync(id);
            if (accountHolder == null)
            {
                return NotFound();
            }

            _context.AccountHolders.Remove(accountHolder);
            await _context.SaveChangesAsync();

            return Ok(accountHolder);
        }

        private bool AccountHolderExists(Guid id)
        {
            return _context.AccountHolders.Any(e => e.Id == id);
        }
    }
}