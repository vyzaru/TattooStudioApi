namespace PetAPI.DTOs;

public class AppointmentDto
{
    public int Id { get; set; }
    public string ClientName { get; set; } = string.Empty;
    public string ClientPhone { get; set; } = string.Empty;
    public string? ClientEmail { get; set; }
    public DateTime AppointmentDate { get; set; }
    public int DurationHours { get; set; }
    public string? Notes { get; set; }
    public int? TattooId { get; set; }
    public string? TattooTitle { get; set; }
    public int MasterId { get; set; }
    public string? MasterName { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class CreateAppointmentDto
{
    public string ClientName { get; set; } = string.Empty;
    public string ClientPhone { get; set; } = string.Empty;
    public string? ClientEmail { get; set; }
    public DateTime AppointmentDate { get; set; }
    public int DurationHours { get; set; } = 1;
    public string? Notes { get; set; }
    public int? TattooId { get; set; }
    public int MasterId { get; set; }
}

public class UpdateAppointmentDto
{
    public string ClientName { get; set; } = string.Empty;
    public string ClientPhone { get; set; } = string.Empty;
    public string? ClientEmail { get; set; }
    public DateTime AppointmentDate { get; set; }
    public int DurationHours { get; set; }
    public string? Notes { get; set; }
    public int? TattooId { get; set; }
    public int MasterId { get; set; }
}