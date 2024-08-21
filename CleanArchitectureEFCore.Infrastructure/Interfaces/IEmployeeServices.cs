using CleanArchitectureEFCore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureEFCore.Infrastructure.Interfaces
{
    public interface IEmployeeServices
    {
        Task CreateEmployee(EmployeeRequest employeeRequest);
        Task<Employee> UpdateEmp(int id, EmployeeRequest employeeRequest);
        Task DeleteEmp(int id);
        Task<IEnumerable<Employee>> GetAllEmployee();
        Task<Employee> GetEmployeeById(int id);

    }
}
