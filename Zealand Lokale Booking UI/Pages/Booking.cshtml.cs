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
        public List<Booking> AvailableBookings { get; set; } = new();

        public List<SelectListItem> DepartmentOptions { get; set; } = new();
        public List<SelectListItem> BuildingOptions { get; set; } = new();
        public List<SelectListItem> LevelOptions { get; set; } = new();
        public List<SelectListItem> RoomTypeOptions { get; set; } = new();
        public List<SelectListItem> TimeOptions { get; set; } = new();
        public DateTime? Date { get; set; }

        [BindProperty] public List<int> SelectedDepartments { get; set; } = new();
        [BindProperty] public List<int> SelectedBuildings { get; set; } = new();
        [BindProperty] public List<string> SelectedLevels { get; set; } = new();
        [BindProperty] public List<int> SelectedRoomTypes { get; set; } = new();
        [BindProperty] public List<int> SelectedTimes { get; set; } = new();
        [BindProperty] public DateTime SelectedDate { get; set; } = DateTime.Today;

        public IActionResult OnPostFilter()
        {
            if (!ModelState.IsValid)
            {
                _populate();
                return Page();
            }
            var filter = new BookingFilter
            {
                UserID = 1,
                Date = SelectedDate,
                DepartmentIds = _nullIfEmpty(SelectedDepartments),
                BuildingIds = _nullIfEmpty(SelectedBuildings),
                RoomIds = null,
                RoomTypeIds = _nullIfEmpty(SelectedRoomTypes),
                Levels = _nullIfEmpty(SelectedLevels),
                Times = _convertHours(SelectedTimes)
            };
            // ====== DEBUG OUTPUT ======
            Console.WriteLine("===== BOOKING FILTER =====");
            Console.WriteLine($"UserID: {filter.UserID}");
            Console.WriteLine($"Date: {filter.Date:yyyy-MM-dd}");

            Console.WriteLine($"Departments: {_formatList(filter.DepartmentIds)}");
            Console.WriteLine($"Buildings: {_formatList(filter.BuildingIds)}");
            Console.WriteLine($"RoomIds (legacy): {_formatList(filter.RoomIds)}");
            Console.WriteLine($"RoomTypes: {_formatList(filter.RoomTypeIds)}");
            Console.WriteLine($"Levels: {_formatList(filter.Levels)}");

            Console.WriteLine("Times: " +
                (filter.Times == null ? "null" : string.Join(", ", filter.Times.Select(t => t.ToString("HH:mm")))));

            Console.WriteLine("===========================");

            _populate();
            return Page();
        }

        // helpers
        private static List<T>? _nullIfEmpty<T>(List<T> list) =>
            list.Count > 0 ? list : null;

        private static List<TimeOnly>? _convertHours(List<int> hours) =>
            hours.Count > 0
                ? hours.Select(h => new TimeOnly(h, 0)).ToList()
                : null;

        private static string _formatList<T>(List<T>? list) =>
            list == null ? "null" : string.Join(", ", list);

        [TempData] public string TempBookingRoomID { get; set; }
        [TempData] public string TempBookingTime { get; set; }
        [TempData] public DateTime? TempBookingDate { get; set; }
        [TempData] public string TempRoomName { get; set; }
        [TempData] public string TempBuildingName { get; set; }
        [TempData] public string TempDepartmentName { get; set; }

        public bool ShowPopup { get; set; } = false;

       

        private void LoadData()

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
        }
        public void OnGet()
        {
            LoadData();

            if (!string.IsNullOrEmpty(TempRoomName))
            {
                ShowPopup = true;
            }

          

            DepartmentOptions = new()
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

            Departments = new List<string> { "Roskilde", "Køge", "Slagelse", "Næstved", "Holbæk", "Nykøbing Falster", "Nødebo" };
            Buildings = new List<string> { "A", "D" };
            Floors = new List<int> { 1, 2, 3 };
            Rooms = new List<string> { "1", "2" };
            Times = new List<string> { "08-10", "10-12", "12-14", "14-16" };
            Types = new List<string> { "Klasselokale", "Study Room", "Auditorium" };
        }
        public IActionResult OnPostPrepareBooking(int roomId)
        {
            LoadData();
            var booking = AvailableBookings.FirstOrDefault(b => b.RoomID == roomId);

            if (booking != null)
            {
                TempRoomName = booking.RoomName;
                TempBuildingName = booking.BuildingName;
                TempBookingRoomID = booking.RoomID.ToString();
                TempBookingDate = booking.Date;
                TempBookingTime = booking.StartTime.ToString(@"hh\:mm") + "-" + booking.StartTime.Add(new TimeSpan(2, 0, 0)).ToString(@"hh\:mm");
                TempDepartmentName = booking.DepartmentName;

            }
            return RedirectToPage();
        }
    }
}