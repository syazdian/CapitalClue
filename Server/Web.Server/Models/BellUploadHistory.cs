using System;
using System.Collections.Generic;

namespace Bell.Reconciliation.Web.Server.Models;

public partial class BellUploadHistory
{
    public int Id { get; set; }

    public string? FileName { get; set; }

    public int? SuccessRowCount { get; set; }

    public int? ProblemRowCount { get; set; }

    public string? ProblemRows { get; set; }

    public string? UploadBy { get; set; }

    public DateTime? UploadDate { get; set; }
}
