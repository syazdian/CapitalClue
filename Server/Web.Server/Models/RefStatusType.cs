using System;
using System.Collections.Generic;

namespace Bell.Reconciliation.Web.Server.Models;

public partial class RefStatusType
{
    public short Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<RefDealer> RefDealers { get; set; } = new List<RefDealer>();

    public virtual ICollection<RefStore> RefStores { get; set; } = new List<RefStore>();
}
