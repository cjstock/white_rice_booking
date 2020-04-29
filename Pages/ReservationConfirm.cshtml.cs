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
        
        private ReservationService _reservationService;
        
        public ReservationConfirmModel(ILogger<ReservationConfirmModel> logger, ReservationService reservationService)
        {
            _logger = logger;
            _reservationService = reservationService;
        }

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
        
        [BindProperty(SupportsGet = true)]
        public string City{ get; set; }
        
        [BindProperty(SupportsGet = true)]
        public string State{ get; set; }
        
        [BindProperty(SupportsGet = true)]
        public int ZipCode{ get; set; }

        public void OnGet()
        {
            //display everything
            int flightID = 0;
            
            //seats, date, time, depart airport, depart city, arrival airport, arrival city, billing


            string temp = _reservationService.FindFlightInfo(flightID);
            string[] words = temp.Split(','); //string array output 
        }

        public IActionResult OnPost()
        {
            int userID = 159753;
            _reservationService.Create(FirstName, LastName, depart_ID, return_ID, userID);
            return Page();
        }
    }
}