using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Zealand_Lokale_Booking_Library.Models;
using Zealand_Lokale_Booking_Library.Services;

namespace Zealand_Lokale_Booking_UI.Pages
{
    public class BookingModel : PageModel
    {
        

        public BookingModel()
        {
        }
        public List<Booking> AvailableBookings { get; set; }

        public List<RoomBooking> Bookings { get; set; } = new();
        public List<string> Departments { get; set; } = new();
        public List<string> Buildings { get; set; } = new();
        public List<int> Floors { get; set; } = new();
        public List<string> Types { get; set; } = new();
        public List<string> Rooms { get; set; } = new();
        public List<string> Times { get; set; } = new();
        public DateTime? Date { get; set; }

        [BindProperty] public List<string> SelectedDepartments { get; set; } = new();
        public List<SelectListItem> DepartmentOptions { get; set; }


        [BindProperty] public string SelectedDepartment { get; set; }
        [BindProperty] public string SelectedBuilding { get; set; }
        [BindProperty] public int SelectedFloor { get; set; }
        [BindProperty] public string SelectedRoom { get; set; }
        [BindProperty] public string SelectedTime { get; set; }
        [BindProperty] public string SelectedType { get; set; }
        [BindProperty] public DateTime? SelectedDate { get; set; }

        public void OnGet()
        {
            AvailableBookings = new()
            {
                new Booking { BookingID = null, Date = new DateTime(2025,1,12), StartTime = new TimeSpan(8,0,0), UserID = null, UserName = null, RoomID = 101, RoomName = "Lokale 101", Level = "1", RoomTypeID = 1, RoomType = "Undervisning", Capacity = 24, BuildingID = 1, BuildingName = "Hovedbygning", DepartmentID = 10, DepartmentName = "Datamatiker", SmartBoardID = null },
                new Booking { BookingID = 12, Date = new DateTime(2025,1,12), StartTime = new TimeSpan(10,0,0), UserID = 8, UserName = "Sara", RoomID = 102, RoomName = "Lokale 102", Level = "1", RoomTypeID = 2, RoomType = "Mødelokale", Capacity = 12, BuildingID = 1, BuildingName = "Hovedbygning", DepartmentID = 10, DepartmentName = "Datamatiker", SmartBoardID = 3 },
                new Booking { BookingID = null, Date = new DateTime(2025,1,13), StartTime = new TimeSpan(9,0,0), UserID = null, UserName = null, RoomID = 103, RoomName = "Lokale 103", Level = "1", RoomTypeID = 1, RoomType = "Undervisning", Capacity = 30, BuildingID = 1, BuildingName = "Hovedbygning", DepartmentID = 11, DepartmentName = "Markedsføring", SmartBoardID = null },
                new Booking { BookingID = 44, Date = new DateTime(2025,1,13), StartTime = new TimeSpan(13,0,0), UserID = 21, UserName = "Oliver", RoomID = 104, RoomName = "Lokale 104", Level = "2", RoomTypeID = 3, RoomType = "IT-Lab", Capacity = 20, BuildingID = 2, BuildingName = "Labbygning", DepartmentID = 12, DepartmentName = "IT", SmartBoardID = 7 },
                new Booking { BookingID = null, Date = new DateTime(2025,1,14), StartTime = new TimeSpan(8,30,0), UserID = null, UserName = null, RoomID = 105, RoomName = "Lokale 105", Level = "2", RoomTypeID = 1, RoomType = "Undervisning", Capacity = 18, BuildingID = 1, BuildingName = "Hovedbygning", DepartmentID = 10, DepartmentName = "Datamatiker", SmartBoardID = null },
                new Booking { BookingID = 77, Date = new DateTime(2025,1,14), StartTime = new TimeSpan(14,0,0), UserID = 3, UserName = "Frederik", RoomID = 106, RoomName = "Lokale 106", Level = "3", RoomTypeID = 2, RoomType = "Mødelokale", Capacity = 10, BuildingID = 3, BuildingName = "Bygning C", DepartmentID = 13, DepartmentName = "Økonomi", SmartBoardID = null },
                new Booking { BookingID = null, Date = new DateTime(2025,1,15), StartTime = new TimeSpan(11,0,0), UserID = null, UserName = null, RoomID = 107, RoomName = "Lokale 107", Level = "3", RoomTypeID = 1, RoomType = "Undervisning", Capacity = 20, BuildingID = 3, BuildingName = "Bygning C", DepartmentID = 13, DepartmentName = "Økonomi", SmartBoardID = null },
                new Booking { BookingID = 91, Date = new DateTime(2025,1,15), StartTime = new TimeSpan(15,0,0), UserID = 16, UserName = "Emil", RoomID = 108, RoomName = "Lokale 108", Level = "1", RoomTypeID = 3, RoomType = "IT-Lab", Capacity = 28, BuildingID = 2, BuildingName = "Labbygning", DepartmentID = 12, DepartmentName = "IT", SmartBoardID = 2 },
                new Booking { BookingID = null, Date = new DateTime(2025,1,16), StartTime = new TimeSpan(9,30,0), UserID = null, UserName = null, RoomID = 109, RoomName = "Lokale 109", Level = "2", RoomTypeID = 2, RoomType = "Mødelokale", Capacity = 16, BuildingID = 1, BuildingName = "Hovedbygning", DepartmentID = 11, DepartmentName = "Markedsføring", SmartBoardID = null },
                new Booking { BookingID = 102, Date = new DateTime(2025,1,16), StartTime = new TimeSpan(12,0,0), UserID = 5, UserName = "Laura", RoomID = 110, RoomName = "Lokale 110", Level = "2", RoomTypeID = 1, RoomType = "Undervisning", Capacity = 22, BuildingID = 1, BuildingName = "Hovedbygning", DepartmentID = 10, DepartmentName = "Datamatiker", SmartBoardID = 1 }
            };

            DepartmentOptions = new()
            {
                new SelectListItem { Value = "1", Text = "Roskile" },
                new SelectListItem { Value = "2", Text = "Køge" },
                new SelectListItem { Value = "3", Text = "Slagelse" }
            };

            Departments = new List<string> { "Roskilde", "Køge", "Slagelse", "Næstved", "Holbæk", "Nykøbing Falster", "Nødebo" };
            Buildings = new List<string> { "A", "D" };
            Floors = new List<int> { 1, 2, 3 };
            Rooms = new List<string> { "1", "2" };
            Times = new List<string> { "08-10", "10-12", "12-14", "14-16" };
            Types = new List<string> { "Klasselokale", "Study Room", "Auditorium" };
        }
    }
}