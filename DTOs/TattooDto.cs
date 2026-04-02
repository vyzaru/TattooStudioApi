namespace PetAPI.DTOs;

public class TattooDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string Style { get; set; } = string.Empty;
    public string BodyPlacement { get; set; } = string.Empty;
    public string? ImageUrl { get; set; }
    public decimal Price { get; set; }
    public int MasterId { get; set; }
    public string? MasterName { get; set; }
}

public class CreateTattooDto
{
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string Style { get; set; } = string.Empty;
    public string BodyPlacement { get; set; } = string.Empty;
    public string? ImageUrl { get; set; }
    public decimal Price { get; set; }
    public int MasterId { get; set; }
}

public class UpdateTattooDto
{
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string Style { get; set; } = string.Empty;
    public string BodyPlacement { get; set; } = string.Empty;
    public string? ImageUrl { get; set; }
    public decimal Price { get; set; }
}