using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

using white_rice_booking.Models;
using white_rice_booking.Services;



/*
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
*/



namespace white_rice_booking.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        public JsonFileProductService ProductService;
        public IEnumerable<Product> Products { get; private set; }
        public IndexModel(ILogger<IndexModel> logger, JsonFileProductService productService)
        {
            _logger = logger;
            ProductService = productService;
        }

        public void OnGet()
        {
            Products = ProductService.GetProducts();
        }
    }
}
