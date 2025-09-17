

namespace Demo.DAL.Repositories;
public class EmployeeRepositiry(CompanyDBContext context) : BaseRepositry<Employee>(context), IEmployeeRepositiry
{
    public IEnumerable<Employee> GetAll(string name)
    {
        return _dbSet.Where(x => x.Name == name).ToList();
    }
}
