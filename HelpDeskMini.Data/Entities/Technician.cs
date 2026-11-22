using System.ComponentModel.DataAnnotations;

namespace HelpDeskMini.Data.Entities;

public class Technician
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Името на техника е задължително.")]
    [StringLength(100)]
    public string FullName { get; set; } = string.Empty;

    [StringLength(100)]
    [EmailAddress(ErrorMessage = "Невалиден имейл адрес.")]
    public string? Email { get; set; }

    public ICollection<HelpRequest> AssignedRequests { get; set; } = new List<HelpRequest>();
}
