using Event.Business.Category;
using Event.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Event.RazorWebApp.Pages
{
    public class LoginModel : PageModel
    {   
        public CustomerBussiness _customerBussiness { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string ErrorMessage { get; set; } = string.Empty;
        public LoginModel() 
        { 
        _customerBussiness ??= new CustomerBussiness();
        }
        public void OnGet()
        {
        
        }
        public async Task<IActionResult> OnPost(string Email, string Password)
        {
            if (!ModelState.IsValid)
            {
                ErrorMessage = "Please fill in all fields.";
                return Page();
            }
            

            var customers = (await _customerBussiness.GetAll()).Data as List<Customer>;
            var customer = customers?.FirstOrDefault(x => x.Email.ToLower().Equals(Email.ToLower().Trim()) && x.Password.Equals(Password));
            if(customer == null)
            {
                ErrorMessage = "Email/Password is wrong.";
                return Page();
            }
            ErrorMessage = "";
            HttpContext.Session.SetString("user", customer.CustomerId.ToString());
            return RedirectToPage("/Index");
        }
    }
}
