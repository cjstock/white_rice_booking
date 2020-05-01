/*
    Name: SignUpModel
    Date Last Updated: 4-30-2020
    Programmer Names: Corey Stock
    Description: Defines the 'code-behind' for the SignUp.cshtml page. It
        handles form submission and keeps track of the page state.
    Important Methods/Structures/Etc: 
            Functions - SignUpModel, OnGet, OnPost
    Major Decisions: Sign up functionality uses the client's 'session' to pass
        the current logged-in user details between pages.
*/
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using white_rice_booking.Models;
using white_rice_booking.Services;

namespace white_rice_booking.Pages
{
    public class SignUpModel : PageModel
    {
        private readonly ILogger<SignUpModel> _logger;

        private AccountService _accountService;

        public SignUpModel(ILogger<SignUpModel> logger, AccountService accountService)
        {
            _accountService = accountService;
            _logger = logger;
        }

        /*
            Name: OnGet
            Date Last Updated: 4-20-2020
            Last Updated Programmer Name: Corey Stock
            Description: Gets called when this page is first reached.
                It handles any OnGet form submissions.
        */
        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty(SupportsGet = true)]
        public UserAccount UserAccount { get; set; }

        [BindProperty]
        public string confirmedPassword { get; set; }

        /*
            Name: OnPost
            Date Last Updated: 4-30-2020
            Last Updated Programmer Name: Corey Stock
            Description: Handles OnPost form requests
        */
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Check that the entered passwords match
            if (UserAccount.Password.Equals(confirmedPassword))
            {
                // Attempt to create and store a new user account
                var success = _accountService.Create(UserAccount.Email, UserAccount.Password);

                if (success)
                {
                    // Account sucessfully created. Log the user in, and redirect to the home page.
                    HttpContext.Session.SetString("loggedInUser", UserAccount.Email);
                    return RedirectToPage("./Index");
                }
                // Account could not be created, have the user try again
                else { return Page(); }
            }

            // Entered passwords do not match, have the user try again.
            return Page();
        }
    }
}
