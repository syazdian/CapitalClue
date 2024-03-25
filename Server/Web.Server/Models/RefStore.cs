using System;
using System.Collections.Generic;

namespace Bell.Reconciliation.Web.Server.Models;

public partial class RefStore
{
    public int StoreId { get; set; }

    public string Name { get; set; } = null!;

    public string? Suite { get; set; }

    public string? Address { get; set; }

    public string? City { get; set; }

    public string? ProvinceCode { get; set; }

    public string? PostalCode { get; set; }

    public string? Location { get; set; }

    public string? Region { get; set; }

    public string? District { get; set; }

    public short StatusTypeId { get; set; }

    public virtual ICollection<RefDealer> RefDealers { get; set; } = new List<RefDealer>();

    public virtual RefStatusType StatusType { get; set; } = null!;
}
