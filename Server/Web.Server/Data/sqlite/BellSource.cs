using System;
using System.Collections.Generic;

namespace CapitalClue.Web.Server.Data.sqlite;

public partial class BellSource
{
    public long Id { get; set; }

    public long? Phone { get; set; }

    public long? Amount { get; set; }

    public string? Comment { get; set; }

    public long? OrderNumber { get; set; }

    public string? Imei { get; set; }

    public DateTime? TransactionDate { get; set; }

    public string? CustomerName { get; set; }

    //public string? CommissionDetails { get; set; }

    public string? Lob { get; set; }

    public string? SubLob { get; set; }

    public string? RebateType { get; set; }

    public string? Reconciled { get; set; }

    public string? ReconciledBy { get; set; }

    public DateTime? ReconciledDate { get; set; }

    public string? MatchStatus { get; set; }
}