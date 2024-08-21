using CleanArchitectureEFCore.Domain.Entities;
using CleanArchitectureEFCore.Infrastructure.Data;
using CleanArchitectureEFCore.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureEFCore.Infrastructure.Services
{
    public class EmployeeServices : IEmployeeServices
    {
        private readonly AppDbContext _context;

        public EmployeeServices(AppDbContext appContext)
        {
            _context = appContext; 
        }

        public async Task CreateEmployee(EmployeeRequest employeeRequest)
        {
            var employee = new Employee 
            {
                Name = employeeRequest.Name,
                Role = employeeRequest.Role,
                Salary = employeeRequest.Salary
            };

            await _context.Employees.AddAsync(employee);
            _context.SaveChanges();

        }

        public async Task DeleteEmp(int id)
        {
            var response = await _context.Employees.FindAsync(id);
            if (response == null)
                throw new KeyNotFoundException("Employee not found with given id.");
            _context.Employees.Remove(response);
            await _context.SaveChangesAsync();

            var remainingEmployees = await _context.Employees.AnyAsync();
            if (!remainingEmployees)
                await _context.Database.ExecuteSqlRawAsync("DBCC CHECkIDENT ('Employees', RESEED, 0)");

        }

        public async Task<IEnumerable<Employee>> GetAllEmployee()
        {
            var response = await _context.Employees.ToListAsync();
            if (response == null)
                throw new ArgumentNullException("No data Found.");
            return response;
        }

        public async Task<Employee> GetEmployeeById(int id)
        {
            var response = await _context.Employees.FindAsync(id);
            return response;
        }

        public async Task<Employee> UpdateEmp(int id, EmployeeRequest employeeRequest)
        {
            var response = await _context.Employees.FindAsync(id);
            if (response == null)
                throw new KeyNotFoundException("No data is present with given id.");

            response.Name = employeeRequest.Name;
            response.Role = employeeRequest.Role;
            response.Salary = employeeRequest.Salary;

            _context.Employees.Update(response);
            await _context.SaveChangesAsync();
            return response;
        }
    }
}
