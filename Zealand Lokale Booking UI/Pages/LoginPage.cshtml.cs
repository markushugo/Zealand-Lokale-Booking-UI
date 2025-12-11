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
            var user = _userService.ValidateLogin(LoginEmail, LoginPassword);

            if (user == null)
            {
                ErrorMessage = "Forkert email eller password.";
                return Page();
            }

            // Session set
            HttpContext.Session.SetInt32("UserID", user.UserID);
            HttpContext.Session.SetString("UserName", user.Name);

            return RedirectToPage("/Index");
        }
    }
}
