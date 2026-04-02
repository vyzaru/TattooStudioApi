using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc;
using PetAPI.DTOs;
using PetAPI.Services;

namespace PetAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MastersController : ControllerBase
{
    private readonly IMasterService _masterService;
    private readonly ILogger<MastersController> _logger;

    public MastersController(IMasterService masterService, ILogger<MastersController> logger)
    {
        _masterService = masterService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllMasters()
    {
        var masters = await _masterService.GetAllMastersAsync();
        return Ok(masters);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetMasterById(int id)
    {
        var master = await _masterService.GetMasterByIdAsync(id);
        if (master == null)
            return NotFound($"Master with ID {id} not found");

        return Ok(master);
    }

    [HttpGet("active")]
    public async Task<IActionResult> GetActiveMasters()
    {
        var masters = await _masterService.GetActiveMastersAsync();
        return Ok(masters);
    }

    [HttpGet("specialization/{specialization}")]
    public async Task<IActionResult> GetMastersBySpecialization(string specialization)
    {
        var masters = await _masterService.GetMasterBySpecializationAsync(specialization);
        return Ok(masters);
    }

    [HttpPost]
    public async Task<IActionResult> CreateMaster([FromBody] CreateMasterDto createDto)
    {
        try
        {
            var master = await _masterService.CreateMasterAsync(createDto);
            return CreatedAtAction(nameof(GetMasterById), new { id = master.Id }, master);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateMaster(int id, [FromBody] UpdateMasterDto updateDto)
    {
        try
        {
            var updated = await _masterService.UpdateMasterAsync(id, updateDto);
            if (updated == null)
                return NotFound($"Master with ID {id} not found");
            return Ok(updated);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMaster(int id)
    {
        var deleted = await _masterService.DeleteMasterAsync(id);
        if (!deleted)
            return NotFound($"Master with ID {id} not found");

        return NoContent();
    }
}