/*
    Name: LoginModel
    Date Last Updated: 4-30-2020
    Programmer Names: Corey Stock
    Description: Defines the 'code-behind' for the Login page. Handles
        form submission.
    Important Methods/Structures/Etc:
        Methods - OnPost
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
    public class LoginModel : PageModel
    {
        private readonly ILogger<LoginModel> _logger;

        private AccountService _accountService;

        public LoginModel(ILogger<LoginModel> logger, AccountService accountService)
        {
            _accountService = accountService;
            _logger = logger;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty(SupportsGet = true)]
        public UserAccount UserAccount { get; set; }

        /*
            Name: OnPost
            Date Last Updated: 4-29-2020
            Last Updated Programmer Name: Corey Stock
            Description: Handles form submission for the Login page.
        */
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var success = _accountService.Login(UserAccount.Email, UserAccount.Password);

            if (success)
            {
                HttpContext.Session.SetString("loggedInUser", UserAccount.Email);
                var returnString = HttpContext.Session.GetString("loggedInUser");
                Console.WriteLine(returnString);
                return RedirectToPage("./Index");
            }
            else { return Page(); }

        }
    }
}
