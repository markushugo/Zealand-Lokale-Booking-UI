using Microsoft.AspNetCore.Mvc.RazorPages;
using Zealand_Lokale_Booking_Library.Models;
using Zealand_Lokale_Booking_Library.Services;

namespace Zealand_Lokale_Booking_UI.Pages
{
    public class BookingModel : PageModel
    {
        //private readonly IBookingService _bookingService;

        //public BookingModel(IBookingService bookingService)
        //{
        //    _bookingService = bookingService;
        //}
        //public List<RoomBooking> Bookings { get; set; } = new();

        //public List<string> Departments { get; set; } = new();
        //public List<string> Buildings { get; set; } = new();
        //public List<int> Floors { get; set; } = new();
        //public List<string> Types { get; set; } = new();
        //public List<string> Rooms { get; set; } = new();
        //public List<string> Times { get; set; } = new();

        //public void OnGet()
        //{
        //    // Collects data from SQL
        //    Departments = _bookingService.GetDepartments().ToList();
        //    Buildings = _bookingService.GetBuildings().ToList();
        //    Floors = _bookingService.GetFloors().ToList();
        //    Types = _bookingService.GetTypes().ToList();
        //    Rooms = _bookingService.GetRooms().ToList();
        //    Times = _bookingService.GetTimes().ToList();

        //    Bookings = _bookingService.GetBookings(new BookingFilter()).ToList();
        //}
    }
}