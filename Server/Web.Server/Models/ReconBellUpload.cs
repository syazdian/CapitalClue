using System;
using System.Collections.Generic;

namespace Bell.Reconciliation.Web.Server.Models;

public partial class ReconBellUpload
{
    public long Id { get; set; }

    public DateTime? TransactionDate { get; set; }

    public string? DealerCode { get; set; }

    public string? OrderNumber { get; set; }

    public long? Phone { get; set; }

    public string? Imei { get; set; }

    public string? Simserial { get; set; }

    public string? RebateType { get; set; }

    public string? Product { get; set; }

    public decimal? Amount { get; set; }

    public string? Comments { get; set; }

    public string? CustomerAccount { get; set; }

    public string? CustomerName { get; set; }

    public string? Lob { get; set; }

    public short? Reconciled { get; set; }

    public short? MatchStatus { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? ReconciledDate { get; set; }

    public string? ReconciledBy { get; set; }

    public string? SubLob { get; set; }

    public string? StoreNumber { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public string? UpdatedBy { get; set; }

    public string? Brand { get; set; }

    public int? UploadHistoryId { get; set; }
}
