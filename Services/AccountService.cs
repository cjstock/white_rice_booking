/*
    Name: Account Service
    Date Last Updated: 4-30-2020
    Programmer Names: Corey Stock
    Description: This class implements the functions for managing user accounts
    Important Methods/Structures/Etc: 
            Functions - Account Service, Create, WrtieToFile, Login,
                GetUserAccounts
    Major Decisions: 
            - Used built in search function to look for data in the database because it keeps the 
            code cleaner and easier to use
            - Use a local json database for ease-of-use
*/
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Text.Json;
using white_rice_booking.Models;
using Microsoft.AspNetCore.Hosting;

namespace white_rice_booking.Services
{
    public class AccountService
    {
        public IWebHostEnvironment WebHostEnvironment { get; }
        
        /*
            Name: AccountService
            Date Last Updated: 4-29-2020
            Last Updated Programmer Name: Corey Stock
            Description: Acts as the constructor for the AccountService class.
                Based on .Net Core's pattern of Dependency Injection, takes the
                IWebHostEnvironment as a parameter.
        */
        public AccountService(IWebHostEnvironment webHostEnvironment)
        {
            WebHostEnvironment = webHostEnvironment;
        }

        /*
            Name: Create
            Date Last Updated: 4-29-2020
            Last Updated Programmer Name: Corey Stock
            Description: Creates a new user account and stores it in the
                database. Returns true if an account was sucessfully created,
                and false if not.
        */
        public bool Create(string emailAddress, string password)
        {
            var users = GetUserAccounts();

            var query = users.FirstOrDefault(x => x.Email == emailAddress);

            if (query == null)
            {
                // Create a new user
                UserAccount newUser = new UserAccount();
                newUser.ID = users.Count(); // Ensures user IDs are unique
                newUser.Email = emailAddress;
                newUser.Password = password;

                // Add the new user to the existing users
                var userList = users.ToList();
                userList.Add(newUser);
                users = userList.AsEnumerable();

                // Write to the user_accounts.json file
                WriteToFile(JsonFileName, users);

                // Sucessfully created and stored a new user account
                return true;
            }

            // The a user with the entered 'emailAddress' already exists
            return false;
        }

        /*
            Name: WriteToFile
            Date Last Updated: 4-29-2020
            Last Updated Programmer Name: Corey Stock
            Description: Writes a set of enumerable objects to 
                a json file.
        */
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

        /*
            Name: Login
            Date Last Updated: 4-30-2020
            Last Updated Programmer Name: Corey Stock
            Description: Checks if the 'emailAddress' and 'password' are valid
                login details for a user, and true or false accordingly.
        */
        public bool Login(string emailAddress, string password)
        {
            //Get the enumerable object of users
            var users = GetUserAccounts();

            // Find the first account with the correct email address. Returns
            // null if the account could not be found.
            var account = users.FirstOrDefault(x => x.Email == emailAddress);

            if (account != null)
            {
                // Check if the user entered the correct password
                if (account.Password == password)
                {
                    // The user successfully logged in
                    return true;
                }

                // The passwords did not match
                return false;
            }

            // The entered email address is not associated with an existing
            // account.
            return false;
        }

        /*
            Name: ModifyInfo
            Date Last Updated: 4-30-2020
            Last Updated Programmer Name: Corey Stock
            Description: Allows a user to change their account information
        */
        public ActionResult<UserAccount> ModifyInfo(string email)
        {
            
            return null;
        }

        /*
            Name: GetUserAccounts
            Date Last Updated: 4-20-2020
            Last Updated Programmer Name: Corey Stock
            Description: Reads the database and returns a set of enumerable
                UserAccount objects.
        */
        public IEnumerable<UserAccount> GetUserAccounts()
        {
            using (var jsonFileReader = File.OpenText(JsonFileName))
            {
                return JsonSerializer.Deserialize<UserAccount[]>(jsonFileReader.ReadToEnd());
            }
        }

        // Returns the path to the user_accounts.json database file
        private string JsonFileName
        {
            get { return Path.Combine(WebHostEnvironment.WebRootPath, "data", "user_accounts.json"); }
        }

    
    }
}
