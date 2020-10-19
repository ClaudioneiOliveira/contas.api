using contas.api.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace contas.api.Infrastructure.Context
{
    public partial class ContasContext : DbContext
    {
        public ContasContext()
        {
        }

        public ContasContext(DbContextOptions<ContasContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ContasModel> Contas { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=.\\sqlclaudionei;Initial Catalog=contas;Persist Security Info=True;User ID=sa;Password=claudionei");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ContasModel>(entity =>
            {
                entity.HasKey(e => e.CodSequencia);

                entity.ToTable("contas");

                entity.Property(e => e.CodSequencia)
                    .HasColumnType("numeric(18, 0)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.DataPagamento)
                    .HasColumnType("datetime");

                entity.Property(e => e.DataVencimento)
                    .HasColumnType("datetime");

                entity.Property(e => e.DiasAtraso)
                    .HasColumnType("numeric(18, 0)");

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.PercJuros)
                    .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.PercMulta)
                    .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.ValorCorrigido)
                    .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.ValorJuros)
                    .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.ValorMulta)
                    .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.ValorOriginal)
                    .HasColumnType("decimal(18, 2)");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}