using Microsoft.EntityFrameworkCore;
using ServiceApplication;
using System;

namespace ServiceB
{
    public class ApplicationContext : DbContext
    {
        private string connectionString;

        public DbSet<subdivision> subdivisions { get; set; }
        public ApplicationContext()
        {
            connectionString = StatusService.connectionString;
            StatusService.EnsureCreated(connectionString);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(connectionString);
        }
    }
}
