using HelpDeskMini.Models;

namespace HelpDeskMini.Services
{
    public interface IHelpRequestService
    {
        Task<List<HelpRequest>> GetAllAsync(string? search, string? status);
        Task<HelpRequest?> GetByIdAsync(int id);
        Task AddAsync(HelpRequest request);
        Task UpdateAsync(HelpRequest request);
        Task DeleteAsync(int id);
    }
}
