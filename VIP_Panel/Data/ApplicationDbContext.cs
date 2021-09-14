using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using VIP_Panel.Models;

namespace VIP_Panel.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<PostModel> posts { get; set; }
        public DbSet<VIP_Panel.Models.VipUserModel> vipUsers { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlServer(
                    @"Server=tcp:lact-test.database.windows.net,1433;Database=HomeUs_testDb;User ID=lact_tk;Password=21.testDb;Trusted_Connection=False;Encrypt=True;",
                    providerOptions => { providerOptions.EnableRetryOnFailure(); });
        }

        
    }
}

