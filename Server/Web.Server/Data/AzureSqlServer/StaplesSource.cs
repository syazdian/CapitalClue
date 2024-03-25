using System;
using System.Collections.Generic;

namespace Bell.Reconciliation.Web.Server.Data.Sqlserver;

public partial class StaplesSource
{
    public int Id { get; set; }

    public long? Phone { get; set; }

    public int? Amount { get; set; }

    public string? Comment { get; set; }

    public long? OrderNumber { get; set; }

    public string? RebateType { get; set; }

    public string? Product { get; set; }

    public string? Rec { get; set; }

    public long? Imei { get; set; }

    public string? SalesPerson { get; set; }

    public string? CustomerName { get; set; }

    public long? TaxCode { get; set; }

    public int? Msf { get; set; }

    public string? DeviceCo { get; set; }

    public string? Location { get; set; }

    public string? Brand { get; set; }

    public string? Lob { get; set; }

    public string? SubLob { get; set; }

    public string? Reconciled { get; set; }

    public string? ReconciledBy { get; set; }

    public DateTime? ReconciledDate { get; set; }

    public int? MatchStatus { get; set; }

    public DateTime? TransactionDate { get; set; }

    public DateTime? CreateDate { get; set; }

    public DateTime? UpdateDate { get; set; }
}