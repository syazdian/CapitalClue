using System;
using System.Collections.Generic;

namespace Bell.Reconciliation.Web.Server.Data.Sqlserver;

public partial class RefJobCode
{
    public string JobCode { get; set; } = null!;

    public string Role { get; set; } = null!;
}