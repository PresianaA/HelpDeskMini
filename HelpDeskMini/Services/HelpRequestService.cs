using HelpDeskMini.Models;
using HelpDeskMini.Services;
using HelpDeskMini.Web.Data;

using Microsoft.EntityFrameworkCore;

namespace HelpDeskMini.Services
{
    public class HelpRequestService : IHelpRequestService
    {
        private readonly AppDbContext _context;

        public HelpRequestService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<HelpRequest>> GetAllAsync(string? search, string? status)
        {
            var requests = _context.HelpRequests.AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                requests = requests.Where(r => r.Title.Contains(search));
            }

            if (!string.IsNullOrWhiteSpace(status))
            {
                requests = requests.Where(r => r.Status == status);
            }

            return await requests.OrderByDescending(r => r.Id).ToListAsync();
        }

        public async Task<HelpRequest?> GetByIdAsync(int id)
        {
            return await _context.HelpRequests.FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task AddAsync(HelpRequest request)
        {
            _context.HelpRequests.Add(request);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(HelpRequest request)
        {
            _context.HelpRequests.Update(request);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var request = await GetByIdAsync(id);
            if (request != null)
            {
                _context.HelpRequests.Remove(request);
                await _context.SaveChangesAsync();
            }
        }
    }
}
