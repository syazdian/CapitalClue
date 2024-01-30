using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CapitalClue.Web.Server.Data.sqlite;

public partial class BellRecContext : DbContext
{
    public BellRecContext()
    {
    }

    public BellRecContext(DbContextOptions<BellRecContext> options)
        : base(options)
    {
    }

    public virtual DbSet<BellSource> BellSources { get; set; }

    public virtual DbSet<SampleName> SampleNames { get; set; }

    public virtual DbSet<StaplesSource> StaplesSources { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlite("Data Source=data\\sqlite\\BellRec.sqlite; providerName=");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BellSource>(entity =>
        {
            entity.ToTable("BellSource");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Imei).HasColumnName("IMEI");
            entity.Property(e => e.Lob).HasColumnName("LOB");
        });

        modelBuilder.Entity<SampleName>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("sampleNames");

            entity.Property(e => e.Address)
                .HasColumnType("TEXT(255)")
                .HasColumnName("address");
            entity.Property(e => e.City)
                .HasColumnType("TEXT(255)")
                .HasColumnName("city");
            entity.Property(e => e.CompanyName)
                .HasColumnType("TEXT(255)")
                .HasColumnName("company_name");
            entity.Property(e => e.Email)
                .HasColumnType("TEXT(255)")
                .HasColumnName("email");
            entity.Property(e => e.FirstName)
                .HasColumnType("TEXT(255)")
                .HasColumnName("first_name");
            entity.Property(e => e.LastName)
                .HasColumnType("TEXT(255)")
                .HasColumnName("last_name");
            entity.Property(e => e.Phone1)
                .HasColumnType("TEXT(255)")
                .HasColumnName("phone1");
            entity.Property(e => e.Phone2)
                .HasColumnType("TEXT(255)")
                .HasColumnName("phone2");
            entity.Property(e => e.Postal)
                .HasColumnType("TEXT(255)")
                .HasColumnName("postal");
            entity.Property(e => e.Province)
                .HasColumnType("TEXT(255)")
                .HasColumnName("province");
            entity.Property(e => e.Web)
                .HasColumnType("TEXT(255)")
                .HasColumnName("web");
        });

        modelBuilder.Entity<StaplesSource>(entity =>
        {
            entity.ToTable("StaplesSource");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Imei).HasColumnName("IMEI");
            entity.Property(e => e.Lob).HasColumnName("LOB");
            entity.Property(e => e.Msf).HasColumnName("MSF");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
