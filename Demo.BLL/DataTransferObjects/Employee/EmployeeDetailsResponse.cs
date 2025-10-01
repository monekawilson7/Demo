namespace Demo.BLL.DataTransferObjects.Employee;
public class EmployeeDetailsResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public int? Age { get; set; }
    public string? Address { get; set; }
    public decimal Salary { get; set; }
    public bool IsActive { get; set; }
    public string? Email { get; set; } = string.Empty;
    public string? PhoneNumber { get; set; } = string.Empty;
    public DateOnly HireDate { get; set; }
    public string Gender { get; set; }
    public string EmployeeType { get; set; }
    public int CreatedBy { get; set; }  
    public DateTime CreatedOn { get; set; }
    public int LastModifiedBy { get; set; }
    public DateTime LastModifiedOn { get; set; }
    public int DepartmentId { get; set; }
    public string? Department { get; set; }
    public string? Image { get; set; }
}
