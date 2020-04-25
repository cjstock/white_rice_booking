
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Globalization;
using System.IO;
using System.Text.Json;
using System.Web;
using System.Net;
using System.Net.Http;
using white_rice_booking.Models;
using Microsoft.AspNetCore.Hosting;

namespace white_rice_booking.Services
{
    public class AccountService
    {
        // TODO: Creates a login token used to validate the user is logged in
        private readonly int _loginToken;

        private IEnumerable<UserAccount> _users;

        public IWebHostEnvironment WebHostEnvironment { get; }
        
        /* Handles requests for dealing with UserAccounts,
        including logging in, creating an account, and modifying
        account info */
        public AccountService(IWebHostEnvironment webHostEnvironment)
        {
            WebHostEnvironment = webHostEnvironment;
            _users = GetUserAccounts();
            // _userID = InitUserID();
        }

        /* Use the entered email and password to create a new UserAccount,
        then push the account to the database */
        public bool Create(string emailAddress, string password)
        {
            var users = GetUserAccounts();

            var query = users.FirstOrDefault(x => x.Email == emailAddress);

            if (query == null)
            {
                //Create a new user
                UserAccount newUser = new UserAccount();
                newUser.ID = users.Count();
                newUser.Email = emailAddress;
                newUser.Password = password;

                //Add the new user to the existing users
                var userList = _users.ToList();
                userList.Add(newUser);
                _users = userList.AsEnumerable();


                //Write to data
                WriteToFile(JsonFileName, _users);

                return true;
            }

            return false;
        }

        public void WriteToFile(string fileName, IEnumerable<object> objects)
        {
            using(var outputStream = File.OpenWrite(fileName))
            {
                JsonSerializer.Serialize<IEnumerable<object>>(
                    new Utf8JsonWriter(outputStream, new JsonWriterOptions
                    {
                        SkipValidation = false,
                        Indented = true
                    }),
                    objects
                );
            }
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
        public IEnumerable<UserAccount> GetUserAccounts()
        {
            using (var jsonFileReader = File.OpenText(JsonFileName))
            {
                return JsonSerializer.Deserialize<UserAccount[]>(jsonFileReader.ReadToEnd());
            }
        }

        //Ensures users do not have duplicate IDs
        private int InitUserID()
        {
            List<UserAccount> usersList = _users.ToList();
            if (usersList.Any()) { return usersList.Count(); }
            else { return 0; }
        }

        private string JsonFileName
        {
            get { return Path.Combine(WebHostEnvironment.WebRootPath, "data", "user_accounts.json"); }
        }

    
    }
}
