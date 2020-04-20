using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Hotel_california.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Hotel_california.Data
{
    public class DbHelper
    {
        public static void SeedData(ApplicationDbContext context, UserManager<ApplicationUser> userManager, ILogger log)
        {
            Seed(context, log);
            SeedUsers(userManager, log);
        }

        private static void Seed(ApplicationDbContext context, ILogger log)
        {
            var m = context.DinnerPlans.FirstOrDefault();
            if (m == null)
            {
                log.LogInformation("Seeding Plan");
                var Plans = new List<DinnerPlan>();
                m = new DinnerPlan()
                {
                    Day = 20,
                    Month = 4,
                    ExpectedAdults = 20,
                    ExpectedKids = 20,
                    ExpectedTotal = 40,
                    CheckedInAdults = 2,
                    CheckedInKids = 2,
                    CheckedInTotal = 4
                };
                Plans.Add(m);
                m = new DinnerPlan()
                {
                    Day = 21,
                    Month = 4,
                    ExpectedAdults = 20,
                    ExpectedKids = 20,
                    ExpectedTotal = 40,
                    CheckedInAdults = 5,
                    CheckedInKids = 10,
                    CheckedInTotal = 14
                };
                Plans.Add(m);
                context.DinnerPlans.AddRange(Plans);
            }

            var b = context.Bookings.FirstOrDefault();
            if (b == null)
            {
                log.LogInformation("Seeding bookings");
                var Bookings = new List<Booking>();
                b = new Booking()
                {
                    Day = 21,
                    Month = 4,
                    Adults = 2,
                    Kids = 3,
                    RoomNr = 200

                };
                Bookings.Add(b);
                context.Bookings.AddRange(Bookings);
            }


            var a = context.Guests.FirstOrDefault();
            if (a == null)
            {
                log.LogInformation("Seeding Guests");
                var Guestlist = new List<Guest>();
                a = new Guest()
                {
                    RoomNum = 200,
                    Age = 12,
                    FName = "Tim",
                    LName = "Timsen",
                    IsAdult = false
                };
                Guestlist.Add(a);
                context.Guests.AddRange(Guestlist);
                context.SaveChangesAsync();
            }
        }

        public static async Task SeedUsers(UserManager<ApplicationUser> userManager, ILogger log)
        { 
            const string adminUsername = "Admin";
            const string adminPassword = "Password1";

            if (userManager.FindByNameAsync(adminUsername) == null)
            {
                log.LogWarning("Seeding the admin user");
                ApplicationUser user = new ApplicationUser
                {
                    UserName = adminUsername,
                    Name = "SystemAdmin",
                };
                IdentityResult result = userManager.CreateAsync(
                    user,adminPassword).Result;
                if (result.Succeeded)
                {
                    var adminclaim = new Claim("Admin", "Yes");
                    userManager.AddClaimAsync(user, adminclaim).Wait();
                }
            }
        }
    }
}