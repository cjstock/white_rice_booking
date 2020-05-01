/*
    Name: Account Service
    Date Last Updated: 4-30-2020
    Programmer Names: Corey Stock
    Description: This class contains the Main function, which is the entry
                    point for the code
    Important Methods/Structures/Etc: 
            Functions - Main, CreateHostBuilder
    Major Decisions: None. This file was generate by .Net Core
*/
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace white_rice_booking
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
