/*
    Name: Index
    Date Last Updated: 4-23-2020
    Programmer Names: Justin Tran, Corey Stock, William Yung
    Description: This class connects the home page front-end, namely Index.cshtml, with the 
                 initial page loading and page navigation controls in the back-end
    Important Methods/Structures/Etc: OnPost
    Major Decisions: N/A
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using white_rice_booking.Controllers;
using white_rice_booking.Models;
using white_rice_booking.Services;
using Microsoft.AspNetCore.Http;

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

        
        public IActionResult OnPost() 
        {
            return RedirectToPage("/FilterFlights");
            
        }

    }
}
