using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Zealand_Lokale_Booking_UI.Pages
{
    public class BookingModel : PageModel
    {
        public List<string> Departments { get; set; } = new();
        public List<string> Buildings { get; set; } = new();
        public List<string> Floors { get; set; } = new();
        public List<string> Types { get; set; } = new();
        public List<string> Rooms { get; set; } = new();
        public List<string> Times { get; set; } = new();

        public void OnGet()
        {
            Departments = new List<string> { "Roskilde", "Køge", "Slagelse", "Næstved", "Holbæk", "Nykøbing Falster", "Nødebo" };
            Buildings = new List<string> { "A", "D" };
            Floors = new List<string> { "1", "2", "3" };
            Rooms = new List<string> { "1", "2" };
            Times = new List<string> { "08-10", "10-12", "12-14", "14-16" };
            Types = new List<string> { "Klasselokale", "Study Room", "Auditorium" };
        }

    }
}
