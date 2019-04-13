using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GiftWizItApi.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {

        }

        public DbSet<GiftLists> GiftLists { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GiftLists>().Property(gl => gl.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
            modelBuilder.Entity<GiftLists>().Property(gl => gl.CreatedAt).ValueGeneratedOnAdd();
        }
    }
}
