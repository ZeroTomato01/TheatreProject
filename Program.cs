using Microsoft.EntityFrameworkCore;
using TheatreProject.Models;
using TheatreProject.Services;

namespace TheatreProject
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDistributedMemoryCache();
            builder.Services.AddSession(options => 
            {
                options.IdleTimeout = TimeSpan.FromMinutes(40);
                options.Cookie.HttpOnly = true; 
                options.Cookie.IsEssential = true; 
            });

            builder.Services.AddControllers();

            builder.Services.AddDbContext<DatabaseContext>(
                options => options.UseSqlite(builder.Configuration.GetConnectionString("SqlLiteDb")));

            builder.Services.AddScoped<ILoginService, LoginService>();
            builder.Services.AddScoped<IVenueService, VenueService>();
            builder.Services.AddScoped<ITheatreShowService, TheatreShowService>();
            builder.Services.AddScoped<ITheatreShowDateService, TheatreShowDateService>();
            builder.Services.AddScoped<IReservationService, ReservationService>();
            builder.Services.AddScoped<ICustomerService, CustomerService>();
            
            
            var app = builder.Build();
            app.UseSession();
            app.UseStaticFiles();
            app.MapControllers();
            app.MapFallbackToFile("index.html");

            app.Use((context, next) => 
            {
                var a = context.Request;
                return next.Invoke();

            });

            app.Run();
        }
    }
}