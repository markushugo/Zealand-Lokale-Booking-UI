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

        public List<SelectListItem> DepartmentOptions { get; set; } = new();
        public List<SelectListItem> BuildingOptions { get; set; } = new();
        public List<SelectListItem> LevelOptions { get; set; } = new();
        public List<SelectListItem> RoomTypeOptions { get; set; } = new();
        public List<SelectListItem> TimeOptions { get; set; } = new();
        public DateTime? Date { get; set; }

        [BindProperty] public List<int> SelectedDepartments { get; set; } = new();
        [BindProperty] public List<int> SelectedBuildings { get; set; } = new();
        [BindProperty] public List<int> SelectedLevels { get; set; } = new();
        [BindProperty] public List<int> SelectedRoomTypes { get; set; } = new();
        [BindProperty] public List<int> SelectedTimes { get; set; } = new();
        [BindProperty] public DateTime SelectedDate { get; set; } = DateTime.Today;

        public IActionResult OnPost()
        {
            Console.WriteLine("Selected Departments:");
            foreach (var id in SelectedDepartments)
                Console.WriteLine(" - " + id);
            Console.WriteLine("Selected Buildings:");
            foreach (var id in SelectedBuildings)
                Console.WriteLine(" - " + id);
            Console.WriteLine("Selected Levels:");
            foreach (var id in SelectedLevels)
                Console.WriteLine(" - " + id);
            Console.WriteLine("Selected Room Types:");
            foreach (var id in SelectedRoomTypes)
                Console.WriteLine(" - " + id);
            Console.WriteLine("Selected Times:");
            foreach (var id in SelectedTimes)
                Console.WriteLine(" - " + id);
            Console.WriteLine($"Selected Date: {SelectedDate}");
            _populate();
            return Page();
        }

        public void OnGet()
        {
            _populate();
        }

        private void _populate()
        {
            AvailableBookings = new()
            {
                new Booking { BookingID = null, Date = new DateTime(2025,1,12), StartTime = new TimeSpan(8,0,0), UserID = null, UserName = null, RoomID = 101, RoomName = "Lokale 101", Level = "1", RoomTypeID = 1, RoomType = "Undervisning", Capacity = 24, BuildingID = 1, BuildingName = "Hovedbygning", DepartmentID = 10, DepartmentName = "Datamatiker", SmartBoardID = null },
                new Booking { BookingID = null, Date = new DateTime(2025,1,12), StartTime = new TimeSpan(10,0,0), UserID = 8, UserName = "Sara", RoomID = 102, RoomName = "Lokale 102", Level = "1", RoomTypeID = 2, RoomType = "Mødelokale", Capacity = 12, BuildingID = 1, BuildingName = "Hovedbygning", DepartmentID = 10, DepartmentName = "Datamatiker", SmartBoardID = 3 },
                new Booking { BookingID = null, Date = new DateTime(2025,1,13), StartTime = new TimeSpan(9,0,0), UserID = null, UserName = null, RoomID = 103, RoomName = "Lokale 103", Level = "1", RoomTypeID = 1, RoomType = "Undervisning", Capacity = 30, BuildingID = 1, BuildingName = "Hovedbygning", DepartmentID = 11, DepartmentName = "Markedsføring", SmartBoardID = null },
                new Booking { BookingID = null, Date = new DateTime(2025,1,13), StartTime = new TimeSpan(13,0,0), UserID = 21, UserName = "Oliver", RoomID = 104, RoomName = "Lokale 104", Level = "2", RoomTypeID = 3, RoomType = "IT-Lab", Capacity = 20, BuildingID = 2, BuildingName = "Labbygning", DepartmentID = 12, DepartmentName = "IT", SmartBoardID = 7 },
                new Booking { BookingID = null, Date = new DateTime(2025,1,14), StartTime = new TimeSpan(8,30,0), UserID = null, UserName = null, RoomID = 105, RoomName = "Lokale 105", Level = "2", RoomTypeID = 1, RoomType = "Undervisning", Capacity = 18, BuildingID = 1, BuildingName = "Hovedbygning", DepartmentID = 10, DepartmentName = "Datamatiker", SmartBoardID = null },
                new Booking { BookingID = null, Date = new DateTime(2025,1,14), StartTime = new TimeSpan(14,0,0), UserID = 3, UserName = "Frederik", RoomID = 106, RoomName = "Lokale 106", Level = "3", RoomTypeID = 2, RoomType = "Mødelokale", Capacity = 10, BuildingID = 3, BuildingName = "Bygning C", DepartmentID = 13, DepartmentName = "Økonomi", SmartBoardID = null },
                new Booking { BookingID = null, Date = new DateTime(2025,1,15), StartTime = new TimeSpan(11,0,0), UserID = null, UserName = null, RoomID = 107, RoomName = "Lokale 107", Level = "3", RoomTypeID = 1, RoomType = "Undervisning", Capacity = 20, BuildingID = 3, BuildingName = "Bygning C", DepartmentID = 13, DepartmentName = "Økonomi", SmartBoardID = null },
                new Booking { BookingID = null, Date = new DateTime(2025,1,15), StartTime = new TimeSpan(15,0,0), UserID = 16, UserName = "Emil", RoomID = 108, RoomName = "Lokale 108", Level = "1", RoomTypeID = 3, RoomType = "IT-Lab", Capacity = 28, BuildingID = 2, BuildingName = "Labbygning", DepartmentID = 12, DepartmentName = "IT", SmartBoardID = 2 },
                new Booking { BookingID = null, Date = new DateTime(2025,1,16), StartTime = new TimeSpan(9,30,0), UserID = null, UserName = null, RoomID = 109, RoomName = "Lokale 109", Level = "2", RoomTypeID = 2, RoomType = "Mødelokale", Capacity = 16, BuildingID = 1, BuildingName = "Hovedbygning", DepartmentID = 11, DepartmentName = "Markedsføring", SmartBoardID = null },
                new Booking { BookingID = null, Date = new DateTime(2025,1,16), StartTime = new TimeSpan(12,0,0), UserID = 5, UserName = "Laura", RoomID = 110, RoomName = "Lokale 110", Level = "2", RoomTypeID = 1, RoomType = "Undervisning", Capacity = 22, BuildingID = 1, BuildingName = "Hovedbygning", DepartmentID = 10, DepartmentName = "Datamatiker", SmartBoardID = 1 }
            };

            DepartmentOptions = new List<SelectListItem>
            {
                new SelectListItem { Value = "1", Text = "Roskile" },
                new SelectListItem { Value = "2", Text = "Køge" },
            };
            BuildingOptions = new List<SelectListItem>
            {
                new SelectListItem { Value = "1", Text = "A" },
                new SelectListItem { Value = "2", Text = "D" }
            };
            LevelOptions = new List<SelectListItem>
            {
                new SelectListItem { Value = "1", Text = "1" },
                new SelectListItem { Value = "2", Text = "2" },
                new SelectListItem { Value = "3", Text = "3" }
            };
            RoomTypeOptions = new List<SelectListItem>
            {
                new SelectListItem { Value = "1", Text = "Classroom" },
                new SelectListItem { Value = "2", Text = "Studyroom" },
                new SelectListItem { Value = "3", Text = "Auditorium" }
            };
            TimeOptions = new List<SelectListItem>
            {
                new SelectListItem { Value = "8", Text = "8-10" },
                new SelectListItem { Value = "10", Text = "10-12" },
                new SelectListItem { Value = "12", Text = "12-14" },
                new SelectListItem { Value = "14", Text = "14-16" }
            };
        }

        
    }
}