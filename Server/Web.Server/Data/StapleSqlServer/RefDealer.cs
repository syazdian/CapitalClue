using System;
using System.Collections.Generic;

namespace CapitalClue.Web.Server.Data.Sqlserver;

public partial class RefDealer
{
    public string DealerCode { get; set; } = null!;

    public string Name { get; set; } = null!;

    public int StoreId { get; set; }

    public short StatusTypeId { get; set; }

    public virtual RefStatusType StatusType { get; set; } = null!;

    public virtual RefStore Store { get; set; } = null!;
}
