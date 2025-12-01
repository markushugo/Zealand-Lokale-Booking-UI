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
        private readonly CreateBookingRepo _createBookingRepo=new CreateBookingRepo("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=ZealandBooking;Integrated Security=True;Encrypt=False;TrustServerCertificate=False;");

        public BookingModel(FilterRepository filterRepository)
        {
            _filterRepository = filterRepository;
        }

        public FilterOptions Filters { get; set; } = new();
        public BookingFilter BookingFilter { get; set; } = new BookingFilter { UserID = 1 };
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

            BuildBookingFilterFromSelection();
            _populate();
            return Page();
        }

        private void BuildBookingFilterFromSelection()
        {
            BookingFilter = new BookingFilter
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
        }

        public async Task<IActionResult> OnPostCreateBookingAsync(int roomId, DateTime date, string time)
        {
            try
            {
                // "10:00-12:00" "10:00"
                var startTime = TimeSpan.Parse(time.Split('-')[0]);
                await _createBookingRepo.CreateBookingAsync(
                    1,          // userId (later from session)
                    roomId,
                    date,
                    startTime,
                    null
                );

                TempData["SuccessMessage"] = "Booking created!";
            }
            catch (ArgumentException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return Page();
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "An unexpected error occurred: " + ex.Message;
            }
            BuildBookingFilterFromSelection();
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
        private void _populate()
        {
            AvailableBookings = _filterRepository.GetAvailableBookingSlots((BookingFilter)).ToList();
            int userId = 1; // TODO: change when there is login
            var filterData = _filterRepository.GetFilterOptionsForUserAsync(userId).Result;

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
        }
    }
}