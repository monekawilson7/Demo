using System.Linq.Expressions;

namespace Demo.DAL.Repositories;
public interface IEmployeeRepositiry : IReopsitory<Employee>
{
   IEnumerable<Employee> GetAll(String name);
    IQueryable<Employee> GetAllAsQuerable();
    IEnumerable<TResult> GetAll<TResult>(Expression<Func<Employee, TResult>> resultSelector,
        Expression<Func<Employee, bool>> predicate = null);

}
