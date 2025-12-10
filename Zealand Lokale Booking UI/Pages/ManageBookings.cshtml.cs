using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Zealand_Lokale_Booking_Library.Models;
using Zealand_Lokale_Booking_Library.Services;

namespace Zealand_Lokale_Booking_UI.Pages
{
    public class ManageBookingsModel : PageModel
    {
        private readonly IBookingService _bookingService;

        // Midlertidigt: hardcoded lærer-bruger til test
        private const int CurrentUserId = 5; // TODO: hent fra login senere

        public ManageBookingsModel(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        public FilterOptions Filters { get; set; } = new();
        public BookingFilter BookingFilter { get; set; } = new BookingFilter { UserID = CurrentUserId };
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

        // GET: intial load
        public void OnGet()
        {
            BuildBookingFilterFromSelection();
            Populate();
        }

        // POST: filter
        public IActionResult OnPostFilter()
        {
            if (!ModelState.IsValid)
            {
                Populate();
                return Page();
            }

            BuildBookingFilterFromSelection();
            Populate();
            return Page();
        }

        // POST: Delete booking
        public async Task<IActionResult> OnPostDeleteAsync(int bookingId)
        {
            try
            {
                await _bookingService.DeleteBookingAsync(bookingId, CurrentUserId);
                TempData["SuccessMessage"] = "Bookingen blev slettet.";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Kunne ikke slette bookingen: " + ex.Message;
            }

            BuildBookingFilterFromSelection();
            Populate();
            return Page();
        }

        // ----------------- helpers -----------------

        private void BuildBookingFilterFromSelection()
        {
            BookingFilter = new BookingFilter
            {
                UserID = CurrentUserId,
                Date = SelectedDate,
                DepartmentIds = NullIfEmpty(SelectedDepartments),
                BuildingIds = NullIfEmpty(SelectedBuildings),
                RoomIds = null,
                RoomTypeIds = NullIfEmpty(SelectedRoomTypes),
                Levels = NullIfEmpty(SelectedLevels),
                Times = ConvertHours(SelectedTimes)
            };
        }

        private void Populate()
        {
            // Get bookings based on filter
            AvailableBookings = _bookingService
                .GetFilteredBookings(BookingFilter)
                .ToList();

            // Filter options for this user
            var filterData = _bookingService
                .GetFilterOptionsForUserAsync(CurrentUserId)
                .Result;

            DepartmentOptions = filterData.Departments
                .Select(d => new SelectListItem { Value = d.Key, Text = d.Value })
                .ToList();

            BuildingOptions = filterData.Buildings
                .Select(b => new SelectListItem { Value = b.Key, Text = b.Value })
                .ToList();

            LevelOptions = filterData.LevelOptions
                .Select(l => new SelectListItem { Value = l.Key, Text = l.Value })
                .ToList();

            RoomTypeOptions = filterData.RoomTypes
                .Select(rt => new SelectListItem { Value = rt.Key, Text = rt.Value })
                .ToList();

            TimeOptions = filterData.TimeSlots
                .Select(t => new SelectListItem { Value = t.Key, Text = t.Value })
                .ToList();
        }

        private static List<T>? NullIfEmpty<T>(List<T> list) =>
            list.Count > 0 ? list : null;

        private static List<TimeOnly>? ConvertHours(List<int> hours) =>
            hours.Count > 0
                ? hours.Select(h => new TimeOnly(h, 0)).ToList()
                : null;
    }
}
