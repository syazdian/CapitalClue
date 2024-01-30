using System;
using System.Collections.Generic;

namespace Bell.Reconciliation.Web.Server.Data.Sqlserver;

public partial class BellSource
{
    public int Id { get; set; }

    public long? Phone { get; set; }

    public int? Amount { get; set; }

    public string? Comment { get; set; }

    public long? OrderNumber { get; set; }

    public long? Imei { get; set; }

    public DateTime? TransactionDate { get; set; }

    public string? CustomerName { get; set; }

    public string? Lob { get; set; }

    public string? SubLob { get; set; }

    public string? RebateType { get; set; }

    public string? Reconciled { get; set; }

    public string? ReconciledBy { get; set; }

    public DateTime? ReconciledDate { get; set; }

    public string? MatchStatus { get; set; }

    public DateTime? CreateDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public DateTime? TransDate { get; set; }
}
