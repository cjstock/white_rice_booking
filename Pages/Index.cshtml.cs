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


namespace white_rice_booking.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private FilterFlightsService _filterflightsService;
        public List<Flights> availableFlights;

        public IndexModel(ILogger<IndexModel> logger, FilterFlightsService filterflightsService)
        {
            _logger = logger;
            _filterflightsService = filterflightsService;
        }

        [BindProperty(SupportsGet = true)]
        public IActionResult OnGet()
        {
            availableFlights = _filterflightsService.FilterFlights();
            return RedirectToPage("./Index");
        }
    }
}
