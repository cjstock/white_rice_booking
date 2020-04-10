using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;

namespace white_rice_booking.Pages
{
    public class IndexModel : PageModel
    {
        public string Message { get; private set; } = "PageModel in C#";

        public void OnGet()
        {
            //Message += $"Hello world!";
            Message += $" Server time is { DateTime.Now }";
        }
    }
}