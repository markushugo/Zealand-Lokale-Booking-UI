using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Zealand_Lokale_Booking_Library.Services;

namespace Zealand_Lokale_Booking_UI.Pages
{
    public class LoginPageModel : PageModel
    {
        private readonly IUserService _userService;

        public LoginPageModel(IUserService userService)
        {
            _userService = userService;
        }

        [BindProperty]
        public string LoginEmail { get; set; }

        [BindProperty]
        public string LoginPassword { get; set; }

        public string ErrorMessage { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            // Debug output
            Console.WriteLine($"Trying to login with: {LoginEmail}");

            var user = _userService.ValidateLogin(LoginEmail, LoginPassword);

            if (user == null)
            {
                Console.WriteLine("Login failed - user not found");
                ErrorMessage = "Forkert email eller password.";
                return Page();
            }

            Console.WriteLine($"Login success for: {user.Name}");

            HttpContext.Session.SetInt32("UserID", user.UserID);
            HttpContext.Session.SetString("UserName", user.Name);

            return RedirectToPage("/Home");
        }
    }
}
