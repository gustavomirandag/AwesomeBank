using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AwesomeBank.Domain;
using AwesomeBank.Infra.DataAccess.Contexts;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace AwesomeBank.WebApp.Controllers
{
    public class AccountHoldersController : Controller
    {
        private readonly AwesomeBankContext _context;

        public AccountHoldersController(AwesomeBankContext context)
        {
            _context = context;
        }

        // GET: AccountHolders
        public async Task<IActionResult> Index()
        {
            var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://localhost:44389");
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var httpResponseMessage = await httpClient.GetAsync("/api/accountholders");
            IEnumerable<AccountHolder> accountHolders = new List<AccountHolder>();
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                var serializedAccountHolders = await httpResponseMessage.Content.ReadAsStringAsync();
                accountHolders = JsonConvert.DeserializeObject<IEnumerable<AccountHolder>>(serializedAccountHolders);
            }
            return View(accountHolders);
        }

        // GET: AccountHolders/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var accountHolder = await _context.AccountHolders
                .FirstOrDefaultAsync(m => m.Id == id);
            if (accountHolder == null)
            {
                return NotFound();
            }

            return View(accountHolder);
        }

        // GET: AccountHolders/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AccountHolders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Cpf,Name")] AccountHolder accountHolder)
        {
            if (ModelState.IsValid)
            {
                accountHolder.Id = Guid.NewGuid();
                _context.Add(accountHolder);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(accountHolder);
        }

        // GET: AccountHolders/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var accountHolder = await _context.AccountHolders.FindAsync(id);
            if (accountHolder == null)
            {
                return NotFound();
            }
            return View(accountHolder);
        }

        // POST: AccountHolders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Cpf,Name")] AccountHolder accountHolder)
        {
            if (id != accountHolder.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(accountHolder);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AccountHolderExists(accountHolder.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(accountHolder);
        }

        // GET: AccountHolders/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var accountHolder = await _context.AccountHolders
                .FirstOrDefaultAsync(m => m.Id == id);
            if (accountHolder == null)
            {
                return NotFound();
            }

            return View(accountHolder);
        }

        // POST: AccountHolders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var accountHolder = await _context.AccountHolders.FindAsync(id);
            _context.AccountHolders.Remove(accountHolder);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AccountHolderExists(Guid id)
        {
            return _context.AccountHolders.Any(e => e.Id == id);
        }
    }
}
