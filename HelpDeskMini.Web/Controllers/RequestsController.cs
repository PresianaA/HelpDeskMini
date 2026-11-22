using HelpDeskMini.Data.Entities;
using HelpDeskMini.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HelpDeskMini.Web.Controllers;

public class RequestsController : Controller
{
    private readonly IHelpRequestService _service;
    public RequestsController(IHelpRequestService service) { _service = service; }

    public async Task<IActionResult> Index(string? search, string? status)
    {
        var requests = await _service.GetAllAsync(search, status);
        ViewBag.Search = search;
        ViewBag.Status = status;
        return View(requests);
    }

    public async Task<IActionResult> Details(int id)
    {
        var request = await _service.GetByIdAsync(id);
        return request is null ? NotFound() : View(request);
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        await LoadSelectionsAsync();
        return View(new HelpRequest());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(HelpRequest request)
    {
        ValidateChoices(request);
        if (!ModelState.IsValid)
        {
            await LoadSelectionsAsync(request.DepartmentId, request.TechnicianId);
            return View(request);
        }
        await _service.AddAsync(request);
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var request = await _service.GetByIdAsync(id);
        if (request is null) return NotFound();
        await LoadSelectionsAsync(request.DepartmentId, request.TechnicianId);
        return View(request);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, HelpRequest request)
    {
        if (id != request.Id) return NotFound();
        ValidateChoices(request);
        if (!ModelState.IsValid)
        {
            await LoadSelectionsAsync(request.DepartmentId, request.TechnicianId);
            return View(request);
        }

        var existing = await _service.GetByIdAsync(id);
        if (existing is null) return NotFound();
        existing.Title = request.Title;
        existing.Cabinet = request.Cabinet;
        existing.Description = request.Description;
        existing.Priority = request.Priority;
        existing.Status = request.Status;
        existing.DepartmentId = request.DepartmentId;
        existing.TechnicianId = request.TechnicianId;
        await _service.UpdateAsync(existing);
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        var request = await _service.GetByIdAsync(id);
        return request is null ? NotFound() : View(request);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _service.DeleteAsync(id);
        return RedirectToAction(nameof(Index));
    }

    private async Task LoadSelectionsAsync(int? departmentId = null, int? technicianId = null)
    {
        ViewBag.Departments = new SelectList(await _service.GetDepartmentsAsync(), "Id", "Name", departmentId);
        ViewBag.Technicians = new SelectList(await _service.GetTechniciansAsync(), "Id", "FullName", technicianId);
    }

    private void ValidateChoices(HelpRequest request)
    {
        if (request.Priority is not ("Нисък" or "Среден" or "Висок"))
            ModelState.AddModelError(nameof(request.Priority), "Изберете валиден приоритет.");
        if (request.Status is not ("Нова" or "В процес" or "Решена"))
            ModelState.AddModelError(nameof(request.Status), "Изберете валиден статус.");
    }
}
