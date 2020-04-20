using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hotel_california.Data;
using Hotel_california.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;

namespace Hotel_california.Controllers
{
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

        [HttpPost]
        public async Task<IActionResult> addGuest([Bind("Fname,Lname,RoomNum,Age")]Guest guest)
        {
            if (ModelState.IsValid)
            {
                _context.Add(guest);
                await _context.SaveChangesAsync();
                return View();
            }

            return BadRequest();
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
        public async Task<IActionResult> DisplayGuest([Bind("room")]int roomid)
        {
            return View(await _context.Guests.Where(m=>m.RoomNum == roomid).ToListAsync());
        }
    }
}