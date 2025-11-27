using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

public class TestPageModel : PageModel
{
    private readonly ITestService _testService;

    public TestPageModel(ITestService testService)
    {
        _testService = testService;
    }

    [BindProperty]
    public string InputValue { get; set; }

    public string Message { get; set; }

    public void OnGet()
    {
        Message = "Enter a value to insert into the database.";
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!string.IsNullOrWhiteSpace(InputValue))
        {
            await _testService.AddTestValueAsync(InputValue);
            Message = $"Value '{InputValue}' was inserted successfully!";
            InputValue = string.Empty; // clear input
        }
        else
        {
            Message = "Please enter a value.";
        }

        return Page();
    }
}
