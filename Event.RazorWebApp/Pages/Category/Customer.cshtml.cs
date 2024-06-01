using Event.Business.Category;
using Event.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Event.RazorWebApp.Pages.Category
{
    public class CustomerModel : PageModel
    {
        private readonly ICustomerBussiness _customer;

        public CustomerModel(ICustomerBussiness customerBussiness)
        {
            _customer = customerBussiness;
        }

        [BindProperty] 
        public List<Customer> Customers { get; set; }
        [BindProperty]
        public Customer customers { get; set; } = new Customer();

        public async Task OnGetAsync()
        {
            var result = await _customer.GetAll();
            if(result != null && result.Data != null)
            {
                Customers = (List<Customer>)result.Data;
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _customer.GetAll();

            return RedirectToPage("./Customer Page");
        }

        public async Task<IActionResult> OnPostCreateAsync()
        {
            var newCustomer = new Customer
            {
                CustomerName = customers.CustomerName,
                Email = customers.Email,
                Address = customers.Address,
                Password = customers.Password,
                Phone = customers.Phone

            };
            await _customer.Save(newCustomer);
            return RedirectToPage("Customer");
        }

        public async Task<IActionResult> OnPostRemoveAsync(int id)
        {
            await _customer.DeleteById(id);
            return RedirectToPage("Customer");
        }

        public async Task<IActionResult> OnPostUpdateAsync()
        {
            var newCustomer = new Customer
            {
                CustomerId = customers.CustomerId,
                CustomerName = customers.CustomerName,
                Email = customers.Email,
                Address = customers.Address,
                Password = customers.Password,
                Phone = customers.Phone

            };
            await _customer.Update(newCustomer);
            return RedirectToPage("Customer");
        }

    }
}
