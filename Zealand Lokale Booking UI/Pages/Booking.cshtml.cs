using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Zealand_Lokale_Booking_Library.Models;
using Zealand_Lokale_Booking_Library.Services;

namespace Zealand_Lokale_Booking_UI.Pages
{
    public class BookingModel : PageModel
    {
        // Add this property to fix CS1061
        public DateTime? Date { get; set; }

        public BookingModel()
        {
        }
        public List<RoomBooking> Bookings { get; set; } = new();

        public List<string> Departments { get; set; } = new();
        public List<string> Buildings { get; set; } = new();
        public List<int> Floors { get; set; } = new();
        public List<string> Types { get; set; } = new();
        public List<string> Rooms { get; set; } = new();
        public List<string> Times { get; set; } = new();

        [BindProperty] public string SelectedDepartment { get; set; }
        [BindProperty] public string SelectedBuilding { get; set; }
        [BindProperty] public int SelectedFloor { get; set; }
        [BindProperty] public string SelectedRoom { get; set; }
        [BindProperty] public string SelectedTime { get; set; }
        [BindProperty] public string SelectedType { get; set; }
        [BindProperty] public DateTime? SelectedDate { get; set; }

        public void OnGet()
        {
            Departments = new List<string> { "Roskilde", "Køge", "Slagelse", "Næstved", "Holbæk", "Nykøbing Falster", "Nødebo" };
            Buildings = new List<string> { "A", "D" };
            Floors = new List<int> { 1, 2, 3 };
            Rooms = new List<string> { "1", "2" };
            Times = new List<string> { "08-10", "10-12", "12-14", "14-16" };
            Types = new List<string> { "Klasselokale", "Study Room", "Auditorium" };
        }
    }
}