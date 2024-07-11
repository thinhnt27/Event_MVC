using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Event.RazorWebApp.Pages
{
    public class Index1Model : PageModel
    {
        public ActionResult OnGet()
        {
            HttpContext.Session.Remove("user");
            return Redirect("./Index");
        }
    }
}
