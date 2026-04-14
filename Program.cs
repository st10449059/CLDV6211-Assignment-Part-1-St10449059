using Microsoft.EntityFrameworkCore;
using CLDV6211_Assignment_Part_1_St10449059.Data;

namespace CLDV6211_Assignment_Part_1_St10449059
{
 
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            /* * 1. Service Registration: 
             * Registering the ApplicationDbContext into the Dependency Injection (DI) container. 
             * This allows controllers to request a database instance via their constructors (Freeman, 2022).
             */
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Registers the MVC services required to handle Controllers and Views.
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            /* * 2. Middleware Pipeline Configuration:
             * Defines how HTTP requests are handled as they travel through the application 
             * (Lerman & Miller, 2015).
             */
            if (!app.Environment.IsDevelopment())
            {
                // Exception handling and HSTS for production-level security.
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            // Enables the serving of static assets like CSS, JavaScript, and Images (Microsoft, 2023).
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            /* * 3. Routing:
             * Defines the Conventional Routing pattern used to map URLs to specific 
             * Controller actions (Freeman, 2022).
             */
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}