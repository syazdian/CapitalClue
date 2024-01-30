using System;
using System.Collections.Generic;

namespace Bell.Reconciliation.Web.Server.Models;

public partial class OrderPhoneImei
{
    public long Id { get; set; }

    public string MasterId { get; set; } = null!;

    public int Version { get; set; }

    public string Value { get; set; } = null!;

    public string Type { get; set; } = null!;

    public string? Notes { get; set; }

    public DateTime UpdatedTime { get; set; }

    public string UpdatedBy { get; set; } = null!;
}
