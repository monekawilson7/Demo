using System.Linq.Expressions;

namespace Demo.DAL.Repositories;
public interface IEmployeeRepositiry : IReopsitory<Employee>
{
   //Task<IEnumerable<Employee>> GetAllAsync(String name);
    IQueryable<Employee> GetAllAsQuerable();
    Task<IEnumerable<TResult>> GetAllAsync<TResult>(Expression<Func<Employee, TResult>> resultSelector,
        Expression<Func<Employee, bool>> predicate = null);

}
