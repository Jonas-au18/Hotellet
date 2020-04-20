using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hotel_california.Data;
using Hotel_california.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hotel_california.Controllers
{
    public class KitchenController : Controller
    {
        private readonly ApplicationDbContext _context;

        public KitchenController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult  Test()
        {
        
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Test([Bind("Day,Month")]DinnerPlan request)
        {
            var Plan = await _context.DinnerPlans.FirstOrDefaultAsync(m => m.Day == request.Day && m.Month == request.Month);
            return View(Plan);
        }
    }
}