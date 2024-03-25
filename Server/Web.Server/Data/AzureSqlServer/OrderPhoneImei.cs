using System;
using System.Collections.Generic;

namespace Bell.Reconciliation.Web.Server.Data.Sqlserver;

public partial class OrderPhoneImei
{
    public int Id { get; set; }

    public string MasterId { get; set; } = null!;

    public int Version { get; set; }

    public string Value { get; set; } = null!;

    public string Type { get; set; } = null!;

    public string? Notes { get; set; }

    public DateTime UpdatedTime { get; set; }

    public string UpdatedBy { get; set; } = null!;
}