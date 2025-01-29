using Microsoft.EntityFrameworkCore;
using TheatreProject.Models;
using TheatreProject.Services;

///////////////
// routing used to be handled like this
//     app.MapControllerRoute(
//                     name: "default",
//                     pattern: "{controller=Home}/{action=Index}/{id?}");
// which automatically routes e.g. "Login/ViewLoginPage" to LoginController.ViewLoginPage()
// you can override routes using attribute, make sure to include the Controller name ([HttpPost("/Login/api/LoginAction")])
///////////////
//but in some instances using Attributes still appeared to be required to work (for example in calling Logout from _Layout.cshtml)
//I'm not sure WHY. But because using BOTH automated routing AND manual attributes makes it vague when we need to use one or the other, 
//let's stick with using just attributes

//User interacts with buttons/links in Views, such as 

//a. the navbar in Views/Shared/_Layout.cshtml:
//  <li class="nav-item">
//     <a class="nav-link text-dark" asp-area="" asp-controller="Login"
//         asp-action="ViewLoginPage">Login</a>
// </li>
//this interaction calls from a controller ("Login"Controller) a method named "ViewLoginPage", THIS REQUIRES ATTRIBUTE

//b. Login form in Views/Login/Login.cshtml:
// <form asp-controller="Login" asp-action="LoginAction" method="post">
//     Username: <input type="text" name="username" />
//     Password: <input type="password" name="password" />
//     <button>Login</button>
// </form>
//      also works with just <form action="LoginAction" method="post"> 
//      because The Login Controller is likely associated to this view behind the scenes by AddControllersWithViews();
//this works WITHOUT ATTRIBUTE, likely because method is specified in the call

//methods in a controller could thus either:
//a. return a different View() (and redirect the user to a new page)
//b. be an API call, and CRUD something in the model layer (and potentially change the View as well)




namespace TheatreProject
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllersWithViews();

        


            builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            builder.Services.AddDistributedMemoryCache();
            builder.Services.AddSession(options => 
            {
                options.IdleTimeout = TimeSpan.FromMinutes(40);
                options.Cookie.HttpOnly = true; 
                options.Cookie.IsEssential = true; 
            });

            builder.Services.AddDbContext<DatabaseContext>(
                options => options.UseSqlite(builder.Configuration.GetConnectionString("SqlLiteDb")));

            builder.Services.AddScoped<ILoginService, LoginService>();
            builder.Services.AddScoped<IVenueService, VenueService>();
            builder.Services.AddScoped<ITheatreShowService, TheatreShowService>();
            builder.Services.AddScoped<ITheatreShowDateService, TheatreShowDateService>();
            builder.Services.AddScoped<IReservationService, ReservationService>();
            builder.Services.AddScoped<ICustomerService, CustomerService>();
            
            var app = builder.Build();
            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            //app.UseHttpsRedirection();
            app.UseStaticFiles();
            //app.UseRouting();
            //app.rout
            //app.UseAuthorization();

            
           
            
            app.UseSession();

            //app.UseStaticFiles(); //:OOO
        
            app.MapControllers();
            app.MapFallbackToFile("{controller=Login}","wwwroot/js/main.js");

            // app.MapControllerRoute(
            //     name: "default",
            //     pattern: "");
                //example: HomeController

            // app.MapControllerRoute(
            //     name: "default",
            //     pattern: "{controller=Home}/api/{action=Index}/{id?}");
            app.Use((context, next) => 
            {
                //context.Request.
                var a = context.Request;
                return next.Invoke();

            });

            app.Run();
            

        }
    }
}