using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Event.Data.Models;
using Event.Business.Category;
using System.Drawing.Printing;

namespace Event.RazorWebApp.Pages.Category.CustomerPage
{
    public class IndexModel : PageModel
    {
        private readonly ICustomerBussiness _customerBusiness;

        public IndexModel()
        {
            _customerBusiness ??= new CustomerBussiness();
        }
        public PaginatedList<Customer> CustomerNe {  get; set; }

        public string NameSort { get; set; }
        public string MailSort { get; set; }
        public string AddressSort {  get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort {  get; set; }
        public IList<Customer> Customer { get;set; } = default!;
        public int PageSize { get; set; } = 3;
        public string SearchType { get; set; }

        public async Task<ActionResult> OnGetAsync(string sortOrder, string currentFilter, string searchString, int? pageIndex, int? pageSize, string searchType)
        {
            var user = HttpContext.Session.Get("user");
            if (user == null)
            {
                return Redirect("../../../Login");
            }
            if (pageSize.HasValue)
            {
                PageSize = pageSize.Value;
            }

            CurrentSort = sortOrder;
            NameSort = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            MailSort = sortOrder == "Mail" ? "mail_desc" : "Mail";
            AddressSort = sortOrder == "Address" ? "address_desc" : "Address";
            CurrentFilter = searchString;
            SearchType = searchType;

            if(searchString != null)
            {
                pageIndex = 1;
            } 
            else
            {
                searchString = currentFilter;
            }

            var result = _customerBusiness.GetAll();
            var customers = result.Status > 0 && result.Result.Data != null
                ? (List<Customer>)result.Result.Data
                : new List<Customer>();

            if (!String.IsNullOrEmpty(searchString))
            {
                switch (searchType)
                {
                    case "Name":
                        customers = customers.Where(c => c.CustomerName.Contains(searchString)).ToList();
                        break;
                    case "Address":
                        customers = customers.Where(c => c.Address.Contains(searchString)).ToList();
                        break;
                    case "Phone":
                        customers = customers.Where(c => c.Phone.Contains(searchString)).ToList();
                        break;
                    default:
                        customers = customers.Where(c => c.CustomerName.Contains(searchString)).ToList();
                        break;
                }
            }

            switch (sortOrder)
            {
                case "name_desc":
                    customers = customers.OrderByDescending(c => c.CustomerName).ToList();
                    break;
                case "mail_desc":
                    customers = customers.OrderByDescending(c => c.Email).ToList();
                    break;
                case "Mail":
                    customers = customers.OrderBy(c => c.Email).ToList();
                    break;
                case "address_desc":
                    customers = customers.OrderByDescending(c => c.Address).ToList();
                    break;
                case "Address":
                    customers = customers.OrderBy(c => c.Address).ToList();
                    break;
                default:
                    customers = customers.OrderBy(c => c.CustomerId).ToList();
                    break;
            }

            

            CustomerNe = await PaginatedList<Customer>.CreateAsync(customers, pageIndex ?? 1, PageSize);

            return Page();
        }
    }
}
