using System.Threading.Tasks;

namespace Demo.DAL.Repositories;
public class UnitOfWork (CompanyDBContext dBContext,
    IEmployeeRepositiry employee,
    IDepartmentRepository department)
    : IUnitOfWork
{
    public IEmployeeRepositiry Employees => employee;

    public IDepartmentRepository Departments => department;

    public async Task<int> SaveChangesAsync()=> await dBContext.SaveChangesAsync();
}
