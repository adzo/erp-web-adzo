using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Tsi.Erp.FirstApi.Entities;

namespace Tsi.Erp.FirstApi.Data;

public partial class ApplicationContext : DbContext
{
    public ApplicationContext(DbContextOptions<ApplicationContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Delegation> Delegations { get; set; }

    public virtual DbSet<Gouvernorat> Gouvernorats { get; set; }

    public virtual DbSet<Ville> Villes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("French_CI_AS");

        modelBuilder.Entity<Delegation>(entity =>
        {
            entity.HasKey(e => e.Uid).HasName("PK__Delegati__C5B19602929DFAE8");

            entity.HasIndex(e => new { e.Code, e.Gouvernorat }, "IX_UNIQUE_CODE_GOUVERNORAT")
                .IsUnique()
                .HasFilter("([Code] IS NOT NULL AND [Gouvernorat] IS NOT NULL)");

            entity.Property(e => e.Uid).HasDefaultValueSql("(newid())");

            entity.HasOne(d => d.GouvernoratNavigation).WithMany(p => p.Delegations).HasConstraintName("FK_Gouvernorats_Delegation");
        });

        modelBuilder.Entity<Gouvernorat>(entity =>
        {
            entity.HasKey(e => e.Uid).HasName("PK__Gouverno__C5B196029981BDE2");

            entity.HasIndex(e => e.Code, "IX_Gouvernorats")
                .IsUnique()
                .HasFilter("([Code] IS NOT NULL)");

            entity.Property(e => e.Uid).HasDefaultValueSql("(newid())");
        });

        modelBuilder.Entity<Ville>(entity =>
        {
            entity.HasKey(e => e.Uid).HasName("PK__Villes__C5B19602564A1946");

            entity.HasIndex(e => e.Code, "IX_Villes")
                .IsUnique()
                .HasFilter("([Code] IS NOT NULL)");

            entity.Property(e => e.Uid).HasDefaultValueSql("(newid())");

            entity.HasOne(d => d.GouvernoratsNavigation).WithMany(p => p.Villes)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Villes_Gouvern");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
