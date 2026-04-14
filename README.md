Theory Questions 
1. Cloud vs. On Premises Deployment
Deploying "EventEase" in the cloud (e.g., Azure or AWS) differs significantly from traditional on-premises hosting (using your own local server) in three main areas:
•	Security:
o	On-Premises: You are responsible for everything, from the physical locks on the server room door to the network firewalls.
o	Cloud: Security follows a Shared Responsibility Model. The cloud provider secures the physical hardware and data centers, while you focus on securing your application code and user data.
o	Example: In the cloud, you can enable Multi-Factor Authentication (MFA) with a single click, whereas on premises might require complex server installations.
•	Deployment Speed:
o	On-Premises: Deploying can take weeks because you might need to purchase, rack, and configure physical hardware before installing the OS and SQL Server.
o	Cloud: Deployment happens in minutes using provisioning. You can spin up a web server and a database for EventEase almost instantly via a management portal.
•	Resource Management:
o	On-Premises: You must buy enough hardware to handle "peak load" (e.g., a massive event booking day), which remains idle and wasted during quiet periods.
o	Cloud: Uses Elasticity and Scalability. If your EventEase site gets a surge of users, the cloud can automatically add more power (Scale-out) and then shrink back down when the surge is over to save costs.
2. IaaS, PaaS, and SaaS: The "Pizza as a Service" Comparison
The primary difference between these models is control vs. convenience.
Service Model	Definition	Responsibility
IaaS (Infrastructure)	Provides raw virtual machines, storage, and networks.	You manage the OS, runtime, and updates.
PaaS (Platform)	Provides a framework for developers to build and deploy apps.	You only manage the code and data.
SaaS (Software)	Provides a finished product via a web browser (e.g., Gmail).	The provider manages everything.
Why EventEase benefits from PaaS (e.g., Azure App Service):
As a university student/developer, PaaS is the best fit for your project for these reasons:
1.	Focus on Coding: You don't have to worry about updating Windows Server or patching the SQL Server engine. You just push your code, and the platform handles the rest.
2.	Built-in Tools: PaaS provides easy "Push-to-Deploy" from GitHub or Visual Studio, which is much faster than manually setting up an IaaS virtual machine.
3.	Cost-Efficiency: You only pay for the platform resources you use, rather than paying for a whole virtual machine that might be overpowered for a second-year project.



# ERD Diagram: 
 <img width="940" height="491" alt="image" src="https://github.com/user-attachments/assets/842d15cc-e58f-424b-a761-a7e4c141553f" />


# GitHub Link: 

# YouTube link: 
 
# App Running
# Home Page:  
<img width="939" height="498" alt="image" src="https://github.com/user-attachments/assets/25b48cd1-46d6-40de-8b38-f8bd09fe4bad" />

# Event Page:
<img width="939" height="499" alt="image" src="https://github.com/user-attachments/assets/70a505a7-0c5c-46e7-9343-faf9db4d7aa6" />

# Venues Page:
 <img width="939" height="500" alt="image" src="https://github.com/user-attachments/assets/cee73a03-5dd6-4149-aabe-e0055b72d8a2" />

# Bookings Page:
 <img width="939" height="501" alt="image" src="https://github.com/user-attachments/assets/ff7adeaf-7fde-47cc-a629-80b93afca1d8" />


 
# SQL Script: 
CREATE TABLE Venues (
    VenueId INT PRIMARY KEY IDENTITY(1,1),
    VenueName NVARCHAR(100) NOT NULL,
    Location NVARCHAR(200) NOT NULL,
    ImageUrl NVARCHAR(MAX) NULL
);

CREATE TABLE Events (
    EventId INT PRIMARY KEY IDENTITY(1,1),
    EventName NVARCHAR(100) NOT NULL,
    EventDate DATETIME NOT NULL,
    ImageUrl NVARCHAR(MAX) NULL,
    VenueId INT NOT NULL,
    FOREIGN KEY (VenueId) REFERENCES Venues(VenueId)
);

CREATE TABLE Bookings (
    BookingId INT PRIMARY KEY IDENTITY(1,1),
    AttendeeName NVARCHAR(100) NOT NULL,
    EventId INT NOT NULL,
    FOREIGN KEY (EventId) REFERENCES Events(EventId)
);

 
# MVC Code:
# Venue.cs: 
using System.ComponentModel.DataAnnotations;

namespace CLDV6211_Assignment_Part_1_St10449059.Models
{
    public class Venue
    {
        [Key]
        public int VenueId { get; set; }
        [Required]
        public string VenueName { get; set; }
        [Required]
        public string Location { get; set; }
        public string? ImageUrl { get; set; }
        public virtual ICollection<Event>? Events { get; set; }
    }
}
# Event.cs
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CLDV6211_Assignment_Part_1_St10449059.Models
{
    public class Event
    {
        [Key]
        public int EventId { get; set; }
        [Required]
        public string EventName { get; set; }
        [Required]
        public DateTime EventDate { get; set; }
        public string? ImageUrl { get; set; }
        public int VenueId { get; set; }
        [ForeignKey("VenueId")]
        public virtual Venue? Venue { get; set; }
        public virtual ICollection<Booking>? Bookings { get; set; }
    }
}
ApplicationDbContext.cs 
using Microsoft.EntityFrameworkCore;
using CLDV6211_Assignment_Part_1_St10449059.Models;

namespace CLDV6211_Assignment_Part_1_St10449059.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Venue> Venues { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Booking> Bookings { get; set; }
    }
}
# Program.cs
using Microsoft.EntityFrameworkCore;
using CLDV6211_Assignment_Part_1_St10449059.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllersWithViews();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

HomeController.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CLDV6211_Assignment_Part_1_St10449059.Data;

namespace CLDV6211_Assignment_Part_1_St10449059.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        public HomeController(ApplicationDbContext context) { _context = context; }

        public async Task<IActionResult> Index()
        {
            var venues = await _context.Venues.ToListAsync();
            ViewBag.Events = await _context.Events.Include(e => e.Venue).ToListAsync();
            ViewBag.Bookings = await _context.Bookings.Include(b => b.Event).ToListAsync();
            return View(venues);
        }
    }
}
 
Main Dashboard View
@model IEnumerable<CLDV6211_Assignment_Part_1_St10449059.Models.Venue>

<div class="container text-center py-5">
    <h1 class="display-4 text-primary fw-bold">EventEase Dashboard</h1>
    <div class="row g-4 mt-4">
        <div class="col-md-4"><a asp-controller="Venues" asp-action="Index" class="btn btn-primary w-100 p-4">📍 Manage Venues</a></div>
        <div class="col-md-4"><a asp-controller="Events" asp-action="Index" class="btn btn-success w-100 p-4">📅 Manage Events</a></div>
        <div class="col-md-4"><a asp-controller="Bookings" asp-action="Index" class="btn btn-info w-100 p-4 text-white">🎟️ Manage Bookings</a></div>
    </div>
</div>

<div class="container pb-5">
    <h3 class="mt-4">Registered Venues</h3>
    <table class="table table-hover shadow-sm bg-white">
        <thead class="table-dark">
            <tr>
                <th>Image</th>
                <th>Name</th>
                <th>Location</th>
            </tr>
        </thead>
        <tbody>
            @if (Model != null && Model.Any())
            {
                @foreach (var item in Model)
                {
                    <tr>
                        <td>
                            <img src="@(string.IsNullOrEmpty(item.ImageUrl) ? "https://via.placeholder.com/60" : item.ImageUrl)"
                                 width="60" height="40" style="object-fit: cover; border-radius: 4px;" />
                        </td>
                        <td><strong>@item.VenueName</strong></td>
                        <td>@item.Location</td>
                    </tr>
                }
            }
        </tbody>
    </table>
</div>
 
# References: 
•	Connolly, T. M. & Begg, C. E. (2015). Database Systems: A Practical Approach to Design, Implementation, and Management. 6th edition. Pearson Education.
•	Coyne, J. (2021). CSS Refactoring: Architecting Systems for Change. 2nd edition. O'Reilly Media.
•	Elmasri, R. & Navathe, S. B. (2017). Fundamentals of Database Systems. 7th edition. Pearson.
•	Freeman, A. (2022). Pro ASP.NET Core 6: Develop Cloud-Ready Web Applications Using MVC, Blazor, and Razor Pages. 9th edition. Apress.
•	Lerman, J. & Miller, R. (2015). Programming Entity Framework: DbContext. 2nd edition. O'Reilly Media.
•	Lucid Software Inc. (2026). Lucidchart Cloud-based Visual Workspace. [Online]. Available at: https://www.lucidchart.com [Accessed 14 April 2026].
•	Microsoft. (2023). ASP.NET Core Middleware. [Online]. Available at: https://learn.microsoft.com/en-us/aspnet/core/fundamentals/middleware/ [Accessed 14 April 2026].
•	Microsoft. (2023). Configuration in ASP.NET Core. [Online]. Available at: https://learn.microsoft.com/en-us/aspnet/core/fundamentals/configuration/ [Accessed 14 April 2026].
•	Microsoft. (2023). Model-View-Controller (MVC) Pattern. [Online]. Available at: https://learn.microsoft.com/en-us/aspnet/core/mvc/overview [Accessed 14 April 2026].
•	Microsoft. (2023). Primary and Foreign Key Constraints. [Online]. Available at: https://learn.microsoft.com/en-us/sql/relational-databases/tables/primary-and-foreign-key-constraints [Accessed 14 April 2026].
