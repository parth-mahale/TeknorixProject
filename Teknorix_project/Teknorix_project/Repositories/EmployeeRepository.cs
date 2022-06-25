//using Dapper;
using Microsoft.EntityFrameworkCore;
using Teknorix_project.Interfaces;
using Teknorix_project.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Teknorix_project.Repositories
{
    public class EmployeeRepository : IEmployee
    {
        private readonly EmployeeContext _Db;
        public EmployeeRepository(EmployeeContext Db)
        {
            _Db = Db;
        }

        public async Task AddEmployee(Employee employee)
        {
            //if (ModelState.IsValid)
            //{
                _Db.Add(employee);
                await _Db.SaveChangesAsync();
                //return RedirectToAction(nameof(Index));
           // }
          //  return (employee);
        }

        //public async Task EditEmployee(int id, Employee employee)
        //{

        //    //if (ModelState.IsValid)
        //    //{
        //    try
        //    {
        //        _Db.Update(employee);
        //        await _Db.SaveChangesAsync();
        //    }
        //    catch (Exception ex)
        //    {
        //       // return employee;
        //        //if (!EmployeeExists(employee.ID))
        //        //{
        //        //    return NotFound();
        //        //}
        //        //else
        //        //{
        //        //    throw;
        //        //}
        //    }
        //    //    }
        //    //    return RedirectToAction(nameof(Index));
        //    //}
        //    //return View(employee);
        //}

        public List<Employee> GetAllEmployee()
        {
            var EmployeeList = _Db.Employees.Include(d => d.Department).ToList();
            return EmployeeList;
        }

        public List<Employee> GetEmpById(string searchBy, string search)
        {
            if (searchBy == "Firstname")
            {
                return (_Db.Employees.Where(x => x.FirstName.StartsWith(search) || search == null).ToList());
            }
            else
            {
                return (_Db.Employees.Where(x => x.LastName.StartsWith(search) || search == null).ToList());
            }
        }

        public Employee EditEmployee(int id, Employee employee)
        {
             try
            {
                Employee fact1 = _Db.Employees.Where(a => a.ID == id).SingleOrDefault();
                var temp = _Db.Employees.Find(id);

                return new Employee
                    {
                        ID = temp.ID,
                        FirstName = temp.FirstName,
                        LastName = temp.LastName,
                        Gender = temp.Gender,
                        Address = temp.Address,
                        PhoneNumber = temp.PhoneNumber,
                        MobileNumber = temp.MobileNumber,
                        DeptID = temp.DeptID
                       // DeptName = temp == null ? "" : temp.DeptName
                    };
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<Employee> GetEmpList(string firstname, string lastname, string searchtb)
        {
            var factlist1 = _Db.Employees.Include(d => d.Department).AsNoTracking();
            var factlist = from FT in _Db.Employees
                           join DT in _Db.Department
                           on FT.DeptID equals DT.DeptID
                           into Dep
                           from DT in Dep.DefaultIfEmpty()

                           select new Employee
                           {
                               ID = FT.ID,
                               FirstName = FT.FirstName,
                               LastName = FT.LastName,
                               Gender = FT.Gender,
                               Address = FT.Address,
                               PhoneNumber = FT.PhoneNumber,
                               MobileNumber = FT.MobileNumber,
                               DeptID = DT.DeptID
                           };

            if (!String.IsNullOrEmpty(searchtb))
            {
                factlist = factlist.Where(x => x.FirstName.Contains(searchtb) || x.LastName.Contains(searchtb));
            }
            return factlist.ToList();
        }
    }
    
}
