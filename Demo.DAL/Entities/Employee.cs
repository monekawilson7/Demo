

    public class Employee : BaseEntitiy
    {
    public string Name { get; set; } = null!;
    public int? Age { get; set; }
    public string? Address { get; set; }
    public decimal Salary { get; set; }
    public bool IsActive { get; set; }
    public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public DateOnly HireDate { get; set; }
       public Gender gender { get; set; }
    public EmployeeType employeeType { get; set; }
}

