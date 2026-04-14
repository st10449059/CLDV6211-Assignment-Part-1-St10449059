/* * CODE ATTRIBUTION
 * ------------------------------------------------------------------------------------------
 * Author/Source: Microsoft Documentation (ASP.NET Core Fundamentals)
 * Date: 15 April 2026
 * Description: Configuration of the web application builder, dependency injection for 
 * SQL LocalDB, and the middleware request pipeline.
 * Link: https://learn.microsoft.com/en-us/aspnet/core/fundamentals/startup
 * ------------------------------------------------------------------------------------------
 */




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

/* * =========================================================================================
 * REFERENCE LIST & ATTRIBUTION
 * =========================================================================================
 * The following resources were utilized for the design, implementation, and documentation 
 * of the EventEase Web Application (Part 1).
 * * · Connolly, T. M. & Begg, C. E. (2015). Database Systems: A Practical Approach to Design, 
 * Implementation, and Management. 6th edition. Pearson Education.
 * * · Coyne, J. (2021). CSS Refactoring: Architecting Systems for Change. 2nd edition. 
 * O'Reilly Media.
 * * · Elmasri, R. & Navathe, S. B. (2017). Fundamentals of Database Systems. 7th edition. 
 * Pearson.
 * * · Freeman, A. (2022). Pro ASP.NET Core 6: Develop Cloud-Ready Web Applications Using MVC, 
 * Blazor, and Razor Pages. 9th edition. Apress.
 * * · Lerman, J. & Miller, R. (2015). Programming Entity Framework: DbContext. 2nd edition. 
 * O'Reilly Media.
 * * · Lucid Software Inc. (2026). Lucidchart Cloud-based Visual Workspace. [Online]. 
 * Available at: https://www.lucidchart.com [Accessed 14 April 2026].
 * * · Microsoft. (2023). ASP.NET Core Middleware. [Online]. 
 * Available at: https://learn.microsoft.com/en-us/aspnet/core/fundamentals/middleware/ 
 * [Accessed 14 April 2026].
 * * · Microsoft. (2023). Configuration in ASP.NET Core. [Online]. 
 * Available at: https://learn.microsoft.com/en-us/aspnet/core/fundamentals/configuration/ 
 * [Accessed 14 April 2026].
 * * · Microsoft. (2023). Model-View-Controller (MVC) Pattern. [Online]. 
 * Available at: https://learn.microsoft.com/en-us/aspnet/core/mvc/overview 
 * [Accessed 14 April 2026].
 * * · Microsoft. (2023). Primary and Foreign Key Constraints. [Online]. 
 * Available at: https://learn.microsoft.com/en-us/sql/relational-databases/tables/primary-and-foreign-key-constraints 
 * [Accessed 14 April 2026].
 * =========================================================================================
 */