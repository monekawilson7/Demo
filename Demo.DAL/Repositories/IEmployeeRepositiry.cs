namespace Demo.DAL.Repositories;
public interface IEmployeeRepositiry : IReopsitory<Employee>
{
   IEnumerable<Employee> GetAll(String name);
}
