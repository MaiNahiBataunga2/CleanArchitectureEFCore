using CleanArchitectureEFCore.Domain.Entities;
using CleanArchitectureEFCore.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitectureEFCore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeServices _employee;

        public EmployeeController(IEmployeeServices employeeServices)
        {
            _employee = employeeServices;   
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var employee = await _employee.GetAllEmployee();
            return Ok(employee);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmpById(int id)
        {
            var employee = await _employee.GetEmployeeById(id);
            return Ok(employee);
        }

        [HttpPost]
        public async Task<IActionResult> Create(EmployeeRequest employeeRequest) 
        {
            await _employee.CreateEmployee(employeeRequest);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update(int id, EmployeeRequest employeeRequest) 
        {
            if (id <= 0)
                return BadRequest("");

            try 
            {
                var employee = await _employee.UpdateEmp(id, employeeRequest);
                return Ok();
            }
            catch (KeyNotFoundException) 
            {
                return NotFound($"Employee with id {id} not found.");
            }

        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id) 
        {
            if (id <= 0)
                return BadRequest($"Employee with id {id} not found");

            try
            {
                await _employee.DeleteEmp(id);
                return Ok($"Employee with id {id} is deleted Successfully.");
            }
            catch (KeyNotFoundException) 
            {
                return NotFound($"Employee with id {id} not found.");
            }

        }

        
    }
}
