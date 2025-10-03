using AuditTrail1.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace AuditTrail1.Data
{
    public class AuditTrailContext : DbContext
    {
        public AuditTrailContext(DbContextOptions<AuditTrailContext> options)
            : base(options) { }

        public DbSet<AuditTrail> AuditTrails { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AuditTrail>().ToTable("AuditTrail");
        }
    }
}



