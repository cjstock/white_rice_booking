using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using white_rice_booking.Controllers;
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
                return RedirectToPage("./Index");
            }
            else { return Page(); }

        }
    }
}
