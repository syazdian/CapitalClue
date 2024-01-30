using System;
using System.Collections.Generic;

namespace CapitalClue.Web.Server.Data.sqlite;

public partial class StaplesSource
{
    public long Id { get; set; }

    public long? Phone { get; set; }

    public long? Amount { get; set; }

    public string? Comment { get; set; }

    public long? OrderNumber { get; set; }

    public string? RebateType { get; set; }

    public string? Product { get; set; }

    public string? Rec { get; set; }

    public string? Imei { get; set; }

    public DateTime? TransactionDate { get; set; }

    public string? SalesPerson { get; set; }

    public string? CustomerName { get; set; }

    public long? TaxCode { get; set; }

    public long? Msf { get; set; }

    public string? DeviceCo { get; set; }

    public string? Location { get; set; }

    public string? Brand { get; set; }

    public string? Lob { get; set; }

    public string? SubLob { get; set; }

    public string? Reconciled { get; set; }

    public string? ReconciledBy { get; set; }

    public DateTime? ReconciledDate { get; set; }

    public long? MatchStatus { get; set; }
}