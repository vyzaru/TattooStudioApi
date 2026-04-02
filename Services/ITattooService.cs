using PetAPI.DTOs;

namespace PetAPI.Services;

public interface ITattooService
{
    Task<IEnumerable<TattooDto>> GetAllTattoosAsync();
    Task<TattooDto?> GetTattooByIdAsync(int id);
    Task<TattooDto> CreateTattooAsync(CreateTattooDto createDto);
    Task<TattooDto?> UpdateTattooAsync(int id, UpdateTattooDto updateDto);
    Task<bool> DeleteTattooAsync(int id);
    Task<IEnumerable<TattooDto>> GetTattoosByMasterAsync(int masterId);
    Task<IEnumerable<TattooDto>> GetTattoosByStyleAsync(string style);
    Task<IEnumerable<TattooDto>> GetTattoosByPriceRangeAsync(decimal minPrice, decimal maxPrice);
}