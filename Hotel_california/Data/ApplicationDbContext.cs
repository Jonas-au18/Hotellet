using System;
using System.Collections.Generic;
using System.Text;
using Hotel_california.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Hotel_california.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<ApplicationUser> MApplicationUsers { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<DinnerPlan> DinnerPlans { get; set; }
        public DbSet<Guest> Guests { get; set; }
    }
}
