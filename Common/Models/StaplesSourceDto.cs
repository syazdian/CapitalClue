using System.ComponentModel.DataAnnotations.Schema;

namespace CapitalClue.Common.Models;

public class StaplesSourceDto
{
    public long Id { get; set; }
    public DateTime? TransactionDate { get; set; }
    public string? Location { get; set; }
    public string? DealerCode { get; set; }
    public string? Brand { get; set; }
    public string? OrderNumber { get; set; }
    public long? Phone { get; set; }
    public string? Imei { get; set; }
    public string? Simserial { get; set; }
    public string? RebateType { get; set; }
    public string? Product { get; set; }
    public decimal? Amount { get; set; }
    public decimal? Msf { get; set; }
    public string? TaxCode { get; set; }
    public string? Comments { get; set; }
    public string? CustomerAccount { get; set; }
    public string? CustomerName { get; set; }
    public string? SalesPerson { get; set; }
    public string? Lob { get; set; }
    public string? SubLob { get; set; }
    public bool Reconciled { get; set; } = false;
    public short? MatchStatus { get; set; }
    public DateTime? CreateDate { get; set; }
    public DateTime? ReconciledDate { get; set; }
    public string? ReconciledBy { get; set; }
    public DateTime? UpdateDate { get; set; }
    public string? UpdatedBy { get; set; }
    public string? BellTransactionId { get; set; }
    public string? StoreNumber { get; set; }
    [NotMapped]
    public bool IsEditMode { get; set; }
    [NotMapped]
    public bool IsSelected { get; set; }
}