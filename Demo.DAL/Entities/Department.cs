using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Entities
{
    public class Department : BaseEntitiy
    {
        public string Name {get; set;} = null!;
        public string Code {get; set;} = null!;
        public string? Description {get; set;}
        public DateTime CreatedAt {get; set;}
        public DateTime CreatedOn {get; set;}
        public ICollection<Employee> Employees { get; set; } = [];
    }
}
