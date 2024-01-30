using Microsoft.EntityFrameworkCore;

namespace CapitalClue.Common.Models;

public class StoreDto
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Suite { get; set; }

    public string? Address { get; set; }

    public string? City { get; set; }

    public string? ProvinceCode { get; set; }

    public string? PostalCode { get; set; }

    public string? Location { get; set; }

    public string? Region { get; set; }

    public string? District { get; set; }

    public short StatusTypeId { get; set; }
}