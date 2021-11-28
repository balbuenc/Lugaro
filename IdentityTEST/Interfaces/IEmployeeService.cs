using CoreERP.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreERP.UI.Interfaces
{
    public interface IEmployeeService
    {
        Task<IEnumerable<Employee>> GetAllEmployees();

        Task<Employee> GetEmployeeDetailsByUsername(string username);

        Task<Employee> GetEmployeeDetails(int id);

        Task SaveEmployee(Employee employee);


        Task DeleteEmployee(int id);
    }
}
