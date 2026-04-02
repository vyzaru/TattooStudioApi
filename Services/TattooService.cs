using Microsoft.EntityFrameworkCore;
using PetAPI.Data;
using PetAPI.DTOs;
using PetAPI.Models;

namespace PetAPI.Services;

public class TattooService : ITattooService
{
    private readonly TattooStudioDbContext _context;
    private readonly ILogger<TattooService> _logger;

    public TattooService(TattooStudioDbContext context, ILogger<TattooService> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<TattooDto>> GetAllTattoosAsync()
    {
        _logger.LogInformation("Getting all tattoos");

        var tattoos = await _context.Tattoos.OrderBy(t => t.Title).ToListAsync();

        return tattoos.Select(t => MapToDto(t));
    }

    public async Task<TattooDto?> GetTattooByIdAsync(int id)
    {
        _logger.LogInformation("Getting tattoo with ID: {MasterId}", id);

        var tattoo = await _context.Tattoos.FindAsync(id);
        
        return tattoo == null ? null : MapToDto(tattoo);
    }

    public async Task<TattooDto> CreateTattooAsync(CreateTattooDto createDto)
    {
        await ValidateTattooTitleAsync(createDto.Title);
        await ValidateMasterExistsAsync(createDto.MasterId);
        ValidatePrice(createDto.Price);

        _logger.LogInformation("Creating new tattoo: {Title}", createDto.Title);

        var tattoo = new Tattoo
        {
            Title = createDto.Title,
            Description = createDto.Description,
            Style = createDto.Style,
            BodyPlacement = createDto.BodyPlacement,
            ImageUrl = createDto.ImageUrl,
            Price = createDto.Price,
            MasterId = createDto.MasterId,
            CreatedAt = DateTime.Now
        };

        _context.Tattoos.Add(tattoo);
        await _context.SaveChangesAsync();

        return MapToDto(tattoo);
    }

    public async Task<TattooDto?> UpdateTattooAsync(int id, UpdateTattooDto updateDto)
    {
        var tattoo = await _context.Tattoos.FindAsync(id);
        
        if (tattoo == null)
        {
            _logger.LogWarning("Tattoo with ID {TattooId} not found for update", id);
            return null;
        }

        if (tattoo.Title != updateDto.Title)
        {
            await ValidateTattooTitleAsync(updateDto.Title, id);
        }
        
        ValidatePrice(updateDto.Price);

        _logger.LogInformation("Updating tattoo ID {MasterId}: {Title}", id, updateDto.Title);
        
        tattoo.Title = updateDto.Title;
        tattoo.Description = updateDto.Description;
        tattoo.Style = updateDto.Style;
        tattoo.BodyPlacement = updateDto.BodyPlacement;
        tattoo.ImageUrl = updateDto.ImageUrl;
        tattoo.Price = updateDto.Price;

        await _context.SaveChangesAsync();

        return MapToDto(tattoo);
    }

    public async Task<bool> DeleteTattooAsync(int id)
    {
        var tattoo = await _context.Tattoos.FindAsync(id);

        if (tattoo == null)
        {
            _logger.LogWarning("Tattoo with ID {TattooId} not found to deletion", id);
            return false;
        }

        _logger.LogInformation("Deleting master ID {TattooId}: {Title}", id, tattoo.Title);

        _context.Tattoos.Remove(tattoo);
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<IEnumerable<TattooDto>> GetTattoosByMasterAsync(int masterId)
    {
        _logger.LogInformation("Getting tattoos by masterId: {masterId}", masterId);

        var tattoos = await _context.Tattoos
            .Where(t => t.MasterId == masterId)
            .OrderBy(t => t.Title)
            .ToListAsync();
        
        return tattoos.Select(t => MapToDto(t));
    }

    public async Task<IEnumerable<TattooDto>> GetTattoosByStyleAsync(string style)
    {
        _logger.LogInformation("Getting tattoos by style: {Style}", style);

        var tattoos = await _context.Tattoos
            .Where(t => t.Style == style)
            .OrderBy(t => t.Title)
            .ToListAsync();
        
        return tattoos.Select(t => MapToDto(t));
    }

    public async Task<IEnumerable<TattooDto>> GetTattoosByPriceRangeAsync(decimal minPrice, decimal maxPrice)
    {
        _logger.LogInformation("Getting tattoos by price in range of: {MinPrice} to {MaxPrice}", minPrice, maxPrice);

        var tattoos = await _context.Tattoos
            .Where(t => t.Price >= minPrice)
            .Where(t => t.Price <= maxPrice)
            .OrderBy(t => t.Title)
            .ToListAsync();

        return tattoos.Select(t => MapToDto(t));
    }

    private static TattooDto MapToDto(Tattoo tattoo)
    {
        return new TattooDto
        {
            Id = tattoo.Id,
            Title = tattoo.Title,
            Description = tattoo.Description,
            Style = tattoo.Style,
            BodyPlacement = tattoo.BodyPlacement,
            ImageUrl = tattoo.ImageUrl,
            Price = tattoo.Price,
            MasterId = tattoo.MasterId
        };
    }

    private async Task ValidateTattooTitleAsync(string title, int? excludeId = null)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Tattoo title is required");
        
        if (title.Length < 3)
            throw new ArgumentException("Tattoo title must be at least 3 characters");

        if (title.Length > 200)
            throw new ArgumentException("Tatto title cannot exceed 200 characters");

        var exists = await _context.Tattoos
            .Where(t => t.Title.ToLower() == title.ToLower())
            .Where(t => excludeId == null || t.Id != excludeId)
            .AnyAsync();

        if (exists)
            throw new InvalidOperationException($"Tattoo with title '{title}' already exists");

        _logger.LogDebug("Title '{Title}' is valid", title);
    }

    public async Task ValidateMasterExistsAsync(int masterId)
    {
        var masterExists = await _context.Masters.AnyAsync(m => m.Id == masterId);

        if (!masterExists)
            throw new ArgumentException($"Master with ID {masterId} does not exist");

        _logger.LogDebug("Master with ID {MasterId} exists", masterId);
    }

    private void ValidatePrice(decimal price)
    {
        if (price < 0)
            throw new ArgumentException("Price must be greater than 0");

        if (price > 1000000)
            throw new ArgumentException("Price cannot be exceeded 1,000,000");

        _logger.LogDebug("Price {Price} is valid", price);
    }
}