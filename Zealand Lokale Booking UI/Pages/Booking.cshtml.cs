using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Zealand_Lokale_Booking_Library.Models;
using Zealand_Lokale_Booking_Library.Services;
using Zealand_Lokale_Booking_Library.Repos;

namespace Zealand_Lokale_Booking_UI.Pages
{
    public class BookingModel : PageModel
    {
        private readonly FilterRepository _filterRepository;

        public BookingModel(FilterRepository filterRepository)
        {
            _filterRepository = filterRepository;
        }

        public FilterOptions Filters { get; set; } = new();

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

        public async Task<IActionResult> OnPostFilterAsync()
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
            AvailableBookings = _filterRepository.GetAvailableBookingSlots((filter)).ToList();
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

       


   

        private void _populate()
        {
            // 1) Hent filtermuligheder fra databasen
            int userId = 1; // TODO: skift når du har login
            var filterData = _filterRepository.GetFilterOptionsForUserAsync(userId).Result;

            // 2) Map til dine UI-properties (DepartmentOptions, BuildingOptions osv.)

            DepartmentOptions = filterData.Departments
                .Select(d => new SelectListItem
                {
                    Value = d.Key,
                    Text = d.Value
                })
                .ToList();

            BuildingOptions = filterData.Buildings
                .Select(b => new SelectListItem
                {
                    Value = b.Key,
                    Text = b.Value
                })
                .ToList();

            LevelOptions = filterData.LevelOptions
                .Select(l => new SelectListItem
                {
                    Value = l.Key,
                    Text = l.Value
                })
                .ToList();

            RoomTypeOptions = filterData.RoomTypes
                .Select(rt => new SelectListItem
                {
                    Value = rt.Key,
                    Text = rt.Value
                })
                .ToList();

            TimeOptions = filterData.TimeSlots
                .Select(t => new SelectListItem
                {
                    Value = t.Key,
                    Text = t.Value
                })
                .ToList();
        }
        public void OnGet()
        {
            _populate();
            if (!string.IsNullOrEmpty(TempRoomName))
            {
                ShowPopup = true;
            }



            //DepartmentOptions = new();
            //DepartmentOptions = new List<SelectListItem>
            //{
            //    new SelectListItem { Value = "1", Text = "Roskile" },
            //    new SelectListItem { Value = "2", Text = "Køge" },
            //};
            //BuildingOptions = new List<SelectListItem>
            //{
            //    new SelectListItem { Value = "1", Text = "A" },
            //    new SelectListItem { Value = "2", Text = "D" }
            //};
            //LevelOptions = new List<SelectListItem>
            //{
            //    new SelectListItem { Value = "1", Text = "1" },
            //    new SelectListItem { Value = "2", Text = "2" },
            //    new SelectListItem { Value = "3", Text = "3" }
            //};
            //RoomTypeOptions = new List<SelectListItem>
            //{
            //    new SelectListItem { Value = "1", Text = "Classroom" },
            //    new SelectListItem { Value = "2", Text = "Studyroom" },
            //    new SelectListItem { Value = "3", Text = "Auditorium" }
            //};
            //TimeOptions = new List<SelectListItem>
            //{
            //    new SelectListItem { Value = "8", Text = "8-10" },
            //    new SelectListItem { Value = "10", Text = "10-12" },
            //    new SelectListItem { Value = "12", Text = "12-14" },
            //    new SelectListItem { Value = "14", Text = "14-16" }
            //};

        }
        public IActionResult OnPostPrepareBooking(int roomId)
        {
            
            _populate();
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