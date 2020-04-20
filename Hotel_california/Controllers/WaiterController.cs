using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hotel_california.Data;
using Hotel_california.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hotel_california.Controllers
{   [Authorize("isWaiter")]
    public class WaiterController : Controller
    {
        private readonly ApplicationDbContext _context;

        public WaiterController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Waiter()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Adults,Kids,RoomNr")] CheckIn checkin)
        {
            if (ModelState.IsValid)
            {
                int[] today = {DateTime.Today.Day, DateTime.Today.Month};

                var Plan = await _context.DinnerPlans.FirstOrDefaultAsync(m => m.Day == today[0] && m.Month == today[1]);
                if (Plan == null)
                {
                    return NotFound();
                }

                Plan.CheckedInAdults += checkin.Adults;
                Plan.CheckedInKids += checkin.Kids;
                Plan.CheckedInTotal += (checkin.Adults + checkin.Kids);
                _context.Update(Plan);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Waiter));
            }

            return Waiter();
        }
    }
}