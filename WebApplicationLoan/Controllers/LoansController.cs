using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WebApplicationLoan.Context;
using WebApplicationLoan.Models;

namespace WebApplicationLoan.Controllers
{
    public class LoansController : Controller
    {
        private readonly InternalExamDbContext _context;
        private string apiUrl = @"http://localhost:5193/api/Loan";
        private HttpClient _httpClient;
        public LoansController(InternalExamDbContext context)
        {
            _context = context;
            _httpClient = new HttpClient();
        }

        // GET: Loans
        public async Task<IActionResult> Index()
        {
              return _context.Loans != null ? 
                          View(await _context.Loans.ToListAsync()) :
                          Problem("Entity set 'InternalExamDbContext.Loans'  is null.");
        }

        // GET: Loans/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Loans == null)
            {
                return NotFound();
            }

            var loan = await _context.Loans
                .FirstOrDefaultAsync(m => m.LoanId == id);
            if (loan == null)
            {
                return NotFound();
            }

            return View(loan);
        }

        // GET: Loans/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Loans/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CustomerName,Gender,PrincipalAmount,NoOfYears")] Loan loan)
        {         
            var response = await _httpClient.GetAsync(apiUrl+$"?gender={loan.Gender}&principalAmount={loan.PrincipalAmount}&noOfYears={loan.NoOfYears}");
            string result = await response.Content.ReadAsStringAsync();
            dynamic d = JsonConvert.DeserializeObject(result);
            loan.InterestRate = d.interestRate;
            loan.InterestAmount = d.interestAmount;
            if (ModelState.IsValid)
            {
                _context.Add(loan);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(loan);
        }

        // GET: Loans/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Loans == null)
            {
                return NotFound();
            }

            var loan = await _context.Loans.FindAsync(id);
            if (loan == null)
            {
                return NotFound();
            }
            return View(loan);
        }

        // POST: Loans/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LoanId,CustomerName,Gender,PrincipalAmount,NoOfYears,InterestRate,InterestAmount")] Loan loan)
        {
            if (id != loan.LoanId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(loan);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LoanExists(loan.LoanId))
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
            return View(loan);
        }

        // GET: Loans/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Loans == null)
            {
                return NotFound();
            }

            var loan = await _context.Loans
                .FirstOrDefaultAsync(m => m.LoanId == id);
            if (loan == null)
            {
                return NotFound();
            }

            return View(loan);
        }

        // POST: Loans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Loans == null)
            {
                return Problem("Entity set 'InternalExamDbContext.Loans'  is null.");
            }
            var loan = await _context.Loans.FindAsync(id);
            if (loan != null)
            {
                _context.Loans.Remove(loan);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LoanExists(int id)
        {
          return (_context.Loans?.Any(e => e.LoanId == id)).GetValueOrDefault();
        }
    }
}
