using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hotel_california.Data;
using Hotel_california.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;

namespace Hotel_california.Controllers
{[Authorize("isReception")]
    public class ReceptionController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReceptionController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("RoomNr,Day,Month,Adults,Kids")] Booking booking)
        {
            if (ModelState.IsValid)
            {
                var Dinner =
                    await _context.DinnerPlans.FirstOrDefaultAsync(
                        m => m.Day == booking.Day && m.Month == booking.Month);
                if (Dinner == null)
                {
                    Dinner = new DinnerPlan();
                }

                Dinner.Day = booking.Day;
                Dinner.Month = booking.Month;
                Dinner.ExpectedTotal += (booking.Adults + booking.Kids);
                Dinner.ExpectedAdults += booking.Adults;
                Dinner.ExpectedKids += booking.Kids;
                _context.Update(Dinner);
                _context.Add(booking);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return Index();
        }

        public IActionResult addGuest()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> addGuest([Bind("FName,LName,RoomNum,Age")]Guest guest)
        {
            if (ModelState.IsValid)
            {
                _context.Add(guest);
                await _context.SaveChangesAsync();
                return View();
            }

            return BadRequest();
        }

        public IActionResult Checkout()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Checkout([Bind("room")] int room)
        {
            var Guests = await _context.Guests.Where(m => m.RoomNum == room).ToListAsync();
            if (Guests == null)
            {
                return NotFound();
            }
            foreach (var i in Guests)
            {
                _context.Guests.Remove(i);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Display()
        {
            return View(await _context.Bookings.ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Display([Bind("day,month")]int day, int month)
        {
            return View(await _context.Bookings.Where(m => m.Day == day && m.Month == month).ToListAsync());
        }

        public async Task<IActionResult> DisplayGuest()
        {
            return View(await _context.Guests.ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> DisplayGuest([Bind("room")]int room)
        {
            return View(await _context.Guests.Where(m=>m.RoomNum == room).ToListAsync());
        }
    }
}