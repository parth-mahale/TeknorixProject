using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Teknorix_project.Models;
using System.Text;

namespace Teknorix_project.Interfaces
{
    public interface IEmployee
    {
        public List<Employee> GetAllEmployee();
        public List<Employee> GetEmpById(string searchBy, string search);
        public Task AddEmployee(Employee employee);
        public Employee EditEmployee(int id ,Employee employee);
        public List<Employee> GetEmpList(string firstname, string lastname, string searchtb);
    }

}
