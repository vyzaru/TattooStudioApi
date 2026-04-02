using Microsoft.EntityFrameworkCore;
using PetAPI.Data;
using PetAPI.DTOs;
using PetAPI.Models;

namespace PetAPI.Services;

public class MasterService : IMasterService
{
    private readonly TattooStudioDbContext _context;
    private readonly ILogger<MasterService> _logger;

    public MasterService(TattooStudioDbContext context, ILogger<MasterService> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<MasterDto>> GetAllMastersAsync()
    {
        _logger.LogInformation("Getting all masters");

        var masters = await _context.Masters.OrderBy(m => m.FullName).ToListAsync();

        return masters.Select(m => MapToDto(m));
    }

    public async Task<MasterDto?> GetMasterByIdAsync(int id)
    {
        _logger.LogInformation("Getting master with ID: {MasterId}", id);

        var master = await _context.Masters.FindAsync(id);

        return master == null ? null : MapToDto(master);
    }

    public async Task<MasterDto> CreateMasterAsync(CreateMasterDto createDto)
    {
        await ValidateMasterEmailAsync(createDto.Email);

        _logger.LogInformation("Creating new master: {FullName}", createDto.FullName);

        var master = new Master
        {
            FullName = createDto.FullName,
            Specialization = createDto.Specialization,
            ExperienceYears = createDto.ExperienceYears,
            Email = createDto.Email,
            Phone = createDto.Phone,
            PortfolioUrl = createDto.PortfolioUrl,
            IsActive = true,
            CreatedAt = DateTime.Now
        };

        _context.Masters.Add(master);
        await _context.SaveChangesAsync();

        return MapToDto(master);
    }

    public async Task<MasterDto?> UpdateMasterAsync(int id, UpdateMasterDto updateDto)
    {
        var master = await _context.Masters.FindAsync(id);
        
        if (master == null)
        {
            _logger.LogWarning("Master with ID {MasterId} not found for update", id);
            return null;
        }
        
        if (master.Email != updateDto.Email)
        {
            await ValidateMasterEmailAsync(updateDto.Email, id);
        }

        _logger.LogInformation("Updating master ID {MasterId}: {FullName}", id, updateDto.FullName);
        
        master.FullName = updateDto.FullName;
        master.Specialization = updateDto.Specialization;
        master.ExperienceYears = updateDto.ExperienceYears;
        master.Email = updateDto.Email;
        master.Phone = updateDto.Phone;
        master.PortfolioUrl = updateDto.PortfolioUrl;
        master.IsActive = updateDto.IsActive;

        await _context.SaveChangesAsync();

        return MapToDto(master);
    }

    public async Task<bool> DeleteMasterAsync(int id)
    {
        var master = await _context.Masters.FindAsync(id);

        if (master == null)
        {
            _logger.LogWarning("Master with ID {MasterID} not found to deletion", id);
            return false;
        }

        _logger.LogInformation("Deleting master ID {MasterId}: {FullName}", id, master.FullName);

        _context.Masters.Remove(master);
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<IEnumerable<MasterDto>> GetMasterBySpecializationAsync(string specialization)
    {
        _logger.LogInformation("Getting masters by specialization: {Specialization}", specialization);

        var masters = await _context.Masters
            .Where(m => m.Specialization.ToLower().Contains(specialization.ToLower()))
            .OrderBy(m => m.FullName)
            .ToListAsync();
        
        return masters.Select(m => MapToDto(m));
    }

    public async Task<IEnumerable<MasterDto>> GetActiveMastersAsync()
    {
        _logger.LogInformation("Getting active masters");

        var masters = await _context.Masters.Where(m => m.IsActive).OrderBy(m => m.FullName).ToListAsync();

        return masters.Select(m => MapToDto(m));
    }

    private async Task ValidateMasterEmailAsync(string email, int? excludeId = null)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email is required");

        var exists = await _context.Masters
            .Where(m => m.Email == email)
            .Where(m => excludeId == null || m.Id != excludeId)
            .AnyAsync();
        
        if (exists)
            throw new InvalidOperationException($"Master with email {email} already exists");

        _logger.LogDebug("Email {Email} is valid", email);
    }

    private MasterDto MapToDto(Master master)
    {
        return new MasterDto
        {
            Id = master.Id,
            FullName = master.FullName,
            Specialization = master.Specialization,
            ExperienceYears = master.ExperienceYears,
            Email = master.Email,
            Phone = master.Phone,
            PortfolioUrl = master.PortfolioUrl,
            IsActive = master.IsActive
        };
    }
}