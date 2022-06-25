using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Teknorix_project.Interfaces;
using Teknorix_project.Models;

namespace Teknorix_project.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly EmployeeContext _context;
        private readonly IEmployee _obj;

        public EmployeesController(EmployeeContext context, IEmployee obj)
        {
            _context = context;
            _obj = obj;
        }

        // GET: Employees
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                var GetAllEmp = _obj.GetAllEmployee();
                var EmployeeList = _context.Employees.Include(d => d.Department).ToList();
                //string a = null;
                //var EmployeeList = (_context.Employees.Join(_context.Department, x => x.DeptID, y => y.DeptID,
                //(x, y) => new Employee
                //{
                //    ID = x.ID,
                //    FirstName = x.FirstName,
                //    LastName = x.LastName,
                //    Gender = x.Gender,
                //    Address = x.Address,
                //    PhoneNumber = x.PhoneNumber,
                //    MobileNumber = x.MobileNumber,
                //    DeptID = y.DepartmentName
                //}
                //)).ToList();
                return View(GetAllEmp);
            }
            catch
            {
                return View();
            }
            //return View(await _context.Employees.ToListAsync());
        }
        [HttpPost]
        public ActionResult Index(string searchBy, string search)
        {
            try
            {
                var GetEmpById = _obj.GetEmpById(searchBy, search);
                return View(GetEmpById);
            }
            catch
            {
                return View();
            }
            //if (searchBy == "Firstname")
            //{
            //    return View(_context.Employees.Where(x => x.FirstName.StartsWith(search) || search == null).ToList());
            //}
            //else
            //{
            //    return View(_context.Employees.Where(x => x.LastName.StartsWith(search) || search == null).ToList());
            //}
        }

        // GET: Employees/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            LoadDeptDrpDwnList();
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees
                .FirstOrDefaultAsync(m => m.ID == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // GET: Employees/Create
        public IActionResult Create()
        {
            LoadDeptDrpDwnList();
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Employee employee)
        {
            try
            {
                var AddEmployee = _obj.AddEmployee(employee);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }

        }
        //public async Task<IActionResult> Create([Bind("FirstName,LastName,Gender,Address,PhoneNumber,MobileNumber,DeptID")] Employee employee)
        //{
       
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(employee);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(employee);
        //}

        // GET: Employees/Edit/5
       // [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            LoadDeptDrpDwnList();
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,FirstName,LastName,Gender,Address,PhoneNumber,MobileNumber,DesgnID,DeptID")] Employee employee)
        {
            // LoadDeptDrpDwnList();
            if (id != employee.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employee);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(employee.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(employee);

            //try
            //{
            //    LoadDeptDrpDwnList();
            //    var EditEmployee = _obj.EditEmployee(id, employee);
            //    return View(EditEmployee);
            //}
            //catch
            //{
            //    return RedirectToAction(nameof(Index));
            //}
        }

        // GET: Employees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            LoadDeptDrpDwnList();
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees
                .FirstOrDefaultAsync(m => m.ID == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeExists(int id)
        {
            return _context.Employees.Any(e => e.ID == id);
        }

        public void LoadDeptDrpDwnList()
        {
            try
            {
                List<Departments> deptList = new List<Departments>();
                deptList = _context.Department.ToList();
                deptList.Insert(0, new Departments { DeptID = 0, DepartmentName = "Please Select Department" });
                ViewBag.DeptList = deptList;
            }
            catch
            {

            }
        }
        public JsonResult GetEmpList(string Firstname,string Lastname, string searchtb)
        {
            var factList = _obj.GetEmpList(Firstname, Lastname, searchtb);
            //ViewData["GetEmpList"] = tmp;
            return Json(factList);
        }
    }
}
                            