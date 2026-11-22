using HelpDeskMini.Data.Entities;

namespace HelpDeskMini.Services.Interfaces;

public interface IHelpRequestService
{
    Task<List<HelpRequest>> GetAllAsync(string? search, string? status);
    Task<HelpRequest?> GetByIdAsync(int id);
    Task<List<Department>> GetDepartmentsAsync();
    Task<List<Technician>> GetTechniciansAsync();
    Task AddAsync(HelpRequest request);
    Task UpdateAsync(HelpRequest request);
    Task DeleteAsync(int id);
}
