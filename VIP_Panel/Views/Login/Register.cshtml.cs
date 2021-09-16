using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using VIP_Panel.Data;
using VIP_Panel.Models;

namespace VIP_Panel.Views.Login
{
    public class CreateModel : PageModel
    {
        private readonly VIP_Panel.Data.ApplicationDbContext _context;

        public CreateModel(VIP_Panel.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public VipUserModel VipUserModel { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.vipUsers.Add(VipUserModel);
            await _context.SaveChangesAsync();
            Console.WriteLine("######### ŘŘ DZIALAAA $$$$$$$$$$$");

            return RedirectToPage("./Index");
        }
    }
}
