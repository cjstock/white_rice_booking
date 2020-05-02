/*
    Name: Startup
    Date Last Updated: 4-30-2020
    Programmer Names: Corey Stock, William Yung, Justin Tran
    Description: Defines the configuration of the .Net Core project
    Important Methods/Structures/Etc:
        Functions - Startup, ConfigureServices, Configure
    Major Decisions: Use Razor Pages for the front end because they
        are the default front end for .Net Core websites. Use a collection
        of services for the backend, so we can have a 'separation of concerns'.
*/
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using white_rice_booking.Services;

namespace white_rice_booking
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Environment = env;
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddTransient<AccountService>();
            services.AddSession();
            //services.AddTransient<FilterFlightsService>();
            services.AddTransient<ReservationService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            // Allows HTML, CSS, images, and JavaScriptfiles in wwwroot to be
            // used
            app.UseStaticFiles(); 

            app.UseRouting();

            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}