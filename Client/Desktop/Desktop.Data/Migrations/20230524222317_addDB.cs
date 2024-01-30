using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Desktop.Data.Migrations
{
    /// <inheritdoc />
    public partial class addDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BellSources",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                       .Annotation("Sqlite:Autoincrement", true),
                    IsReconciled = table.Column<bool>(type: "INTEGER", nullable: true),

                    TransactionDate = table.Column<string>(type: "TEXT", nullable: true),
                    DealerCode = table.Column<string>(type: "TEXT", nullable: true),
                    Brand = table.Column<string>(type: "TEXT", nullable: true),
                    OrderNumber = table.Column<long>(type: "INTEGER", nullable: true),
                    Phone = table.Column<long>(type: "INTEGER", nullable: true),
                    Imei = table.Column<string>(type: "TEXT", nullable: true),
                    Simserial = table.Column<string>(type: "TEXT", nullable: true),
                    RebateType = table.Column<string>(type: "TEXT", nullable: true),
                    Product = table.Column<string>(type: "TEXT", nullable: true),
                    Amount = table.Column<decimal>(type: "TEXT", nullable: true),
                    TaxCode = table.Column<long>(type: "INTEGER", nullable: true),
                    Comments = table.Column<string>(type: "TEXT", nullable: true),
                    CustomerAccount = table.Column<string>(type: "TEXT", nullable: true),
                    CustomerName = table.Column<string>(type: "TEXT", nullable: true),
                    SalesPersont = table.Column<string>(type: "TEXT", nullable: true),
                    Lob = table.Column<string>(type: "TEXT", nullable: true),
                    SubLob = table.Column<string>(type: "TEXT", nullable: true),
                    Reconciled = table.Column<bool>(type: "INTEGER", nullable: true),
                    MatchStatus = table.Column<int>(type: "INTEGER", nullable: true),
                    CreateDate = table.Column<string>(type: "TEXT", nullable: true),
                    ReconciledDate = table.Column<string>(type: "TEXT", nullable: true),
                    ReconciledBy = table.Column<string>(type: "TEXT", nullable: true),
                    UpdateDate = table.Column<string>(type: "TEXT", nullable: true),
                    UpdatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    StoreNumber = table.Column<string>(type: "TEXT", nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BellSources", x => x.Id);
                });

            migrationBuilder.CreateTable(
               name: "OnlyInBell",
               columns: table => new
               {
                   Id = table.Column<long>(type: "INTEGER", nullable: false)
                      .Annotation("Sqlite:Autoincrement", true),
                   IsReconciled = table.Column<bool>(type: "INTEGER", nullable: true),

                   TransactionDate = table.Column<string>(type: "TEXT", nullable: true),
                   DealerCode = table.Column<string>(type: "TEXT", nullable: true),
                   Brand = table.Column<string>(type: "TEXT", nullable: true),
                   OrderNumber = table.Column<long>(type: "INTEGER", nullable: true),
                   Phone = table.Column<long>(type: "INTEGER", nullable: true),
                   Imei = table.Column<string>(type: "TEXT", nullable: true),
                   Simserial = table.Column<string>(type: "TEXT", nullable: true),
                   RebateType = table.Column<string>(type: "TEXT", nullable: true),
                   Product = table.Column<string>(type: "TEXT", nullable: true),
                   Amount = table.Column<decimal>(type: "TEXT", nullable: true),
                   TaxCode = table.Column<long>(type: "INTEGER", nullable: true),
                   Comments = table.Column<string>(type: "TEXT", nullable: true),
                   CustomerAccount = table.Column<string>(type: "TEXT", nullable: true),
                   CustomerName = table.Column<string>(type: "TEXT", nullable: true),
                   SalesPersont = table.Column<string>(type: "TEXT", nullable: true),
                   Lob = table.Column<string>(type: "TEXT", nullable: true),
                   SubLob = table.Column<string>(type: "TEXT", nullable: true),
                   Reconciled = table.Column<bool>(type: "INTEGER", nullable: true),
                   MatchStatus = table.Column<int>(type: "INTEGER", nullable: true),
                   CreateDate = table.Column<string>(type: "TEXT", nullable: true),
                   ReconciledDate = table.Column<string>(type: "TEXT", nullable: true),
                   ReconciledBy = table.Column<string>(type: "TEXT", nullable: true),
                   UpdateDate = table.Column<string>(type: "TEXT", nullable: true),
                   UpdatedBy = table.Column<string>(type: "TEXT", nullable: true),
                   StoreNumber = table.Column<string>(type: "TEXT", nullable: true),
               },
               constraints: table =>
               {
                   table.PrimaryKey("PK_OnlyInBell", x => x.Id);
               });

            migrationBuilder.CreateTable(
                name: "StaplesSources",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TransactionDate = table.Column<string>(type: "TEXT", nullable: true),
                    Location = table.Column<string>(type: "TEXT", nullable: true),
                    DealerCode = table.Column<string>(type: "TEXT", nullable: true),
                    Brand = table.Column<string>(type: "TEXT", nullable: true),
                    OrderNumber = table.Column<long>(type: "INTEGER", nullable: true),
                    Phone = table.Column<long>(type: "INTEGER", nullable: true),
                    Imei = table.Column<string>(type: "TEXT", nullable: true),
                    Simserial = table.Column<string>(type: "TEXT", nullable: true),
                    RebateType = table.Column<string>(type: "TEXT", nullable: true),
                    Product = table.Column<string>(type: "TEXT", nullable: true),
                    Amount = table.Column<decimal>(type: "TEXT", nullable: false),
                    Msf = table.Column<string>(type: "TEXT", nullable: true),
                    TaxCode = table.Column<long>(type: "INTEGER", nullable: true),
                    Comments = table.Column<string>(type: "TEXT", nullable: true),
                    CustomerAccount = table.Column<string>(type: "TEXT", nullable: true),
                    CustomerName = table.Column<string>(type: "TEXT", nullable: true),
                    SalesPerson = table.Column<string>(type: "TEXT", nullable: true),
                    Lob = table.Column<string>(type: "TEXT", nullable: true),
                    SubLob = table.Column<string>(type: "TEXT", nullable: true),
                    Reconciled = table.Column<int>(type: "INTEGER", nullable: true),
                    MatchStatus = table.Column<int>(type: "INTEGER", nullable: false),
                    CreateDate = table.Column<string>(type: "TEXT", nullable: true),
                    ReconciledDate = table.Column<string>(type: "TEXT", nullable: true),
                    ReconciledBy = table.Column<string>(type: "TEXT", nullable: true),
                    UpdateDate = table.Column<string>(type: "TEXT", nullable: true),
                    UpdatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    BellTransactionId = table.Column<string>(type: "TEXT", nullable: true),
                    StoreNumber = table.Column<string>(type: "TEXT", nullable: true),
                    IsSelected = table.Column<int>(type: "INTEGER", nullable: true),
                    IsEditMode = table.Column<int>(type: "INTEGER", nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StaplesSources", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OnlyInStaples",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TransactionDate = table.Column<string>(type: "TEXT", nullable: true),
                    Location = table.Column<string>(type: "TEXT", nullable: true),
                    DealerCode = table.Column<string>(type: "TEXT", nullable: true),
                    Brand = table.Column<string>(type: "TEXT", nullable: true),
                    OrderNumber = table.Column<long>(type: "INTEGER", nullable: true),
                    Phone = table.Column<long>(type: "INTEGER", nullable: true),
                    Imei = table.Column<string>(type: "TEXT", nullable: true),
                    Simserial = table.Column<string>(type: "TEXT", nullable: true),
                    RebateType = table.Column<string>(type: "TEXT", nullable: true),
                    Product = table.Column<string>(type: "TEXT", nullable: true),
                    Amount = table.Column<decimal>(type: "TEXT", nullable: false),
                    Msf = table.Column<string>(type: "TEXT", nullable: true),
                    TaxCode = table.Column<long>(type: "INTEGER", nullable: true),
                    Comments = table.Column<string>(type: "TEXT", nullable: true),
                    CustomerAccount = table.Column<string>(type: "TEXT", nullable: true),
                    CustomerName = table.Column<string>(type: "TEXT", nullable: true),
                    SalesPerson = table.Column<string>(type: "TEXT", nullable: true),
                    Lob = table.Column<string>(type: "TEXT", nullable: true),
                    SubLob = table.Column<string>(type: "TEXT", nullable: true),
                    Reconciled = table.Column<int>(type: "INTEGER", nullable: true),
                    MatchStatus = table.Column<int>(type: "INTEGER", nullable: false),
                    CreateDate = table.Column<string>(type: "TEXT", nullable: true),
                    ReconciledDate = table.Column<string>(type: "TEXT", nullable: true),
                    ReconciledBy = table.Column<string>(type: "TEXT", nullable: true),
                    UpdatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    UpdateDate = table.Column<string>(type: "TEXT", nullable: true),
                    BellTransactionId = table.Column<string>(type: "TEXT", nullable: true),
                    StoreNumber = table.Column<string>(type: "TEXT", nullable: true),
                    IsSelected = table.Column<int>(type: "INTEGER", nullable: true),
                    IsEditMode = table.Column<int>(type: "INTEGER", nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OnlyInStaples", x => x.Id);
                });

            migrationBuilder.CreateTable(
               name: "CompareBellStaple",
               columns: table => new
               {
                   Id = table.Column<long>(type: "INTEGER", nullable: false)
                       .Annotation("Sqlite:Autoincrement", true),
                   MatchStatus = table.Column<int>(type: "INTEGER", nullable: true),
                   IsReconciled = table.Column<bool>(type: "INTEGER", nullable: true),

                   SId = table.Column<long>(type: "INTEGER", nullable: true),
                   STransactionDate = table.Column<string>(type: "TEXT", nullable: true),
                   SLocation = table.Column<string>(type: "TEXT", nullable: true),
                   SDealerCode = table.Column<string>(type: "TEXT", nullable: true),
                   SBrand = table.Column<string>(type: "TEXT", nullable: true),
                   SOrderNumber = table.Column<long>(type: "INTEGER", nullable: true),
                   SPhone = table.Column<long>(type: "INTEGER", nullable: true),
                   SImei = table.Column<string>(type: "TEXT", nullable: true),
                   SSimserial = table.Column<string>(type: "TEXT", nullable: true),
                   SRebateType = table.Column<string>(type: "TEXT", nullable: true),
                   SProduct = table.Column<string>(type: "TEXT", nullable: true),
                   SAmount = table.Column<decimal>(type: "TEXT", nullable: true),
                   SMsf = table.Column<string>(type: "TEXT", nullable: true),
                   STaxCode = table.Column<long>(type: "INTEGER", nullable: true),
                   SComments = table.Column<string>(type: "TEXT", nullable: true),
                   SCustomerAccount = table.Column<string>(type: "TEXT", nullable: true),
                   SCustomerName = table.Column<string>(type: "TEXT", nullable: true),
                   SSalesPerson = table.Column<string>(type: "TEXT", nullable: true),
                   SLob = table.Column<string>(type: "TEXT", nullable: true),
                   SSubLob = table.Column<string>(type: "TEXT", nullable: true),
                   SReconciled = table.Column<bool>(type: "INTEGER", nullable: true),
                   SMatchStatus = table.Column<int>(type: "INTEGER", nullable: true),
                   SCreateDate = table.Column<string>(type: "TEXT", nullable: true),
                   SReconciledDate = table.Column<string>(type: "TEXT", nullable: true),
                   SReconciledBy = table.Column<string>(type: "TEXT", nullable: true),
                   SUpdateDate = table.Column<string>(type: "TEXT", nullable: true),
                   SUpdatedBy = table.Column<string>(type: "TEXT", nullable: true),
                   SBellTransactionId = table.Column<string>(type: "TEXT", nullable: true),
                   SStoreNumber = table.Column<string>(type: "TEXT", nullable: true),

                   BId = table.Column<long>(type: "INTEGER", nullable: true),
                   BTransactionDate = table.Column<string>(type: "TEXT", nullable: true),
                   BDealerCode = table.Column<string>(type: "TEXT", nullable: true),
                   BBrand = table.Column<string>(type: "TEXT", nullable: true),
                   BOrderNumber = table.Column<long>(type: "INTEGER", nullable: true),
                   BPhone = table.Column<long>(type: "INTEGER", nullable: true),
                   BImei = table.Column<string>(type: "TEXT", nullable: true),
                   BSimserial = table.Column<string>(type: "TEXT", nullable: true),
                   BRebateType = table.Column<string>(type: "TEXT", nullable: true),
                   BProduct = table.Column<string>(type: "TEXT", nullable: true),
                   BAmount = table.Column<decimal>(type: "TEXT", nullable: true),
                   BComments = table.Column<string>(type: "TEXT", nullable: true),
                   BCustomerAccount = table.Column<string>(type: "TEXT", nullable: true),
                   BCustomerName = table.Column<string>(type: "TEXT", nullable: true),
                   BSalesPerson = table.Column<string>(type: "TEXT", nullable: true),
                   BLob = table.Column<string>(type: "TEXT", nullable: true),
                   BSubLob = table.Column<string>(type: "TEXT", nullable: true),
                   BReconciled = table.Column<bool>(type: "INTEGER", nullable: true),
                   BMatchStatus = table.Column<int>(type: "INTEGER", nullable: true),
                   BCreateDate = table.Column<string>(type: "TEXT", nullable: true),
                   BReconciledDate = table.Column<string>(type: "TEXT", nullable: true),
                   BReconciledBy = table.Column<string>(type: "TEXT", nullable: true),
                   BUpdateDate = table.Column<string>(type: "TEXT", nullable: true),
                   BUpdatedBy = table.Column<string>(type: "TEXT", nullable: true),
                   BStoreNumber = table.Column<string>(type: "TEXT", nullable: true),

                   IsSelected = table.Column<int>(type: "INTEGER", nullable: true),
                   IsEditMode = table.Column<int>(type: "INTEGER", nullable: true),


               },
               constraints: table =>
               {
                   table.PrimaryKey("PK_CompareBellStaple", x => x.Id);
               });

            migrationBuilder.CreateTable(
                name: "ErrorLogs",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    LogLevel = table.Column<string>(type: "TEXT", nullable: true),
                    EventName = table.Column<string>(type: "TEXT", nullable: true),
                    Source = table.Column<string>(type: "TEXT", nullable: true),
                    ExceptionMessage = table.Column<string>(type: "TEXT", nullable: true),
                    StackTrace = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<string>(type: "TEXT", nullable: true),
                    UploadedDate = table.Column<string>(type: "TEXT", nullable: true),
                    UserId = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ErrorLogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
               name: "FetchHistory",
               columns: table => new
               {
                   Id = table.Column<long>(type: "INTEGER", nullable: false)
                       .Annotation("Sqlite:Autoincrement", true),
                   StartDate = table.Column<string>(type: "TEXT", nullable: true),
                   EndDate = table.Column<string>(type: "TEXT", nullable: true),
                   UserId = table.Column<string>(type: "TEXT", nullable: true),
                   DateCreate = table.Column<string>(type: "TEXT", nullable: true),
               },
               constraints: table =>
               {
                   table.PrimaryKey("PK_FetchHistory", x => x.Id);
               });

            migrationBuilder.CreateTable(
              name: "Store",
              columns: table => new
              {
                  Id = table.Column<long>(type: "INTEGER", nullable: false)
                      .Annotation("Sqlite:Autoincrement", true),
                  Name = table.Column<string>(type: "TEXT", nullable: true),
                  Suite = table.Column<string>(type: "TEXT", nullable: true),
                  Address = table.Column<string>(type: "TEXT", nullable: true),
                  City = table.Column<string>(type: "TEXT", nullable: true),
                  ProvinceCode = table.Column<string>(type: "TEXT", nullable: true),
                  PostalCode = table.Column<string>(type: "TEXT", nullable: true),
                  Location = table.Column<string>(type: "TEXT", nullable: true),
                  Region = table.Column<string>(type: "TEXT", nullable: true),
                  District = table.Column<string>(type: "TEXT", nullable: true),
                  StatusTypeId = table.Column<short>(type: "INTEGER", nullable: true),
              },
              constraints: table =>
              {
                  table.PrimaryKey("PK_Store", x => x.Id);
              });

            migrationBuilder.CreateTable(
             name: "SyncLogs",
             columns: table => new
             {
                 Id = table.Column<long>(type: "INTEGER", nullable: false)
                     .Annotation("Sqlite:Autoincrement", true),
                 StartSync = table.Column<string>(type: "TEXT", nullable: true),
                 EndSync = table.Column<string>(type: "TEXT", nullable: true),
                 Success = table.Column<bool>(type: "INTEGER", nullable: true),
             },
             constraints: table =>
             {
                 table.PrimaryKey("PK_SyncLogs", x => x.Id);
             });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BellSources");

            migrationBuilder.DropTable(
                name: "CompareBellStaple");

            migrationBuilder.DropTable(
                name: "ErrorLogs");

            migrationBuilder.DropTable(
                name: "FetchHistory");

            migrationBuilder.DropTable(
                name: "OnlyInBell");

            migrationBuilder.DropTable(
                name: "OnlyInStaples");

            migrationBuilder.DropTable(
                name: "StaplesSources");

            migrationBuilder.DropTable(
                name: "Store");

            migrationBuilder.DropTable(
                name: "SyncLogs");
        }
    }
}
