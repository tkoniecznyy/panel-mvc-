using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using VIP_Panel.Data;
using VIP_Panel.Models;

namespace VIP_Panel.Views.Login
{
    public class IndexModel : PageModel
    {
        private readonly VIP_Panel.Data.ApplicationDbContext _context;

        public IndexModel(VIP_Panel.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<VipUserModel> VipUserModel { get;set; }

        public async Task OnGetAsync()
        {
            VipUserModel = await _context.vipUsers.ToListAsync();
        }
    }
}
