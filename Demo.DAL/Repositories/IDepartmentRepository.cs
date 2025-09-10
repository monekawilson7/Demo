using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

    public interface IDepartmentRepository
    {
    IEnumerable<Department> GetAll(bool trackChanges = false);
    Department? GetById(int id);
    int Add(Department department);
    int Update(Department department);
    int Delete(Department department);
    }
