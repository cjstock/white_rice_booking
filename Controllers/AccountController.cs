using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Globalization;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Web;
using System.Net;
using System.Net.Http;
using white_rice_booking.Models;

namespace white_rice_booking.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        // TODO: Creates a login token used to validate the user is logged in
        private readonly int _loginToken;

        private static List<UserAccount> _users;
        private string _db = @"data/user_accounts.json";
        
        private int _userID;
        private readonly ILogger<AccountController> _logger;


        /* Handles requests for dealing with UserAccounts,
        including logging in, creating an account, and modifying
        account info */
        public AccountController(ILogger<AccountController> logger)
        {
            _logger = logger;
            _users = GetUserAccounts(_db);
            _userID = InitUserID();
        }

        [ActionName("Create")]
        [HttpPost("{emailAddress}")]
        [Route("Create/{emailAddress}/{password}")]

        /* Use the entered email and password to create a new UserAccount,
        then push the account to the database */
        public bool Create(string emailAddress, string password)
        {
            if (CheckEmailExists(emailAddress))
            {
                return false;
            }
            UserAccount newUser = new UserAccount();
            newUser.ID = _userID++;
            newUser.Email = emailAddress;
            newUser.Password = password;
            _users.Add(newUser);
            
            //Write modified _users to the database
            string json = GetDBasString(_db);
            System.IO.File.WriteAllText(_db, json);

            return true;
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

        //Uses the path to the json file to get the list of user accounts
        private static List<UserAccount> GetUserAccounts(string path)
        {
            string users = System.IO.File.ReadAllText(path);
            List<UserAccount> user_list = JsonConvert.DeserializeObject<List<UserAccount>>(users);

            if (user_list == null)
            {
                user_list = new List<UserAccount>();
            }

            return user_list;
        }

        //Ensures users do not have duplicate IDs
        private static int InitUserID()
        {
            if (_users.Any()) { return _users.Count(); }
            else { return 0; }
        }

        private bool CheckEmailExists(string emailAddress)
        {
            JArray accounts = JArray.Parse(GetDBasString(_db));
            var emailAddresses = 
                from ea in accounts.Children()
                select (string)ea["Email"];

            if (emailAddresses.Contains(emailAddress)) return true;
            else return false;
        }

        private static string GetDBasString(string path)
        {
            return @JsonConvert.SerializeObject(_users);
        }
    }
}
