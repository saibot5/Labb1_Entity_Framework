using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Labb1_Entity_Framework.Data;
using Labb1_Entity_Framework.Models;
using Microsoft.AspNetCore.Identity;
using Labb1_Entity_Framework.Areas.Identity.Data;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Labb1_Entity_Framework.Utility;

namespace Labb1_Entity_Framework.Controllers
{
    [Authorize(Roles = SD.Role_Employee + "," + SD.Role_Admin)]
    public class VacationRequestsController : Controller
    {
        private readonly Labb1DbContext _context;
        private readonly UserManager<Labb1User> _userManager;


        public VacationRequestsController(Labb1DbContext context, UserManager<Labb1User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }


        // GET: VacationRequests
        public async Task<IActionResult> Index()
        {
            _context.Database.EnsureCreated();
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var vacationList = _context.VacationRequest.Where(v => v.Employee.Id == userId);
            return View(await vacationList.ToListAsync());
        }

        [Authorize(Roles = SD.Role_Admin)]
        public async Task<IActionResult> ActiveIndex(string id)
        {
            string searchString = id;

            var requests = from v in _context.VacationRequest
                           select v;

            requests = requests.Where(a => a.accepted == false);
            if (!String.IsNullOrEmpty(searchString))
            {
                requests = requests.Where(s => s.Employee.Firstname.Contains(searchString) || s.Employee.Lastname.Contains(searchString));
            }


            return View(await requests.Include(e => e.Employee).ToListAsync());
        }
        [Authorize(Roles = SD.Role_Admin)]
        public async Task<IActionResult> AdminIndex(DateTime? id)
        {

            var requests = from v in _context.VacationRequest
                           select v;

            if (id.HasValue)
            {
                int month = id.Value.Month;
                requests = requests.Where(m => m.CreatedDate.Month == month);
            }


            return View(await requests.Include(e => e.Employee).ToListAsync());
        }

        // GET: VacationRequests/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vacationRequest = await _context.VacationRequest
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vacationRequest == null)
            {
                return NotFound();
            }

            return View(vacationRequest);
        }

        // GET: VacationRequests/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: VacationRequests/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,StartDate,EndDate,VacationType")] VacationRequest vacationRequest)
        {
            if (ModelState.IsValid)
            {
                string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var span = vacationRequest.EndDate.Subtract(vacationRequest.StartDate);
                vacationRequest.Days = span.Days;
                vacationRequest.Employee = await _userManager.FindByIdAsync(userId);
                vacationRequest.CreatedDate = DateTime.Now;
                _context.Add(vacationRequest);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vacationRequest);
        }

        // GET: VacationRequests/Edit/5
        [Authorize(Roles = SD.Role_Admin)]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vacationRequest = await _context.VacationRequest.FindAsync(id);
            if (vacationRequest == null)
            {
                return NotFound();
            }
            return View(vacationRequest);
        }

        // POST: VacationRequests/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,StartDate,EndDate,VacationType,accepted")] VacationRequest vacationRequest)
        {
            if (id != vacationRequest.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vacationRequest);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VacationRequestExists(vacationRequest.Id))
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
            return View(vacationRequest);
        }

        // GET: VacationRequests/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vacationRequest = await _context.VacationRequest
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vacationRequest == null)
            {
                return NotFound();
            }

            return View(vacationRequest);
        }

        // POST: VacationRequests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vacationRequest = await _context.VacationRequest.FindAsync(id);
            if (vacationRequest != null)
            {
                _context.VacationRequest.Remove(vacationRequest);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VacationRequestExists(int id)
        {
            return _context.VacationRequest.Any(e => e.Id == id);
        }
    }
}
