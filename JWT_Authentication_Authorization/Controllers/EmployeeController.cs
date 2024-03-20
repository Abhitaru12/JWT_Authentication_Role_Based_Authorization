using JWT_Authentication_Authorization.Interface;
using JWT_Authentication_Authorization.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace JWT_Authentication_Authorization.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]  // [Authorize(Roles ="Admin")] for roles wise authentication
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService; 
        }

        [HttpGet]
        public List<Employee> GetEmployees()
        {
            return _employeeService.GetEmployeesDetails();
        }

        [HttpPost]
        public Employee AddEmployee([FromBody] Employee employee)
        {
            var emp = _employeeService.AddEmployee(employee);   
            return emp;
        }


    }
}
