﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Bell.Reconciliation.Web.Server.Models;

public partial class BellIntegrationDevContext : DbContext
{
    public BellIntegrationDevContext()
    {
    }

    public BellIntegrationDevContext(DbContextOptions<BellIntegrationDevContext> options)
        : base(options)
    {
    }

    public virtual DbSet<BellUploadHistory> BellUploadHistories { get; set; }

    public virtual DbSet<BellUser> BellUsers { get; set; }

    public virtual DbSet<OrderOperation> OrderOperations { get; set; }

    public virtual DbSet<OrderPhoneImei> OrderPhoneImeis { get; set; }

    public virtual DbSet<OrderSnapshot> OrderSnapshots { get; set; }

    public virtual DbSet<ReconBell> ReconBells { get; set; }

    public virtual DbSet<ReconBellUpload> ReconBellUploads { get; set; }

    public virtual DbSet<ReconBellUploadHistory> ReconBellUploadHistories { get; set; }

    public virtual DbSet<ReconErrorLog> ReconErrorLogs { get; set; }

    public virtual DbSet<ReconStaple> ReconStaples { get; set; }

    public virtual DbSet<RefDealer> RefDealers { get; set; }

    public virtual DbSet<RefJobCode> RefJobCodes { get; set; }

    public virtual DbSet<RefStatusType> RefStatusTypes { get; set; }

    public virtual DbSet<RefStore> RefStores { get; set; }

    public virtual DbSet<ReqResLog> ReqResLogs { get; set; }

    public virtual DbSet<SampleName> SampleNames { get; set; }

    public virtual DbSet<VDataToBell> VDataToBells { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=efi-stg-database-smi.77e4410e91a5.database.windows.net;Initial Catalog=BELL_INTEGRATION_DEV;Persist Security Info=True;User ID=log_util_bell;Password=test;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BellUploadHistory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_BellUploadRecord");

            entity.ToTable("BellUploadHistory", "_data");

            entity.Property(e => e.FileName).HasMaxLength(50);
            entity.Property(e => e.UploadBy).HasMaxLength(50);
            entity.Property(e => e.UploadDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<BellUser>(entity =>
        {
            entity.HasKey(e => e.EmployeeId).HasName("PK_data.BellUser");

            entity.ToTable("BellUser", "_data");

            entity.Property(e => e.EmployeeId)
                .ValueGeneratedNever()
                .HasColumnName("EmployeeID");
            entity.Property(e => e.Created).HasColumnType("smalldatetime");
            entity.Property(e => e.CreatedBy).HasMaxLength(20);
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.LastName).HasMaxLength(50);
            entity.Property(e => e.PhoneNumber).HasMaxLength(50);
            entity.Property(e => e.Updated).HasColumnType("smalldatetime");
            entity.Property(e => e.UpdatedBy).HasMaxLength(20);
            entity.Property(e => e.UserId)
                .HasMaxLength(5)
                .HasColumnName("UserID");
        });

        modelBuilder.Entity<OrderOperation>(entity =>
        {
            entity.ToTable("OrderOperation", "_data", tb => tb.HasTrigger("t_OrderOperation_DateTime"));

            entity.HasIndex(e => new { e.MasterId, e.Version }, "IDX_OrderOperation_MasterId_Version");

            entity.HasIndex(e => e.StapleTransactionId, "IDX_OrderOperation_StapleId");

            entity.HasIndex(e => new { e.MasterId, e.Version }, "UC_OrderOperation_BellTxId_Ver").IsUnique();

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Action).HasMaxLength(64);
            entity.Property(e => e.ActivityId)
                .HasMaxLength(128)
                .IsUnicode(false);
            entity.Property(e => e.MasterId)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.OriginalTransactionId)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.StapleTransactionId)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Status).HasMaxLength(10);
            entity.Property(e => e.TransactionType).HasMaxLength(32);
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(30)
                .IsUnicode(false);
        });

        modelBuilder.Entity<OrderPhoneImei>(entity =>
        {
            entity.ToTable("OrderPhoneImei", "_data");

            entity.HasIndex(e => e.MasterId, "IDX_OrderPhoneImei_MasterId");

            entity.HasIndex(e => e.Value, "IDX_OrderPhoneImei_Value");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.MasterId).HasMaxLength(20);
            entity.Property(e => e.Notes).HasMaxLength(256);
            entity.Property(e => e.Type).HasMaxLength(32);
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Value).HasMaxLength(64);
        });

        modelBuilder.Entity<OrderSnapshot>(entity =>
        {
            entity.ToTable("OrderSnapshot", "_data", tb => tb.HasTrigger("t_OrderSnapshot_DateTime"));

            entity.HasIndex(e => new { e.MasterId, e.Version }, "IDX_OrderSnapshot_MasterId_Version");

            entity.HasIndex(e => e.StapleTransactionId, "IDX_OrderSnapshot_StapleId");

            entity.HasIndex(e => new { e.MasterId, e.Version }, "UC_OrderSnapshot_BellTxId_Ver").IsUnique();

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.ActivityId)
                .HasMaxLength(128)
                .IsUnicode(false);
            entity.Property(e => e.MasterId)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.OriginalTransactionId)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.StapleTransactionId)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Status).HasMaxLength(10);
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(30)
                .IsUnicode(false);
        });

        modelBuilder.Entity<ReconBell>(entity =>
        {
            entity.ToTable("ReconBell", "_data");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Amount).HasColumnType("money");
            entity.Property(e => e.Brand).HasMaxLength(30);
            entity.Property(e => e.CreatedDate).HasColumnType("smalldatetime");
            entity.Property(e => e.CustomerAccount).HasMaxLength(25);
            entity.Property(e => e.CustomerName).HasMaxLength(100);
            entity.Property(e => e.DealerCode)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Imei)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("IMEI");
            entity.Property(e => e.Lob)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("LOB");
            entity.Property(e => e.OrderNumber)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Product).HasMaxLength(250);
            entity.Property(e => e.RebateType)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.ReconciledBy).HasMaxLength(100);
            entity.Property(e => e.ReconciledDate).HasColumnType("smalldatetime");
            entity.Property(e => e.Simserial)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("SIMSerial");
            entity.Property(e => e.StoreNumber).HasMaxLength(10);
            entity.Property(e => e.SubLob)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.TransactionDate).HasColumnType("smalldatetime");
            entity.Property(e => e.UpdatedBy).HasMaxLength(100);
            entity.Property(e => e.UpdatedDate).HasColumnType("smalldatetime");
        });

        modelBuilder.Entity<ReconBellUpload>(entity =>
        {
            entity.ToTable("ReconBellUpload", "_data");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Amount).HasColumnType("money");
            entity.Property(e => e.Brand).HasMaxLength(30);
            entity.Property(e => e.CreatedDate).HasColumnType("smalldatetime");
            entity.Property(e => e.CustomerAccount).HasMaxLength(25);
            entity.Property(e => e.CustomerName).HasMaxLength(100);
            entity.Property(e => e.DealerCode)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Imei)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("IMEI");
            entity.Property(e => e.Lob)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("LOB");
            entity.Property(e => e.OrderNumber)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Product).HasMaxLength(250);
            entity.Property(e => e.RebateType)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.ReconciledBy).HasMaxLength(100);
            entity.Property(e => e.ReconciledDate).HasColumnType("smalldatetime");
            entity.Property(e => e.Simserial)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("SIMSerial");
            entity.Property(e => e.StoreNumber).HasMaxLength(10);
            entity.Property(e => e.SubLob)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.TransactionDate).HasColumnType("smalldatetime");
            entity.Property(e => e.UpdatedBy).HasMaxLength(100);
            entity.Property(e => e.UpdatedDate).HasColumnType("smalldatetime");
        });

        modelBuilder.Entity<ReconBellUploadHistory>(entity =>
        {
            entity.ToTable("ReconBellUploadHistory", "_data");

            entity.Property(e => e.FileName).HasMaxLength(50);
            entity.Property(e => e.UploadedBy).HasMaxLength(50);
            entity.Property(e => e.UploadedDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<ReconErrorLog>(entity =>
        {
            entity.ToTable("ReconErrorLogs", "_data");

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.EventName).HasMaxLength(500);
            entity.Property(e => e.LogLevel).HasMaxLength(50);
            entity.Property(e => e.Source).HasMaxLength(500);
            entity.Property(e => e.UploadedDate).HasColumnType("datetime");
            entity.Property(e => e.UserId).HasMaxLength(50);
        });

        modelBuilder.Entity<ReconStaple>(entity =>
        {
            entity.ToTable("ReconStaples", "_data");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Amount).HasColumnType("money");
            entity.Property(e => e.BellTransactionId).HasMaxLength(15);
            entity.Property(e => e.Brand).HasMaxLength(30);
            entity.Property(e => e.CreatedDate).HasColumnType("smalldatetime");
            entity.Property(e => e.CustomerAccount).HasMaxLength(25);
            entity.Property(e => e.CustomerName).HasMaxLength(100);
            entity.Property(e => e.DealerCode)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Imei)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("IMEI");
            entity.Property(e => e.Lob)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("LOB");
            entity.Property(e => e.Location).HasMaxLength(250);
            entity.Property(e => e.Msf)
                .HasColumnType("money")
                .HasColumnName("MSF");
            entity.Property(e => e.OrderNumber)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Product).HasMaxLength(250);
            entity.Property(e => e.RebateType)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.ReconciledBy).HasMaxLength(100);
            entity.Property(e => e.ReconciledDate).HasColumnType("smalldatetime");
            entity.Property(e => e.SalesPerson).HasMaxLength(100);
            entity.Property(e => e.Simserial)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("SIMSerial");
            entity.Property(e => e.StoreNumber).HasMaxLength(10);
            entity.Property(e => e.SubLob)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.TaxCode)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.TransactionDate).HasColumnType("smalldatetime");
            entity.Property(e => e.UpdatedBy).HasMaxLength(100);
            entity.Property(e => e.UpdatedDate).HasColumnType("smalldatetime");
        });

        modelBuilder.Entity<RefDealer>(entity =>
        {
            entity.HasKey(e => e.DealerCode).HasName("PK_Dealer");

            entity.ToTable("REF_Dealer", "_data");

            entity.Property(e => e.DealerCode).HasMaxLength(5);
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.StatusTypeId).HasColumnName("StatusTypeID");
            entity.Property(e => e.StoreId).HasColumnName("StoreID");

            entity.HasOne(d => d.StatusType).WithMany(p => p.RefDealers)
                .HasForeignKey(d => d.StatusTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Dealer_StatusType");

            entity.HasOne(d => d.Store).WithMany(p => p.RefDealers)
                .HasForeignKey(d => d.StoreId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Dealer_Store");
        });

        modelBuilder.Entity<RefJobCode>(entity =>
        {
            entity.HasKey(e => e.JobCode).HasName("PK_JobCode");

            entity.ToTable("REF_JobCode", "_data");

            entity.Property(e => e.JobCode).HasMaxLength(20);
            entity.Property(e => e.Role).HasMaxLength(50);
        });

        modelBuilder.Entity<RefStatusType>(entity =>
        {
            entity.ToTable("REF_StatusType", "_data");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.Name).HasMaxLength(10);
        });

        modelBuilder.Entity<RefStore>(entity =>
        {
            entity.HasKey(e => e.StoreId).HasName("PK_Store");

            entity.ToTable("REF_Store", "_data");

            entity.Property(e => e.StoreId)
                .ValueGeneratedNever()
                .HasColumnName("StoreID");
            entity.Property(e => e.Address).HasMaxLength(255);
            entity.Property(e => e.City).HasMaxLength(50);
            entity.Property(e => e.District).HasMaxLength(100);
            entity.Property(e => e.Location).HasMaxLength(255);
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.PostalCode).HasMaxLength(6);
            entity.Property(e => e.ProvinceCode)
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Region).HasMaxLength(100);
            entity.Property(e => e.StatusTypeId).HasColumnName("StatusTypeID");
            entity.Property(e => e.Suite).HasMaxLength(20);

            entity.HasOne(d => d.StatusType).WithMany(p => p.RefStores)
                .HasForeignKey(d => d.StatusTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Store_StatusType");
        });

        modelBuilder.Entity<ReqResLog>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("ReqResLogs", "_data");

            entity.HasIndex(e => e.ActivityId, "IDX_ReqResLogs_ActId");

            entity.HasIndex(e => e.ActivityId, "IDX_ReqResLogs_TraceId");

            entity.Property(e => e.ActivityId)
                .HasMaxLength(128)
                .IsUnicode(false);
            entity.Property(e => e.Direction)
                .HasMaxLength(32)
                .IsUnicode(false);
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("ID");
            entity.Property(e => e.Method)
                .HasMaxLength(32)
                .IsUnicode(false);
            entity.Property(e => e.RequestId)
                .HasMaxLength(128)
                .IsUnicode(false);
            entity.Property(e => e.Timestamp).HasColumnType("datetime");
            entity.Property(e => e.Type)
                .HasMaxLength(128)
                .IsUnicode(false);
        });

        modelBuilder.Entity<SampleName>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("sampleNames", "_data");

            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("address");
            entity.Property(e => e.City)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("city");
            entity.Property(e => e.CompanyName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("company_name");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.FirstName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("first_name");
            entity.Property(e => e.LastName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("last_name");
            entity.Property(e => e.Phone1)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("phone1");
            entity.Property(e => e.Phone2)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("phone2");
            entity.Property(e => e.Postal)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("postal");
            entity.Property(e => e.Province)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("province");
            entity.Property(e => e.Web)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("web");
        });

        modelBuilder.Entity<VDataToBell>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("v_DataToBell", "_data");

            entity.Property(e => e.Action)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.DealerCode).HasMaxLength(5);
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.EmailPwd)
                .HasMaxLength(1)
                .IsUnicode(false);
            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            entity.Property(e => e.FirstName).HasMaxLength(15);
            entity.Property(e => e.LastName).HasMaxLength(15);
            entity.Property(e => e.PhoneNumber).HasMaxLength(50);
            entity.Property(e => e.Role).HasMaxLength(50);
            entity.Property(e => e.SubAgent)
                .HasMaxLength(1)
                .IsUnicode(false);
            entity.Property(e => e.UserProfile)
                .HasMaxLength(1)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
