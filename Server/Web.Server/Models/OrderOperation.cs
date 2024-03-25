using System;
using System.Collections.Generic;

namespace Bell.Reconciliation.Web.Server.Models;

public partial class OrderOperation
{
    public long Id { get; set; }

    public string MasterId { get; set; } = null!;

    public string? OriginalTransactionId { get; set; }

    public string StapleTransactionId { get; set; } = null!;

    public int Version { get; set; }

    public DateTime TransactionDate { get; set; }

    public string TransactionType { get; set; } = null!;

    public string Action { get; set; } = null!;

    public string Status { get; set; } = null!;

    public string Data { get; set; } = null!;

    public string ActivityId { get; set; } = null!;

    public DateTime UpdatedTime { get; set; }

    public string UpdatedBy { get; set; } = null!;
}
