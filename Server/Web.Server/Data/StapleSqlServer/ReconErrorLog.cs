using System;
using System.Collections.Generic;

namespace CapitalClue.Web.Server.Data.Sqlserver;

public partial class ReconErrorLog
{
    public int Id { get; set; }

    public string? LogLevel { get; set; }

    public string? EventName { get; set; }

    public string? Source { get; set; }

    public string? ExceptionMessage { get; set; }

    public string? StackTrace { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? UploadedDate { get; set; }

    public string? UserId { get; set; }
}
