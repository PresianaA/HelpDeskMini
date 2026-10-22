using HelpDeskMini.Models;

using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace HelpDeskMini.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<HelpRequest> HelpRequests => Set<HelpRequest>();
    }
}
