using contas.api.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace contas.api.Infrastructure.Context
{
    public class ContasContext : DbContext
    {
        public ContasContext(DbContextOptions options) : base(options)
        { }

        public DbSet<Contas> Contas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}