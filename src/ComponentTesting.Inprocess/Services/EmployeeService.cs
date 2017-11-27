using System.Collections.Generic;
using System.Linq;
using ComponentTesting.Inprocess.Data;
using ComponentTesting.Inprocess.Models;

namespace ComponentTesting.Inprocess.Services
{
    public class EmployeeService
    {
        private readonly IRepository<Employee> _employeeRepo;

        public EmployeeService(IRepository<Employee> employeeRepo)
        {
            this._employeeRepo = employeeRepo;
        }

        public void Add(string name)
        {
            var employee = new Employee { Name = name };
            _employeeRepo.Save(employee);
        }

        public IEnumerable<Employee> Find(string term)
        {
            return _employeeRepo
                .All()
                .Where(b => b.Name.Contains(term))
                .OrderBy(b => b.Id)
                .ToList();
        }
    }
}