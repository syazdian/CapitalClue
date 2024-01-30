using System;
using System.Collections.Generic;

namespace Bell.Reconciliation.Web.Server.Models;

public partial class ReconBellUploadHistory
{
    public int Id { get; set; }

    public string? FileName { get; set; }

    public int? SuccessRowCount { get; set; }

    public int? ProblemRowCount { get; set; }

    public string? ProblemRows { get; set; }

    public string? UploadedBy { get; set; }

    public DateTime? UploadedDate { get; set; }
}
