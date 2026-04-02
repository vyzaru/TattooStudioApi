using Microsoft.AspNetCore.Mvc;
using PetAPI.DTOs;
using PetAPI.Services;

namespace PetAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AppointmentsController : ControllerBase
{
    private readonly IAppointmentService _appointmentService;
    private readonly ILogger<AppointmentsController> _logger;

    public AppointmentsController(IAppointmentService appointmentService, ILogger<AppointmentsController> logger)
    {
        _appointmentService = appointmentService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAppointments()
    {
        var appointments = await _appointmentService.GetAllAppointmentsAsync();
        return Ok(appointments);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAppointmentById(int id)
    {
        var appointment = await _appointmentService.GetAppointmentByIdAsync(id);
        if (appointment == null)
            return NotFound($"Appointment with ID {id} not found");

        return Ok(appointment);
    }

    [HttpGet("master/{masterId}")]
    public async Task<IActionResult> GetAppointmentsByMaster(int masterId)
    {
        var appointments = await _appointmentService.GetAppointmentsByMasterAsync(masterId);
        return Ok(appointments);
    }

    [HttpGet("date/{date}")]
    public async Task<IActionResult> GetAppointmentsByDate(DateTime date)
    {
        var appointments = await _appointmentService.GetAppointmentsByDateAsync(date);
        return Ok(appointments);
    }

    [HttpGet("upcoming")]
    public async Task<IActionResult> GetUpcomingAppointments()
    {
        var appointments = await _appointmentService.GetUpcomingAppointmentsAsync();
        return Ok(appointments);
    }

    [HttpPost]
    public async Task<IActionResult> CreateAppointment([FromBody] CreateAppointmentDto createDto)
    {
        try
        {
            var appointment = await _appointmentService.CreateAppointmentAsync(createDto);
            return CreatedAtAction(nameof(GetAppointmentById), new { id = appointment.Id }, appointment);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new { error = ex.Message });
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAppointment(int id, [FromBody] UpdateAppointmentDto updateDto)
    {
        try
        {
            var updated = await _appointmentService.UpdateAppointmentAsync(id, updateDto);
            if (updated == null)
                return NotFound($"Appointment with ID {id} not found");
            return Ok(updated);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new { error = ex.Message });
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAppointment(int id)
    {
        var deleted = await _appointmentService.DeleteAppointmentAsync(id);

        if (!deleted)
            return NotFound($"Appointment with ID {id} not found");

        return NoContent();
    }
}