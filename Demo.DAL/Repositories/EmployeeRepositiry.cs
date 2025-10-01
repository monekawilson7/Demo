


using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Demo.DAL.Repositories;
public class EmployeeRepositiry(CompanyDBContext context) : BaseRepositry<Employee>(context), IEmployeeRepositiry
{
    public IEnumerable<Employee> GetAllAsync(string name)
    {
        return _dbSet.Where(x => x.Name == name).ToList();
    }
    public override async Task<IEnumerable<Employee>> GetAllAsync(bool trackChanges = false)
    {
        return trackChanges ? await _dbSet.Where (x=> !x.IsDeleted).ToListAsync() 
            :await _dbSet.AsNoTracking().Where(x => !x.IsDeleted).ToListAsync();

    }

    public async Task<IEnumerable<TResult>> GetAllAsync<TResult>(Expression<Func<Employee, TResult>> resultSelector,
        Expression<Func<Employee, bool>> predicate = null)
    {
        if(predicate is null)
        {
            return await _dbSet.Where(x => !x.IsDeleted)
            .Select(resultSelector).ToListAsync();
        }
        return await _dbSet.Where(x => !x.IsDeleted)
            .Where(predicate)
            .Select(resultSelector).ToListAsync();

    }

    public IQueryable<Employee> GetAllAsQuerable()
    {
        return _dbSet.Where(x => !x.IsDeleted);
    }
    override public async Task<Employee?> GetByIdAsync(int id)
    {
        return await  _dbSet.Include(e => e.Department)
            .FirstOrDefaultAsync(e=> e.Id == id);
    }
}
