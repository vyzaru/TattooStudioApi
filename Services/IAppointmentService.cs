using PetAPI.DTOs;

namespace PetAPI.Services;

public interface IAppointmentService
{
    Task<IEnumerable<AppointmentDto>> GetAllAppointmentsAsync();
    Task<AppointmentDto?> GetAppointmentByIdAsync(int id);
    Task<AppointmentDto> CreateAppointmentAsync(CreateAppointmentDto createDto);
    Task<AppointmentDto?> UpdateAppointmentAsync(int id, UpdateAppointmentDto updateDto);
    Task<bool> DeleteAppointmentAsync(int id);
    Task<IEnumerable<AppointmentDto>> GetAppointmentsByMasterAsync(int masterId);
    Task<IEnumerable<AppointmentDto>> GetAppointmentsByDateAsync(DateTime date);
    Task<IEnumerable<AppointmentDto>> GetUpcomingAppointmentsAsync();
}