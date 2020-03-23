using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace white_rice_booking.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {

        private readonly List<UserAccount> _userAccounts = new List<UserAccount>();
        private readonly ILogger<AccountController> _logger;

        public AccountController(ILogger<AccountController> logger)
        {
            _logger = logger;
        }

        [HttpPost("{email}")]
        [Route("CreateAccount")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<UserAccount> Create(string email)
        {
            UserAccount newUser = new UserAccount();
            newUser.Email = email;
            newUser.Password = "password";

            return CreatedAtAction(nameof(newUser), newUser);
        }
    }
}