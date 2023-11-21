using CompanyRecord.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyRecord.Data.Repository
{
    public class EmployeeRepository
    {
        EmsContext EmsDbContext { get; set; }
        public EmployeeRepository()
        {
            this.EmsDbContext = new EmsContext();
        }
        public List<TblEmployee> GetAllEmployees()
        {
            return this.EmsDbContext.TblEmployees.ToList();
        }
        public void AddEmployee(TblEmployee employee)
        {
            this.EmsDbContext.TblEmployees.Add(employee);
            this.EmsDbContext.SaveChanges();
        }
        public void DeleteEmployee(int employeeId)
        {
            var employeeNeedsTobeDeleted = this.EmsDbContext.TblEmployees.Where(e => e.Id == employeeId).FirstOrDefault();
            if (employeeNeedsTobeDeleted != null)
            {
                this.EmsDbContext.Remove(employeeNeedsTobeDeleted);
                this.EmsDbContext.SaveChanges();
            }
        }

    }
}
