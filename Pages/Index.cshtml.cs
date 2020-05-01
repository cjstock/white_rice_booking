using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;

/*
    Name: Index
    Date Last Updated: 4-30-2020
    Programmer Names: Justin Tran, Corey Stock, William Yung
    Description: This class connects the home page front-end, namely
        Index.cshtml, with the initial page loading and page navigation controls in the back-end.
    Important Methods/Structures/Etc:
        Methods - OnPost, Logout
    Major Decisions: N/A
*/

namespace white_rice_booking.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }
        public void OnGet() 
        {

        }

        /*
            Name: Logout
            Date Last Updated: 4-29-2020
            Last Updated Programmer Name: Corey Stock
            Description: Logs the user out
        */
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("loggedInUser");
            return RedirectToPage("Index");
        }
        
        /*
            Name: OnPost
            Date Last Updated: 4-29-2020
            Last Updated Programmer Name: Justin Tran
            Description: Navigates to the 'FilterFlights' page when the
                form is submitted.
        */
        public IActionResult OnPost() 
        {
            return RedirectToPage("/FilterFlights");
        }

    }
}
