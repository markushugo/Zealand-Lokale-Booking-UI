using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace Zealand_Lokale_Booking_UI.Pages
{
    public class LoginModel : PageModel
    {
        private readonly ILogger<LoginModel> _logger;
        private readonly UserService _userService;

        public LoginModel(ILogger<LoginModel> logger, UserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        [BindProperty]
        [Required(ErrorMessage = "Email er påkrævet")]
        public string Email { get; set; } = string.Empty;

        [BindProperty]
        [Required(ErrorMessage = "Adgangskode er påkrævet")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        public string ErrorMessage { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Password))
            {
                ErrorMessage = "Indtast venligst både email og adgangskode.";
                return Page();
            }

            if (!_userService.TryAuthenticate(Email, Password, out var user))
            {
                // Authentication failed
                ErrorMessage = "Ugyldig email eller adgangskode.";
                return Page();
            }

            // Login successful – save session
            HttpContext.Session.SetString("UserId", user.UserId.ToString());
            HttpContext.Session.SetString("UserName", user.Name);
            HttpContext.Session.SetString("DepartmentId", user.DepartmentID.ToString());
            HttpContext.Session.SetString("UserTypeId", user.UserTypeID.ToString());

            return RedirectToPage("/Index");
        }
    }
}
