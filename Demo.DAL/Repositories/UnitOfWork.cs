namespace Demo.DAL.Repositories;
public class UnitOfWork (CompanyDBContext dBContext,
    IEmployeeRepositiry employee,
    IDepartmentRepository department)
    : IUnitOfWork
{
    public IEmployeeRepositiry Employees => employee;

    public IDepartmentRepository Departments => department;

    public int SaveChanges()=> dBContext.SaveChanges();
}
