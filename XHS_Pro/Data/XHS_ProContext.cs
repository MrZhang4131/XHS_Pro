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
        public DbSet<XHS_Pro.Models.Note> Note { get; set; } = default!;
        public DbSet<XHS_Pro.Models.Comment> Comment { get; set; } = default!;
        public DbSet<XHS_Pro.Models.Start> Start { get; set; } = default!;
        public DbSet<XHS_Pro.Models.Zan> Zan { get; set; } = default!;
        public DbSet<XHS_Pro.Models.Goods> Goods { get; set; } = default!;
        public DbSet<XHS_Pro.Models.Car> Car { get; set; } = default!;
    }
}
