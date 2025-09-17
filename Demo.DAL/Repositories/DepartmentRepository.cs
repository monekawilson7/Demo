


namespace Demo.DAL.Repositories
{
    public class DepartmentRepository(CompanyDBContext Context) 
        : BaseRepositry<Department>(Context), IDepartmentRepository

    {
       

    }
}
