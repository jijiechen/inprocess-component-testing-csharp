using System.Linq;
using ComponentTesting.Inprocess.Models;
using ComponentTesting.Inprocess.Services;
using Microsoft.AspNetCore.Mvc;

namespace ComponentTesting.Inprocess
{
    [Controller]
    public class EmployeeController
    {
        private readonly EmployeeService _employeeService;

        public EmployeeController(EmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [Route("employees/search/{name}")]
        public string Find(string name)
        {
            var employee = _employeeService.Find(name).FirstOrDefault();
            return employee == null ? "Employee not found" : $"{employee.Name}(id={employee.Id})";
        }
    }
}