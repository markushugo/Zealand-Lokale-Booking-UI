using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Zealand_Lokale_Booking_Library.Models;
using Zealand_Lokale_Booking_Library.Repos;

namespace Zealand_Lokale_Booking_UI.Pages
{
    public class MyBookingsModel : PageModel
    {
        private readonly GetBookingsRepo _getBookingsRepo = new GetBookingsRepo("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=ZealandBooking;Integrated Security=True;Encrypt=False;TrustServerCertificate=False;");

        public List<Booking> MyBookings { get; set; } = new();

        public async Task OnGetAsync()
        {
            // TODO: replace with logged-in user ID when authentication is implemented
            int userId = 1;
            MyBookings = await _getBookingsRepo.GetBookingsByUserIdAsync(userId);
        }
    }
}
