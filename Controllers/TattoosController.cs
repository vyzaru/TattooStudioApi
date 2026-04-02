using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc;
using PetAPI.DTOs;
using PetAPI.Services;

namespace PetAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TattoosController : ControllerBase
{
    private readonly ITattooService _tattooService;
    private readonly ILogger<TattoosController> _logger;

    public TattoosController(ITattooService tattooService, ILogger<TattoosController> logger)
    {
        _tattooService = tattooService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllTattoos()
    {
        var tattoos = await _tattooService.GetAllTattoosAsync();
        return Ok(tattoos);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetTattooById(int id)
    {
        var tattoo = await _tattooService.GetTattooByIdAsync(id);
        if (tattoo == null)
            return NotFound($"Master with ID {id} not found");

        return Ok(tattoo);
    }

    [HttpGet("master_id/{id}")]
    public async Task<IActionResult> GetTattoosByMasterId(int id)
    {
        var tattoos = await _tattooService.GetTattoosByMasterAsync(id);
        return Ok(tattoos);
    }

    [HttpGet("style/{style}")]
    public async Task<IActionResult> GetTattoosByStyle(string style)
    {
        var tattoos = await _tattooService.GetTattoosByStyleAsync(style);
        return Ok(tattoos);
    }

    [HttpGet("price-range")]
    public async Task<IActionResult> GetTattoosByPriceRange([FromQuery] decimal minPrice, [FromQuery] decimal maxPrice)
    {
        var tattoos = await _tattooService.GetTattoosByPriceRangeAsync(minPrice, maxPrice);
        return Ok(tattoos);
    }

    [HttpPost]
    public async Task<IActionResult> CreateTattoo([FromBody] CreateTattooDto createDto)
    {
        try
        {
            var tattoo = await _tattooService.CreateTattooAsync(createDto);
            return CreatedAtAction(nameof(GetTattooById), new { id = tattoo.Id }, tattoo);
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
    public async Task<IActionResult> UpdateTattoo(int id, [FromBody] UpdateTattooDto updateDto)
    {
        try
        {
            var updated = await _tattooService.UpdateTattooAsync(id, updateDto);
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
        var deleted = await _tattooService.DeleteTattooAsync(id);

        if (!deleted)
            return NotFound($"Master with ID {id} not found");

        return NoContent();
    }
}