using System.ComponentModel.DataAnnotations;

namespace HelpDeskMini.Data.Entities;

public class Department
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Името на отдела е задължително.")]
    [StringLength(80)]
    public string Name { get; set; } = string.Empty;

    [StringLength(200)]
    public string? Description { get; set; }

    public ICollection<HelpRequest> HelpRequests { get; set; } = new List<HelpRequest>();
}
