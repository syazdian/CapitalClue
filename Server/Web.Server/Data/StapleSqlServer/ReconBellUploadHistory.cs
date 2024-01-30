using System;
using System.Collections.Generic;

namespace CapitalClue.Web.Server.Data.Sqlserver;

public partial class ReconBellUploadHistory
{
    public int Id { get; set; } = 0;

    public string? FileName { get; set; }

    public int? SuccessRowCount { get; set; }

    public int? ProblemRowCount { get; set; }

    public string? ProblemRows { get; set; }

    public string? UploadedBy { get; set; }

    public DateTime? UploadedDate { get; set; }
}