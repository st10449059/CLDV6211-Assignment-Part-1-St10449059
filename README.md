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

3. IaaS, PaaS, and SaaS: The "Pizza as a Service" Comparison

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
https://github.com/st10449059/CLDV6211-Assignment-Part-1-St10449059

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
 
# Refrences 
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
