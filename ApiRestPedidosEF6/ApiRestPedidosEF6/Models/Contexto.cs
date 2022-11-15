#nullable disable
using Microsoft.EntityFrameworkCore;

namespace ApiRestPedidosEF6.Models
{
    public partial class Contexto : DbContext
    {
        public Contexto()
        {
        }

        public Contexto(DbContextOptions<Contexto> options)
            : base(options)
        {
        }

        public virtual DbSet<Cliente> Cliente { get; set; }
        public virtual DbSet<Item> Item { get; set; }
        public virtual DbSet<ItemPedido> ItemPedido { get; set; }
        public virtual DbSet<Pedido> Pedido { get; set; }
        public virtual DbSet<VW_ItensPedido> VW_ItensPedido { get; set; }
        public virtual DbSet<VW_Pedido> VW_Pedido { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.HasIndex(e => new { e.Nome, e.Documento }, "IX_Cliente_NomeDoc")
                    .IsUnique()
                    .HasFillFactor(90);
            });

            modelBuilder.Entity<Item>(entity =>
            {
                entity.Property(e => e.Quantidade).HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<ItemPedido>(entity =>
            {
                entity.Property(e => e.Quantidade).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.IdItemNavigation)
                    .WithMany(p => p.ItemPedido)
                    .HasForeignKey(d => d.IdItem)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ItemPedido_Item");

                entity.HasOne(d => d.IdPedidoNavigation)
                    .WithMany(p => p.ItemPedido)
                    .HasForeignKey(d => d.IdPedido)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ItemPedido_Pedido");
            });

            modelBuilder.Entity<Pedido>(entity =>
            {
                entity.HasIndex(e => e.NumeroPedido, "IX_Pedido_NumeroPedido")
                    .IsUnique()
                    .HasFillFactor(90);

                entity.Property(e => e.DtCriacao).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.IdClienteNavigation)
                    .WithMany(p => p.Pedido)
                    .HasForeignKey(d => d.IdCliente)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Pedido_Cliente");
            });

            modelBuilder.Entity<VW_ItensPedido>(entity =>
            {
                entity.ToView("VW_ItensPedido");
            });

            modelBuilder.Entity<VW_Pedido>(entity =>
            {
                entity.ToView("VW_Pedido");
            });

            OnModelCreatingGeneratedProcedures(modelBuilder);
            OnModelCreatingGeneratedFunctions(modelBuilder);
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}