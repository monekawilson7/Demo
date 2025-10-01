using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Demo.BLL.DataTransferObjects.Employee;
public class EmployeeRequest
{
    [Required]
    [MaxLength(50, ErrorMessage ="Max Lenth should be 50 character")]
    [MinLength(5, ErrorMessage ="Min Lenth should be 5 character")]
    public string Name { get; set; } = null!;
    [Range(22,30)]
    public int? Age { get; set; }
    //[RegularExpression("^[1-9]{1,3}-[a-zA-Z]{5,10}-[a-zA-Z]{5,10}$",
    //    ErrorMessage = "Invalid Format, Example: 123-abcde-fghij")]
    public string? Address { get; set; }
    [DataType(DataType.Currency)]
    public decimal Salary { get; set; }
    [Display(Name = "Is Active")]
    public bool IsActive { get; set; }
    [EmailAddress]
    public string? Email { get; set; }
    [Display(Name = "Phone number")]
    [Phone]
    public string? PhoneNumber { get; set; }
    [Display(Name = "Hiring Date")]
    public DateOnly? HireDate { get; set; }
    public Gender Gender { get; set; }
    public EmployeeType EmployeeType { get; set; }
    [Display(Name = "Department")]
    public int? DepartmentID { get; set; }
    public IFormFile? Image { get; set; }
}

