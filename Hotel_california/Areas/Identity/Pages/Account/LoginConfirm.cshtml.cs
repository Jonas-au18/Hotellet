using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Hotel_california.Areas.Identity.Pages.Account.Manage
{
    [AllowAnonymous]
    public class LoginConfirmModel : PageModel
    {
        public string returnUrl { get; set; }
        public IActionResult OnGet()
        {

            if (User.HasClaim("Admin", "Yes"))
            {
                returnUrl = Url.Action("AdminPage", "Home");
                return LocalRedirect(returnUrl);
            }
            if (User.HasClaim("Waiter", "Yes"))
            {
                returnUrl = Url.Action("Waiter", "Waiter");
                return LocalRedirect(returnUrl);
            }

            if (User.HasClaim("Reception", "Yes"))
            {
                returnUrl = Url.Action("Index", "Reception");
                return LocalRedirect(returnUrl);
            }

            if (User.HasClaim("Kitchen", "Yes"))
            {
                returnUrl = Url.Action("Kitchen", "Kitchen");
                return LocalRedirect(returnUrl);
            }

            return LocalRedirect("Login");
        }

    }
}
