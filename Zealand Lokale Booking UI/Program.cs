using Zealand_Lokale_Booking_Library.Repos;
using Zealand_Lokale_Booking_Library.Services;

namespace Zealand_Lokale_Booking_UI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new InvalidOperationException("Connection string 'DefaultConnection' is not configured.");
            }

            // Register repositories from the class library
            builder.Services.AddScoped<ICreateBookingRepo>(sp =>
                new CreateBookingRepo(connectionString));

            builder.Services.AddScoped<IGetBookingsRepo>(sp =>
                new GetBookingsRepo(connectionString));

            builder.Services.AddScoped<IBookingRepo>(sp =>
                new BookingRepo(connectionString));

            builder.Services.AddScoped<IFilterRepo>(sp =>
                new FilterRepo(connectionString));

            builder.Services.AddScoped<IManageBookingRepo>(sp =>
                new ManageBookingRepo(connectionString));

            // Register the BookingService
            builder.Services.AddScoped<IBookingService, BookingService>();

            builder.Services.AddRazorPages();

            // Session support
            builder.Services.AddDistributedMemoryCache();
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(60);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            builder.Services.AddHttpContextAccessor();

            var app = builder.Build();

            // HTTP pipeline
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseSession();
            app.UseAuthorization();

            app.MapRazorPages();
            app.Run();
        }
    }
}
