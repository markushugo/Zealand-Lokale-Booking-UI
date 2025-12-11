using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Zealand_Lokale_Booking_UI.Pages
{
    public class HomeModel : PageModel
    {

        private readonly ILogger<HomeModel> _logger;
        [BindProperty]
        string UserName
        {
            get; set;
        }

        public HomeModel(ILogger<HomeModel> logger)
        {
            _logger = logger;
        }
        public IActionResult OnGet()
        {
            // Tjek om sessionen er sat
            int? userId = HttpContext.Session.GetInt32("UserID");
            string userName = HttpContext.Session.GetString("UserName");

            if (userId == null)
            {
                // Hvis ikke logget ind, redirect til login
                return RedirectToPage("/LoginPage");
            }

            // Hvis logget ind, gem brugernavn til view
            UserName = userName;
            return Page();
        }

    }
}
