namespace CapitalClue.Common.Models;

public class CompareBellStapleDto
{
    public int Id { get; set; }

    public MatchStatus MatchStatus { get; set; }
    public bool IsReconciled { get; set; } = false;
    public long SId { get; set; }

    public DateTime? STransactionDate { get; set; }

    public string? SLocation { get; set; }

    public string? SDealerCode { get; set; }

    public string? SBrand { get; set; }

    public string? SOrderNumber { get; set; }

    public long? SPhone { get; set; }

    public string? SImei { get; set; }

    public string? SSimserial { get; set; }

    public string? SRebateType { get; set; }

    public string? SProduct { get; set; }

    public decimal? SAmount { get; set; }

    public decimal? SMsf { get; set; }

    public string? STaxCode { get; set; }

    public string? SComments { get; set; }

    public string? SCustomerAccount { get; set; }

    public string? SCustomerName { get; set; }

    public string? SSalesPerson { get; set; }

    public string? SLob { get; set; }

    public string? SSubLob { get; set; }

    public bool? SReconciled { get; set; }

    public short? SMatchStatus { get; set; }

    public DateTime? SCreateDate { get; set; }

    public DateTime? SReconciledDate { get; set; }

    public string? SReconciledBy { get; set; }

    public DateTime? SUpdateDate { get; set; }

    public string? SUpdatedBy { get; set; }
    public string? SBellTransactionId { get; set; }
    public string? SStoreNumber { get; set; }

    public long BId { get; set; }
    public DateTime? BTransactionDate { get; set; }
    public string? BDealerCode { get; set; }
    public string? BBrand { get; set; }
    public string? BOrderNumber { get; set; }

    public long? BPhone { get; set; }

    public string? BImei { get; set; }

    public string? BSimserial { get; set; }

    public string? BRebateType { get; set; }

    public string? BProduct { get; set; }

    public decimal? BAmount { get; set; }

    public string? BComments { get; set; }

    public string? BCustomerAccount { get; set; }

    public string? BCustomerName { get; set; }

    public string? BLob { get; set; }
    public string? BSubLob { get; set; }

    public bool? BReconciled { get; set; }

    public short? BMatchStatus { get; set; }

    public DateTime? BCreateDate { get; set; }

    public DateTime? BReconciledDate { get; set; }

    public string? BReconciledBy { get; set; }

    public DateTime? BUpdateDate { get; set; }

    public string? BUpdatedBy { get; set; }
    public string? BStoreNumber { get; set; }
    public bool IsSelected { get; set; }
    public bool IsEditMode { get; set; }
}