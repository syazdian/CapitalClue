using System;
using System.Collections.Generic;

#nullable disable

namespace CapitalClue.Common.Models;

public partial class ErrorLogDto
{
    public int Id { get; set; }
    public string LogLevel { get; set; }
    public string EventName { get; set; }
    public string Source { get; set; }
    public string ExceptionMessage { get; set; }
    public string StackTrace { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime UploadedDate { get; set; }
    public string UserId { get; set; }
}