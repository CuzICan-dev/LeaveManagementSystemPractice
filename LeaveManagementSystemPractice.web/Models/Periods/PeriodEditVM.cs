using System.ComponentModel.DataAnnotations;

namespace LeaveManagementSystemPractice.web.Models.Periods;

public class PeriodEditVM
{
    public int Id { get; set; }

    [Required]
    [StringLength(150, MinimumLength = 4, ErrorMessage = "Name must be between 4 and 150 characters.")]
    [Display(Name = "Period Name")]
    public string Name { get; set; } = string.Empty;

    [Required]
    [DataType(DataType.Date)]
    [Display(Name = "Start Date")]
    public DateOnly StartDate { get; set; }

    [Required]
    [DataType(DataType.Date)]
    [Display(Name = "End Date")]
    public DateOnly EndDate { get; set; }
}