using Zealand_Lokale_Booking_Library.Models;
using Zealand_Lokale_Booking_Library.Services;

namespace Zealand_Lokale_Booking_UI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            //string _connectionString = "  ";

            // Add services to the container.
            builder.Services.AddRazorPages();

            builder.Services.AddScoped<FilterRepository>(sp =>
            {
                var config = sp.GetRequiredService<IConfiguration>();
                var connStr = "Server=(localdb)\\MSSQLlocaldb; database=ZealandBooking; encrypt=false; integrated security=true;";
                return new FilterRepository(connStr);
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapRazorPages();

            app.Run();
        }
    }
}
