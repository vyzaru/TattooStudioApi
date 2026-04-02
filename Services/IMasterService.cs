using PetAPI.DTOs;

namespace PetAPI.Services;

public interface IMasterService
{
    Task<IEnumerable<MasterDto>> GetAllMastersAsync();
    Task<MasterDto?> GetMasterByIdAsync(int id);
    Task<MasterDto> CreateMasterAsync(CreateMasterDto createDto);
    Task<MasterDto?> UpdateMasterAsync(int id, UpdateMasterDto updateDto);
    Task<bool> DeleteMasterAsync(int id);
    Task<IEnumerable<MasterDto>> GetMasterBySpecializationAsync(string specialization);
    Task<IEnumerable<MasterDto>> GetActiveMastersAsync();
}