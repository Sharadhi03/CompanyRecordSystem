using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using CompanyRecordAPI.Models;
using CompanyRecord.Data.Entities;
using CompanyRecord.Data.Repository;
using Employee = CompanyRecord.Data.Repository.Employee;
using System.Runtime.Intrinsics.X86;

namespace CompanyRecordAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        public EmployeeRepository EmployeeRepository { get; set; }
        public EmployeeController()
        {
            this.EmployeeRepository = new EmployeeRepository();
        }
        //[HttpGet]
        //public List<TblEmployee> GetAllEmployees()
        //{
        //    return EmployeeRepository.GetAllEmployees();
        //}
        [HttpPost]
        public void AddEmployee(Employee employee)
        {
            TblEmployee tblemployee = new TblEmployee();
            tblemployee.Id = 1;
            tblemployee.Age = employee.Age;
            tblemployee.DepartmentName = employee.DepartmentName;
            tblemployee.Name = employee.Name;
            tblemployee.EmailAddress= employee.EmailAddress;
            tblemployee.Salary= employee.Salary;
            this.EmployeeRepository.AddEmployee(tblemployee);
        }
        [HttpDelete]
        public void DeleteEmployee(int employeeId)
        {
            this.EmployeeRepository.DeleteEmployee(employeeId);
        }
        [HttpGet("{employeeId:int}")]
        public Employee GetEmployeeById(int employeeId)
        {
            return new Employee();
        }


        [HttpGet]
        public IActionResult GetEmployees(string sortOrder, string departmentName = "cse", int? age = 20, string sortBy = "Age", int page = 1, int pageSize = 10)
        {
            var tblEmployees = EmployeeRepository.GetAllEmployees();
            List<Employee> employees = new List<Employee>();
            foreach (var item in tblEmployees)
            {
                var employee = new Employee()
                {
                    Age = item.Age,
                    DepartmentName = item.DepartmentName,
                    EmailAddress = item.EmailAddress,
                    Salary = item.Salary,
                    Name = item.Name,
                };
                employees.Add(employee);
            };
            var query = employees;


            //Filter the results based on the filter parameters
            //if (name != null)
            //    query = query.Where(e => e.Name.Contains(name)).ToList();
            if (departmentName != null)
                query = query.Where(e => e.DepartmentName == departmentName).ToList();
            if (age != null)
                query = query.Where(e => e.Age == age).ToList();

            //Sort the results based on the sortBy and sortOrder parameters
            if (sortBy != null)
            {
                if (sortOrder == "desc")
                    query = query.OrderByDescending(e => e.GetType().GetProperty(sortBy).GetValue(e)).ToList();
                else
                    query = query.OrderBy(e => e.GetType().GetProperty(sortBy).GetValue(e)).ToList();
            }

            //Calculate the number of pages
            int totalRecords = query.Count();
            int totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);

            //Implement pagination
            query = query.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            //Return the paged results
            var results = query.ToList();
            return Ok(new
            {
                TotalRecords = totalRecords,
                TotalPages = totalPages,
                Page = page,
                PageSize = pageSize,
                Results = results
            });
        }
    }
}
