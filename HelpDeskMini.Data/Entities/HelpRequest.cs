using System.ComponentModel.DataAnnotations;

namespace HelpDeskMini.Data.Entities;

public class HelpRequest
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Заглавието е задължително.")]
    [StringLength(100, ErrorMessage = "Заглавието не може да е повече от 100 символа.")]
    public string Title { get; set; } = string.Empty;

    [Required(ErrorMessage = "Кабинетът е задължителен.")]
    [StringLength(20, ErrorMessage = "Кабинетът не може да е повече от 20 символа.")]
    public string Cabinet { get; set; } = string.Empty;

    [Required(ErrorMessage = "Описанието е задължително.")]
    [StringLength(500, ErrorMessage = "Описанието не може да е повече от 500 символа.")]
    public string Description { get; set; } = string.Empty;

    [Required(ErrorMessage = "Изберете приоритет.")]
    [RegularExpression("^(Нисък|Среден|Висок)$", ErrorMessage = "Приоритетът трябва да е Нисък, Среден или Висок.")]
    public string Priority { get; set; } = "Среден";

    [Required(ErrorMessage = "Изберете статус.")]
    [RegularExpression("^(Нова|В процес|Решена)$", ErrorMessage = "Изберете валиден статус.")]
    public string Status { get; set; } = "Нова";

    // Допълнителни връзки към поддържащите ентитита.
    public int? DepartmentId { get; set; }
    public Department? Department { get; set; }

    public int? TechnicianId { get; set; }
    public Technician? Technician { get; set; }
}
