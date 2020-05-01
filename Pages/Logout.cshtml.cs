/*
    Name: Logout
    Date Last Updated: 4-30-2020
    Programmer Names: Corey Stock
    Description: Defines the 'code-behind' for the Logout page.
        Keeps track of the page state and handles form submission.
    Important Methods/Structures/Etc:
        Methods - OnGet
    Major Decisions: N/A
*/
using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using white_rice_booking.Models;
using white_rice_booking.Services;

namespace white_rice_booking.Pages
{
    public class LogoutModel : PageModel
    {
        private readonly ILogger<LogoutModel> _logger;

        public LogoutModel(ILogger<LogoutModel> logger, AccountService accountService)
        {
            _logger = logger;
        }

        /*
            Name: OnGet
            Date Last Updated: 4-29-2020
            Last Updated Programmer Name: Corey Stock
            Description: Logs the user out by removing their info
                from the session.
        */
        public IActionResult OnGet()
        {
            HttpContext.Session.Remove("loggedInUser");
            return RedirectToPage("Index");
        }
    }
}
