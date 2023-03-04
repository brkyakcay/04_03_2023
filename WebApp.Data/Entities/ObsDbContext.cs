using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Data.Entities;

public partial class ObsDbContext : DbContext
{
    public ObsDbContext()
    {
    }

    public ObsDbContext(DbContextOptions<ObsDbContext> options)
        : base(options)
    {
    }

    //public virtual DbSet<AspNetRole> AspNetRoles { get; set; }

    //public virtual DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; }

    //public virtual DbSet<AspNetUser> AspNetUsers { get; set; }

    //public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }

    //public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }

    //public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; }

    public virtual DbSet<Bolumler> Bolumlers { get; set; }

    public virtual DbSet<Dersler> Derslers { get; set; }

    public virtual DbSet<Ogrenciler> Ogrencilers { get; set; }

    public virtual DbSet<Ogretmenler> Ogretmenlers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=OBS8;Integrated Security=True;TrustServerCertificate=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        /*
        //modelBuilder.Entity<AspNetRole>(entity =>
        //{
        //    entity.HasIndex(e => e.NormalizedName, "RoleNameIndex")
        //        .IsUnique()
        //        .HasFilter("([NormalizedName] IS NOT NULL)");

        //    entity.Property(e => e.Name).HasMaxLength(256);
        //    entity.Property(e => e.NormalizedName).HasMaxLength(256);
        //});

        //modelBuilder.Entity<AspNetRoleClaim>(entity =>
        //{
        //    entity.HasIndex(e => e.RoleId, "IX_AspNetRoleClaims_RoleId");

        //    entity.HasOne(d => d.Role).WithMany(p => p.AspNetRoleClaims).HasForeignKey(d => d.RoleId);
        //});

        //modelBuilder.Entity<AspNetUser>(entity =>
        //{
        //    entity.HasIndex(e => e.NormalizedEmail, "EmailIndex");

        //    entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex")
        //        .IsUnique()
        //        .HasFilter("([NormalizedUserName] IS NOT NULL)");

        //    entity.Property(e => e.Email).HasMaxLength(256);
        //    entity.Property(e => e.NormalizedEmail).HasMaxLength(256);
        //    entity.Property(e => e.NormalizedUserName).HasMaxLength(256);
        //    entity.Property(e => e.UserName).HasMaxLength(256);

        //    entity.HasMany(d => d.Roles).WithMany(p => p.Users)
        //        .UsingEntity<Dictionary<string, object>>(
        //            "AspNetUserRole",
        //            r => r.HasOne<AspNetRole>().WithMany().HasForeignKey("RoleId"),
        //            l => l.HasOne<AspNetUser>().WithMany().HasForeignKey("UserId"),
        //            j =>
        //            {
        //                j.HasKey("UserId", "RoleId");
        //                j.ToTable("AspNetUserRoles");
        //                j.HasIndex(new[] { "RoleId" }, "IX_AspNetUserRoles_RoleId");
        //            });
        //});

        //modelBuilder.Entity<AspNetUserClaim>(entity =>
        //{
        //    entity.HasIndex(e => e.UserId, "IX_AspNetUserClaims_UserId");

        //    entity.HasOne(d => d.User).WithMany(p => p.AspNetUserClaims).HasForeignKey(d => d.UserId);
        //});

        //modelBuilder.Entity<AspNetUserLogin>(entity =>
        //{
        //    entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

        //    entity.HasIndex(e => e.UserId, "IX_AspNetUserLogins_UserId");

        //    entity.HasOne(d => d.User).WithMany(p => p.AspNetUserLogins).HasForeignKey(d => d.UserId);
        //});

        //modelBuilder.Entity<AspNetUserToken>(entity =>
        //{
        //    entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

        //    entity.HasOne(d => d.User).WithMany(p => p.AspNetUserTokens).HasForeignKey(d => d.UserId);
        //});
        */

        modelBuilder.Entity<Bolumler>(entity =>
        {
            entity.ToTable("Bolumler");

            entity.HasIndex(e => e.OgretmenId, "IX_Bolumler_OgretmenId").IsUnique();

            entity.HasOne(d => d.Ogretmen).WithOne(p => p.Bolumler).HasForeignKey<Bolumler>(d => d.OgretmenId);
        });

        modelBuilder.Entity<Dersler>(entity =>
        {
            entity.ToTable("Dersler");

            entity.HasIndex(e => e.Adi, "IX_Dersler_Adi")
                .IsUnique()
                .HasFilter("([Adi] IS NOT NULL)");

            entity.Property(e => e.Adi)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasMany(d => d.Ogrencilers).WithMany(p => p.Derslers)
                .UsingEntity<Dictionary<string, object>>(
                    "DersOgrenci",
                    r => r.HasOne<Ogrenciler>().WithMany().HasForeignKey("OgrencilerId"),
                    l => l.HasOne<Dersler>().WithMany().HasForeignKey("DerslerId"),
                    j =>
                    {
                        j.HasKey("DerslerId", "OgrencilerId");
                        j.ToTable("DersOgrenci");
                        j.HasIndex(new[] { "OgrencilerId" }, "IX_DersOgrenci_OgrencilerId");
                    });

            entity.HasMany(d => d.Ogretmenlers).WithMany(p => p.Derslers)
                .UsingEntity<Dictionary<string, object>>(
                    "DersOgretman",
                    r => r.HasOne<Ogretmenler>().WithMany().HasForeignKey("OgretmenlerId"),
                    l => l.HasOne<Dersler>().WithMany().HasForeignKey("DerslerId"),
                    j =>
                    {
                        j.HasKey("DerslerId", "OgretmenlerId");
                        j.ToTable("DersOgretmen");
                        j.HasIndex(new[] { "OgretmenlerId" }, "IX_DersOgretmen_OgretmenlerId");
                    });
        });

        modelBuilder.Entity<Ogrenciler>(entity =>
        {
            entity.ToTable("Ogrenciler");

            entity.HasIndex(e => new { e.Adi, e.Soyadi }, "IX_Ogrenciler_Adi_Soyadi")
                .IsUnique()
                .HasFilter("([Adi] IS NOT NULL AND [Soyadi] IS NOT NULL)");

            entity.Property(e => e.Adi)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Eposta)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("EPosta");
            entity.Property(e => e.GobekAdi)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Soyadi)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Ogretmenler>(entity =>
        {
            entity.ToTable("Ogretmenler");

            entity.Property(e => e.Adi)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Eposta)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("EPosta");
            entity.Property(e => e.GobekAdi)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Soyadi)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
