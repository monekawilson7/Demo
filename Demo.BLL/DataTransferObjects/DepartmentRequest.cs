using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.DataTransferObjects
{
    public class DepartmentRequest
    {
        [Required(ErrorMessage ="Name is Reqired.")]
        public string Name { get; set; } = null!;
        [Required]
        public string Code { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
