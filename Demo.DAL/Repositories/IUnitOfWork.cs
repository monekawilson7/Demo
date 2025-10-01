namespace Demo.DAL.Repositories;
public interface IUnitOfWork
{
    public IEmployeeRepositiry Employees { get;  }
    public IDepartmentRepository Departments { get;  }
    Task<int> SaveChangesAsync();
}
