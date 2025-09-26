


using System.Linq.Expressions;

namespace Demo.DAL.Repositories;
public class EmployeeRepositiry(CompanyDBContext context) : BaseRepositry<Employee>(context), IEmployeeRepositiry
{
    public IEnumerable<Employee> GetAll(string name)
    {
        return _dbSet.Where(x => x.Name == name).ToList();
    }
    public override IEnumerable<Employee> GetAll(bool trackChanges = false)
    {
        return trackChanges ?  _dbSet.Where (x=> !x.IsDeleted).ToList() 
            : _dbSet.AsNoTracking().Where(x => !x.IsDeleted).ToList();

    }

    public IEnumerable<TResult> GetAll<TResult>(Expression<Func<Employee, TResult>> resultSelector,
        Expression<Func<Employee, bool>> predicate = null)
    {
        if(predicate is null)
        {
            return _dbSet.Where(x => !x.IsDeleted)
            .Select(resultSelector).ToList();
        }
        return _dbSet.Where(x => !x.IsDeleted)
            .Where(predicate)
            .Select(resultSelector).ToList();

    }

    public IQueryable<Employee> GetAllAsQuerable()
    {
        return _dbSet.Where(x => !x.IsDeleted);
    }
    override public Employee? GetById(int id)
    {
        return  _dbSet.Include(e => e.Department)
            .FirstOrDefault(e=> e.Id == id);
    }
}
