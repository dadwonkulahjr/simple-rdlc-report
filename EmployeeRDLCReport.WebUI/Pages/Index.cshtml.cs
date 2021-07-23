using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace EmployeeRDLCReport.WebUI.Pages
{
    public class IndexModel : PageModel
    {
       public IActionResult OnGet() { return RedirectToPage("./Admin/Employee/Report/Index"); }
    }
}
