using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Teknorix_project.Models
{
    public class Departments
    {
        [Key]
        public int DeptID { get; set; }

        public string DepartmentName { get; set; }
       
    }
}
