using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Zealand_Lokale_Booking_UI.Pages
{
    public class LogoutModel : PageModel
    {
        public IActionResult OnGet()
        {
            // Clear all session data
            HttpContext.Session.Clear();

            // Delete the session cookie so the session truly resets
            HttpContext.Response.Cookies.Delete(".AspNetCore.Session");

            // Redirect back to login page
            return RedirectToPage("/LoginPage");
        }
    }
}

