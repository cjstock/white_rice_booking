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

        private AccountService _accountService;

        public LogoutModel(ILogger<LogoutModel> logger, AccountService accountService)
        {
            _accountService = accountService;
            _logger = logger;
        }

        public IActionResult OnGet()
        {
            HttpContext.Session.Remove("loggedInUser");
            return RedirectToPage("Index");
        }
    }
}
