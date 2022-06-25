using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Teknorix_project.Models
{
    public class EmployeeContext : DbContext
    {
        //public EmployeeContext()
        //{

        //}
        public EmployeeContext(DbContextOptions<EmployeeContext> options) : base(options)
        {

        }
        public DbSet<Employee> Employees { get; set; }

        public DbSet<Departments> Department { get; set; }

        
    }
}
