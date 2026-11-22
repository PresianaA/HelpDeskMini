using HelpDeskMini.Data;
using HelpDeskMini.Data.Entities;
using HelpDeskMini.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HelpDeskMini.Services;

public class HelpRequestService : IHelpRequestService
{
    private readonly AppDbContext _context;

    public HelpRequestService(AppDbContext context) { _context = context; }

    public async Task<List<HelpRequest>> GetAllAsync(string? search, string? status)
    {
        IQueryable<HelpRequest> query = _context.HelpRequests
            .AsNoTracking()
            .Include(x => x.Department)
            .Include(x => x.Technician);

        if (!string.IsNullOrWhiteSpace(search))
            query = query.Where(x => x.Title.Contains(search));

        if (!string.IsNullOrWhiteSpace(status))
            query = query.Where(x => x.Status == status);

        return await query.OrderByDescending(x => x.Id).ToListAsync();
    }

    public Task<HelpRequest?> GetByIdAsync(int id) => _context.HelpRequests
        .Include(x => x.Department)
        .Include(x => x.Technician)
        .FirstOrDefaultAsync(x => x.Id == id);

    public Task<List<Department>> GetDepartmentsAsync() =>
        _context.Departments.AsNoTracking().OrderBy(x => x.Name).ToListAsync();

    public Task<List<Technician>> GetTechniciansAsync() =>
        _context.Technicians.AsNoTracking().OrderBy(x => x.FullName).ToListAsync();

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
        var request = await _context.HelpRequests.FindAsync(id);
        if (request is null) return;
        _context.HelpRequests.Remove(request);
        await _context.SaveChangesAsync();
    }
}
