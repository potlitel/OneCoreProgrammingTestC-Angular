using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace OnecoreTestWebApiRest.Models
{
    public partial class OnecoreProgrammingTestContext : DbContext
    {
        //public OnecoreProgrammingTestContext()
        //{
        //}
        /// <summary>
        /// Método SaveChanges
        /// </summary>
        /// <returns></returns>
        public override int SaveChanges()
        {
            HandleClienteDelete();
            return base.SaveChanges();
        }
        /// <summary>
        /// Método HandleClienteDelete
        /// </summary>
        private void HandleClienteDelete()
        {
            //Manejamos el caso para la eliminación de soft delete (eliminado lógico)
            var entities = ChangeTracker.Entries().Where(e => e.State == EntityState.Deleted);
            foreach (var entity in entities)
            {
                if (entity.Entity is Clientes)
                {
                    //entity.State = EntityState.Modified;
                    //var cliente = entity.Entity as Clientes;
                    //cliente.IsDeleted = true;
                    switch (entity.State)
                    {
                        case EntityState.Added:
                            entity.State = EntityState.Modified;
                            entity.CurrentValues["isDeleted"] = false;
                            break;
                        case EntityState.Deleted:
                            entity.State = EntityState.Modified;
                            entity.CurrentValues["isDeleted"] = true;
                            break;
                    }
                }
            }
        }
        /// <summary>
        /// Método constructor OnecoreProgrammingTestContext
        /// </summary>
        /// <param name="options"></param>
        public OnecoreProgrammingTestContext(DbContextOptions<OnecoreProgrammingTestContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Clientes> Clientes { get; set; }
        public virtual DbSet<Compras> Compras { get; set; }
        public virtual DbSet<Documentos> Documentos { get; set; }
        /// <summary>
        /// Método OnConfiguring
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                //                optionsBuilder.UseSqlServer("Server=localhost;Database=OnecoreProgrammingTest;Integrated Security=True");
            }
        }
        /// <summary>
        /// Método OnModelCreating
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Clientes>(entity =>
            {
                entity.Property(e => e.Cp)
                    .IsRequired()
                    .HasColumnName("CP")
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.Direccion)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Rfc)
                    .IsRequired()
                    .HasColumnName("RFC")
                    .HasMaxLength(15)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Compras>(entity =>
            {
                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.PrecioUnitario).HasColumnType("money");

                entity.Property(e => e.Total).HasColumnType("money");

                entity.HasOne(d => d.IdClienteNavigation)
                    .WithMany(p => p.Compras)
                    .HasForeignKey(d => d.IdCliente)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Compras_Cliente");

                entity.HasOne(d => d.IdDocumentoNavigation)
                    .WithMany(p => p.Compras)
                    .HasForeignKey(d => d.IdDocumento)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Compras_Documento");
            });

            modelBuilder.Entity<Documentos>(entity =>
            {
                entity.Property(e => e.DirecFisica)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.EstadoNotifi)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Fecha).HasColumnType("datetime");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);

            //modelBuilder.Entity<Clientes>().Property<bool>("isDeleted");
            //modelBuilder.Entity<Clientes>().HasQueryFilter(m => EF.Property<bool>(m, "isDeleted") == false);
        }
        /// <summary>
        /// Método OnModelCreatingPartial
        /// </summary>
        /// <param name="modelBuilder"></param>
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
