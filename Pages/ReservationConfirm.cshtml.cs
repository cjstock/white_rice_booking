using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using white_rice_booking.Models;
using white_rice_booking.Services;


namespace white_rice_booking.Pages
{
    public class ReservationConfirmModel : PageModel
    {
        private readonly ILogger<ReservationConfirmModel> _logger;
        [BindProperty(SupportsGet = true)]
        public int depart_ID{ get; set; }

        [BindProperty(SupportsGet = true)]
        public int return_ID{ get; set; }
        
        [BindProperty(SupportsGet = true)]
        public string FirstName{ get; set; }
        
        [BindProperty(SupportsGet = true)]
        public string LastName{ get; set; }
        
        [BindProperty(SupportsGet = true)]
        public string StreetName{ get; set; }
        
        //[BindProperty(SupportsGet = true)]
        //public int AptNum{ get; set; }
        
        [BindProperty(SupportsGet = true)]
        public string City{ get; set; }
        
        [BindProperty(SupportsGet = true)]
        public string State{ get; set; }
        
        [BindProperty(SupportsGet = true)]
        public int ZipCode{ get; set; }

        public void OnGet()
        {

        }

        public IActionResult OnPost()
        {
            return Page();
        }
    }
}