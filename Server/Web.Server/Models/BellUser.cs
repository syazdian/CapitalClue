using System;
using System.Collections.Generic;

namespace Bell.Reconciliation.Web.Server.Models;

public partial class BellUser
{
    public long EmployeeId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string? PhoneNumber { get; set; }

    public string? Email { get; set; }

    public bool? EmailPwd { get; set; }

    public bool? UserProfile { get; set; }

    public string? UserId { get; set; }

    public DateTime Created { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime Updated { get; set; }

    public string UpdatedBy { get; set; } = null!;
}
