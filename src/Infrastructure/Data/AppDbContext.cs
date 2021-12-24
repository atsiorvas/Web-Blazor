using BlazorApp.ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace BlazorApp.Infrastructure.Data {

    public class AppDbContext : DbContext {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Customer> Customers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder) {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}