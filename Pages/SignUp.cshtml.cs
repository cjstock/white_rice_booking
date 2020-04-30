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

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty(SupportsGet = true)]
        public UserAccount UserAccount { get; set; }

        [BindProperty]
        public string confirmedPassword { get; set; }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (UserAccount.Password.Equals(confirmedPassword))
            {
                var success = _accountService.Create(UserAccount.Email, UserAccount.Password);

                if (success)
                {
                    HttpContext.Session.SetString("loggedInUser", UserAccount.Email);
                    return RedirectToPage("./Index");
                }
                else { return Page(); }
            }

            return Page();

        }
    }
}
