using System.ComponentModel.DataAnnotations;

namespace LeaveManagementSystemPractice.web.Models.LeaveTypes;

public class LeaveTypeCreateVM
{
    public int Id { get; set; }   
    
    [Required]
    [StringLength(150, MinimumLength = 4, ErrorMessage = "Name must be between 4 and 150 characters.")]
    [Display(Name = "Leave Type Name")]
    public string Name { get; set; }
    
    [Required]
    [Range(1, 90, ErrorMessage = "Number of days must be between 1 and 90.")]
    [Display(Name = "Number of Days")]
    public int NumberOfDays { get; set; }
}