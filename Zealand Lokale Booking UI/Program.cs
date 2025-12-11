using Zealand_Lokale_Booking_Library.Repos;
using Zealand_Lokale_Booking_Library.Services;
using Zealand_Lokale_Booking_Library.Repos;

namespace Zealand_Lokale_Booking_UI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            string _connectionString =
                "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=ZealandBooking;Integrated Security=True;Encrypt=False;TrustServerCertificate=False;";

            // Razor Pages
            builder.Services.AddRazorPages();

            // -------------------------------
            // Register interfaces -> implementations
            // -------------------------------
            //builder.Services.AddSingleton<ICreateBookingRepo>(new CreateBookingRepo(_connectionString));
            //builder.Services.AddSingleton<ICreateBookingService, CreateBookingService>();

            // Session support
            builder.Services.AddRazorPages(options =>
            {
                options.Conventions.AddPageRoute("/LoginPage", "");
            });
            builder.Services.AddDistributedMemoryCache();
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(60);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            builder.Services.AddHttpContextAccessor();
            builder.Services.AddScoped<ManageBookingRepo>(sp =>
                new ManageBookingRepo(_connectionString));
            builder.Services.AddScoped<FilterRepository>(sp =>
            {
                var config = sp.GetRequiredService<IConfiguration>();
                var connStr = "Server=(localdb)\\MSSQLlocaldb; database=ZealandBooking; encrypt=false; integrated security=true;";
                return new FilterRepository(connStr);
            });

            builder.Services.AddScoped<IUserRepo>(sp =>
                new UserRepo(_connectionString));
            builder.Services.AddScoped<IUserService, UserService>();

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
