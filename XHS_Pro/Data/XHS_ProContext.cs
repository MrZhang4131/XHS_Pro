using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using XHS_Pro.Models;

namespace XHS_Pro.Data
{
    public class XHS_ProContext : DbContext
    {
        public XHS_ProContext (DbContextOptions<XHS_ProContext> options)
            : base(options)
        {
        }

        public DbSet<XHS_Pro.Models.User> User { get; set; } = default!;
    }
}
