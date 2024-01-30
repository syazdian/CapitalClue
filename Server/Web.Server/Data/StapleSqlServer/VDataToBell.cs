using System;
using System.Collections.Generic;

namespace CapitalClue.Web.Server.Data.Sqlserver;

public partial class VDataToBell
{
    public string Action { get; set; } = null!;

    public long EmployeeId { get; set; }

    public string DealerCode { get; set; } = null!;

    public string SubAgent { get; set; } = null!;

    public string Role { get; set; } = null!;

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string EmailPwd { get; set; } = null!;

    public string UserProfile { get; set; } = null!;

    public string? Email { get; set; }

    public string? PhoneNumber { get; set; }
}
