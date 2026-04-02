using Microsoft.EntityFrameworkCore;
using PetAPI.Data;
using PetAPI.DTOs;
using PetAPI.Models;

namespace PetAPI.Services;

public class AppointmentService : IAppointmentService
{
    private readonly TattooStudioDbContext _context;
    private readonly ILogger<AppointmentService> _logger;

    public AppointmentService(TattooStudioDbContext context, ILogger<AppointmentService> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<AppointmentDto>> GetAllAppointmentsAsync()
    {
        _logger.LogInformation("Getting all appointments");

        var appointments = await _context.Appointments
            .Include(a => a.Master)
            .Include(a => a.Tattoo)
            .OrderBy(a => a.AppointmentDate)
            .ToListAsync();

        return appointments.Select(MapToDto);
    }

    public async Task<AppointmentDto?> GetAppointmentByIdAsync(int id)
    {
        _logger.LogInformation("Getting appointment with ID: {AppointmentId}", id);

        var appointment = await _context.Appointments
            .Include(a => a.Master)
            .Include(a => a.Tattoo)
            .FirstOrDefaultAsync(a => a.Id == id);

        return appointment == null ? null : MapToDto(appointment);
    }

    public async Task<AppointmentDto> CreateAppointmentAsync(CreateAppointmentDto createDto)
    {
        _logger.LogInformation("Creating new appointment for client: {ClientName}", createDto.ClientName);

        var appointment = new Appointment
        {
            ClientName = createDto.ClientName.Trim(),
            ClientPhone = createDto.ClientPhone.Trim(),
            ClientEmail = createDto.ClientEmail?.Trim(),
            AppointmentDate = createDto.AppointmentDate,
            DurationHours = createDto.DurationHours,
            Notes = createDto.Notes?.Trim(),
            TattooId = createDto.TattooId,
            MasterId = createDto.MasterId,
            CreatedAt = DateTime.Now
        };

        _context.Appointments.Add(appointment);
        await _context.SaveChangesAsync();

        return MapToDto(appointment);
    }

    public async Task<AppointmentDto?> UpdateAppointmentAsync(int id, UpdateAppointmentDto updateDto)
    {
        var appointment = await _context.Appointments
            .Include(a => a.Master)
            .Include(a => a.Tattoo)
            .FirstOrDefaultAsync(a => a.Id == id);

        if (appointment == null)
        {
            _logger.LogWarning("Appointment with ID {AppointmentId} not found for update", id);
            return null;
        }

        _logger.LogInformation("Updating appointment ID {AppointmentId} for client: {ClientName}", id, updateDto.ClientName);

        appointment.ClientName = updateDto.ClientName.Trim();
        appointment.ClientPhone = updateDto.ClientPhone.Trim();
        appointment.ClientEmail = updateDto.ClientEmail?.Trim();
        appointment.AppointmentDate = updateDto.AppointmentDate;
        appointment.DurationHours = updateDto.DurationHours;
        appointment.Notes = updateDto.Notes?.Trim();
        appointment.TattooId = updateDto.TattooId;
        appointment.MasterId = updateDto.MasterId;

        await _context.SaveChangesAsync();

        return MapToDto(appointment);
    }

    public async Task<bool> DeleteAppointmentAsync(int id)
    {
        var appointment = await _context.Appointments.FindAsync(id);

        if (appointment == null)
        {
            _logger.LogWarning("Appointment with ID {AppointmentId} not found for deletion", id);
            return false;
        }

        _logger.LogInformation("Deleting appointment ID {AppointmentId} for client: {ClientName}", id, appointment.ClientName);

        _context.Appointments.Remove(appointment);
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<IEnumerable<AppointmentDto>> GetAppointmentsByMasterAsync(int masterId)
    {
        _logger.LogInformation("Getting appointments for master ID: {MasterId}", masterId);

        var appointments = await _context.Appointments
            .Include(a => a.Master)
            .Include(a => a.Tattoo)
            .Where(a => a.MasterId == masterId)
            .OrderBy(a => a.AppointmentDate)
            .ToListAsync();

        return appointments.Select(MapToDto);
    }

    public async Task<IEnumerable<AppointmentDto>> GetAppointmentsByDateAsync(DateTime date)
    {
        _logger.LogInformation("Getting appointments for date: {Date}", date.ToShortDateString());

        var startOfDay = date.Date;
        var endOfDay = startOfDay.AddDays(1);

        var appointments = await _context.Appointments
            .Include(a => a.Master)
            .Include(a => a.Tattoo)
            .Where(a => a.AppointmentDate >= startOfDay && a.AppointmentDate < endOfDay)
            .OrderBy(a => a.AppointmentDate)
            .ToListAsync();

        return appointments.Select(MapToDto);
    }

    public async Task<IEnumerable<AppointmentDto>> GetUpcomingAppointmentsAsync()
    {
        _logger.LogInformation("Getting upcoming appointments");

        var now = DateTime.Now;

        var appointments = await _context.Appointments
            .Include(a => a.Master)
            .Include(a => a.Tattoo)
            .Where(a => a.AppointmentDate >= now)
            .OrderBy(a => a.AppointmentDate)
            .ToListAsync();

        return appointments.Select(MapToDto);
    }

    private static AppointmentDto MapToDto(Appointment appointment)
    {
        return new AppointmentDto
        {
            Id = appointment.Id,
            ClientName = appointment.ClientName,
            ClientPhone = appointment.ClientPhone,
            ClientEmail = appointment.ClientEmail,
            AppointmentDate = appointment.AppointmentDate,
            DurationHours = appointment.DurationHours,
            Notes = appointment.Notes,
            TattooId = appointment.TattooId,
            TattooTitle = appointment.Tattoo?.Title,
            MasterId = appointment.MasterId,
            MasterName = appointment.Master?.FullName,
            CreatedAt = appointment.CreatedAt
        };
    }
}