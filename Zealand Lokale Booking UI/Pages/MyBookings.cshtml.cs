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

        public List<Booking>? MyBookings { get; set; } = new();

        public async Task OnGetAsync()
        {

            var userId = HttpContext.Session.GetInt32("UserID");
            try
            {
                MyBookings = await _bookingService.GetBookingsByUserIdAsync((int)userId);
            }
            catch (Exception ex) 
            {
                MyBookings = null;
            }
        }
        public async Task<IActionResult> OnPostDeleteAsync(int bookingId)
        {
            var userId = HttpContext.Session.GetInt32("UserID");

            try
            {
                await _bookingService.DeleteBookingAsync(bookingId, (int)userId);
                TempData["SuccessMessage"] = "Bookingen blev slettet.";
            }

            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }

            // Get bookings again to refresh the list
            MyBookings = await _bookingService.GetBookingsByUserIdAsync((int)userId);
            return Page();
        }
    }
}
