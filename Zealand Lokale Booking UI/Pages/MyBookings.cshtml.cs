using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Zealand_Lokale_Booking_Library.Models;
using Zealand_Lokale_Booking_Library.Repos;
using Zealand_Lokale_Booking_Library.Services;

namespace Zealand_Lokale_Booking_UI.Pages
{
    public class MyBookingsModel : PageModel
    {
        private readonly IBookingService _bookingService;

        public MyBookingsModel(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        public List<Booking> MyBookings { get; set; } = new();

        public async Task OnGetAsync()
        {
            // TODO: replace with logged-in user ID when authentication is implemented
            int userId = 1;
            MyBookings = await _bookingService.GetBookingsByUserIdAsync(userId);
        }
        public async Task<IActionResult> OnPostDeleteAsync(int bookingId)
        {
            // TODO: når der kommer login, hentes userId fra session/claims
            int userId = 1;

        try
            {
                await _bookingService.DeleteBookingAsync(bookingId, userId);
                TempData["SuccessMessage"] = "Bookingen blev slettet.";
            }

        catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }

            // Get bookings again to refresh the list
            MyBookings = await _bookingService.GetBookingsByUserIdAsync(userId);
            return Page();
        }
    }
}
