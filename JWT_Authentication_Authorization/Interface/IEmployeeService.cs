using JWT_Authentication_Authorization.Model;

namespace JWT_Authentication_Authorization.Interface
{
    public interface IEmployeeService
    {
        public List<Employee> GetEmployeesDetails();
        public Employee AddEmployee(Employee employee);

    }
}
