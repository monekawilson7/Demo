


namespace Demo.DAL.Repositories
{
    public class DepartmentRepository(CompanyDBContext dbContext) : IDepartmentRepository
    {
        private CompanyDBContext _dbContext = dbContext;
        private DbSet<Department> _departments = dbContext.Departments;

        //public DepartmentRepository(CompanyDBContext dbContext)
        //{
        //    _dbContext = dbContext;
        //}

        public int Add(Department department)
        {
            _departments.Add(department);  
            return _dbContext.SaveChanges();
                }

        public int Delete(Department department)
        {

            _departments.Remove(department);
            return _dbContext.SaveChanges();
        }

        public int Update(Department department)
        {
            _departments.Update(department);
            return _dbContext.SaveChanges();
        }
        public IEnumerable<Department> GetAll(bool trackChanges = false)
        {
            return trackChanges ?
                _departments.ToList() :
                _departments.AsNoTracking()
                .ToList();
        }

        public Department? GetById(int id)
        {
            return _departments.Find(id);
        }

    }
}
