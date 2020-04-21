using System;
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
                    Day = 25,
                    Month = 4,
                    ExpectedAdults = 120,
                    ExpectedKids = 40,
                    ExpectedTotal = 160,
                    CheckedInAdults = 0,
                    CheckedInKids = 0,
                    CheckedInTotal = 0
                };
                Plans.Add(m);
                m = new DinnerPlan()
                {
                    Day = 26,
                    Month = 4,
                    ExpectedAdults = 200,
                    ExpectedKids = 150,
                    ExpectedTotal = 350,
                    CheckedInAdults = 0,
                    CheckedInKids = 0,
                    CheckedInTotal = 0
                };
                Plans.Add(m);
                m = new DinnerPlan()
                {
                    Day = 28,
                    Month = 4,
                    ExpectedAdults = 180,
                    ExpectedKids = 120,
                    ExpectedTotal = 300,
                    CheckedInAdults = 0,
                    CheckedInKids = 0,
                    CheckedInTotal = 0
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
                b = new Booking()
                {
                    Day = 21,
                    Month = 4,
                    Adults = 2,
                    Kids = 4,
                    RoomNr = 220
                };
                Bookings.Add(b);
                b = new Booking()
                {
                    Day = 31,
                    Month = 4,
                    Adults = 1,
                    Kids = 2,
                    RoomNr = 100
                };
                Bookings.Add(b);
                b = new Booking()
                {
                    Day = 5,
                    Month = 5,
                    Adults = 2,
                    Kids = 1,
                    RoomNr = 320
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
                    LName = "Timsen"
                };
                Guestlist.Add(a);
                a = new Guest()
                {
                    RoomNum = 200,
                    Age = 40,
                    FName = "Hanne",
                    LName = "Timsen",
                };
                Guestlist.Add(a);
                a = new Guest()
                {
                    RoomNum = 200,
                    Age = 42,
                    FName = "Per",
                    LName = "Timsen"
                };
                Guestlist.Add(a);
                a = new Guest()
                {
                    RoomNum = 220,
                    Age = 50,
                    FName = "Jonathan",
                    LName = "Eriksen"
                };
                Guestlist.Add(a);
                a = new Guest()
                {
                    RoomNum = 100,
                    Age = 31,
                    FName = "Jonas",
                    LName = "Nielsen"
                };
                Guestlist.Add(a);
                context.Guests.AddRange(Guestlist);
                context.SaveChangesAsync().Wait();
            }
        }

        public static void SeedUsers(UserManager<ApplicationUser> userManager, ILogger log)
        {
            List<Claim> claims = new List<Claim>();
            var adminclaim = new Claim("Admin", "Yes");
            var kitchenClaim = new Claim("Kitchen", "Yes");
            var WaiterClaim = new Claim("Waiter", "Yes");
            var ReceptionClaim = new Claim("Reception", "Yes");
            claims.Add(adminclaim);
            claims.Add(kitchenClaim);
            claims.Add(WaiterClaim);
            claims.Add(ReceptionClaim);

            const string KitchenUsername = "Kitchen";
            const string KitchenPassword = "Password1";
            if (userManager.FindByNameAsync(KitchenUsername).Result == null)
            {
                ApplicationUser user = new ApplicationUser
                {
                    UserName = KitchenUsername,
                    Email = "",
                    Name = "KitchenStaff"
                };
                IdentityResult result = userManager.CreateAsync(
                    user, KitchenPassword).Result;
                if (result.Succeeded)
                {
                    userManager.AddClaimAsync(user, kitchenClaim).Wait();
                }

            }

            const string WaiterUsername = "Waiter";
            const string WaiterPassword = "Password1";
            if (userManager.FindByNameAsync(WaiterUsername).Result == null)
            {
                ApplicationUser user = new ApplicationUser
                {
                    UserName = WaiterUsername,
                    Name = "Waiterstaff"
                };
                IdentityResult result = userManager.CreateAsync(
                    user, WaiterPassword).Result;
                if (result.Succeeded)
                {
                    userManager.AddClaimAsync(user, WaiterClaim).Wait();
                }
            }

            const string ReceptionUsername = "Reception";
            const string ReceptionPassword = "Password1";
            if (userManager.FindByNameAsync(ReceptionUsername).Result == null)
            {
                ApplicationUser user = new ApplicationUser
                {
                    UserName = ReceptionUsername,
                    Name = "Receptionist"
                };
                IdentityResult result = userManager.CreateAsync(
                    user, ReceptionPassword).Result;
                if (result.Succeeded)
                {
                    userManager.AddClaimAsync(user, ReceptionClaim).Wait();
                }
            }

            const string adminUsername = "Admin";
            const string adminPassword = "Password1";

            if (userManager.FindByNameAsync(adminUsername).Result == null)
            {
                log.LogWarning("Seeding the admin user");
                ApplicationUser user = new ApplicationUser
                {
                    UserName = adminUsername,
                    Email = "",
                    Name = "SystemAdmin",
                };
                IdentityResult result = userManager.CreateAsync(
                    user, adminPassword).Result;
                if (result.Succeeded)
                {
                    userManager.AddClaimsAsync(user, claims).Wait();
                }
            }
        }

    }
}