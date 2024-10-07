using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CRUDWebEf.Models
{
    public partial class bd_pruebasdContext : DbContext
    {
        public bd_pruebasdContext()
        {
        }

        public bd_pruebasdContext(DbContextOptions<bd_pruebasdContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TbCategoria> TbCategorias { get; set; } = null!;
        public virtual DbSet<TbMedida> TbMedidas { get; set; } = null!;
        public virtual DbSet<TbProducto> TbProductos { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TbCategoria>(entity =>
            {
                entity.HasKey(e => e.IdCategoria);

                entity.ToTable("tb_categorias");

                entity.Property(e => e.IdCategoria).HasColumnName("id_categoria");

                entity.Property(e => e.ActivoCa).HasColumnName("activo_ca");

                entity.Property(e => e.DescripcionCa)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("descripcion_ca");
            });

            modelBuilder.Entity<TbMedida>(entity =>
            {
                entity.HasKey(e => e.IdMedida);

                entity.ToTable("tb_medidas");

                entity.Property(e => e.IdMedida).HasColumnName("id_medida");

                entity.Property(e => e.ActivoMe).HasColumnName("activo_me");

                entity.Property(e => e.DescripcionMe)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("descripcion_me");
            });

            modelBuilder.Entity<TbProducto>(entity =>
            {
                entity.HasKey(e => e.IdProducto);

                entity.ToTable("tb_productos");

                entity.Property(e => e.IdProducto).HasColumnName("id_producto");

                entity.Property(e => e.ActivoPr).HasColumnName("activo_pr");

                entity.Property(e => e.DescripcionPr)
                    .HasMaxLength(80)
                    .IsUnicode(false)
                    .HasColumnName("descripcion_pr");

                entity.Property(e => e.FechaCreacion)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_creacion");

                entity.Property(e => e.IdCategoria).HasColumnName("id_categoria");

                entity.Property(e => e.IdMedida).HasColumnName("id_medida");

                entity.Property(e => e.Marca)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("marca");

                entity.Property(e => e.Precio)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("precio");

                entity.Property(e => e.Stock)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("stock");

                entity.HasOne(d => d.oCategoria)
                    .WithMany(p => p.TbProductos)
                    .HasForeignKey(d => d.IdCategoria)
                    .HasConstraintName("FK_tb_productos_tb_categorias");

                entity.HasOne(d => d.oMedida)
                    .WithMany(p => p.TbProductos)
                    .HasForeignKey(d => d.IdMedida)
                    .HasConstraintName("FK_tb_productos_tb_medidas");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
