using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Entrytask_Urban.Models;

namespace Entrytask_Urban.Pages
{
    
    public class errorModel : PageModel
    {

        public void OnGet()
        {
            /*bool authorized = User.Claims.Any(c => c.Type == "authorized" && c.Value == "true");
            if (!authorized)
            {
                return RedirectToPage("/mylogin");
            }

            // Your other logic for the error page goes here

            return Page();*/
        }

    }
}
