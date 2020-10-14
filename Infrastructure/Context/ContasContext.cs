using Microsoft.EntityFrameworkCore;

namespace contas.api.Infrastructure.Context
{
    public class ContasContext : DbContext
    {
        public ContasContext(DbContextOptions options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}