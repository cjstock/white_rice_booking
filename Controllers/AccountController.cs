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
        // TODO: Creates a login token used to validate the user is logged in
        private readonly int _loginToken = 0;
        private readonly ILogger<AccountController> _logger;


        /* Handles requests for dealing with UserAccounts,
        including logging in, creating an account, and modifying
        account info */
        public AccountController(ILogger<AccountController> logger)
        {
            _logger = logger;
        }

        [HttpPost("{email}")]
        [Route("CreateAccount")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        /* Use the entered email and password to create a new UserAccount,
        then push the account to the database */
        public ActionResult<UserAccount> Create(string email)
        {
            UserAccount newUser = new UserAccount();
            newUser.Email = email;
            newUser.Password = "password";

            return CreatedAtAction(nameof(newUser), newUser);
        }

        /* When "login" button is pressed, log the user in by giving them a
        login token */
        public ActionResult<UserAccount> Login(string email, string password)
        {
            return null;
        }

        /* When the user presses the "modify info" button, take the changed
        fields and push them to the database */
        public ActionResult<UserAccount> ModifyInfo(string email)
        {
            
            return null;
        }
    }
}
